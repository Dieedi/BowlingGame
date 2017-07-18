using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent (typeof (CameraController))]
public class PinSetter : MonoBehaviour {

	public Text pinCounter;
	public int lastStandingCount = -1;
	public float secondsToWait = 4;
	public int maxRoundSteps = 2;
	public GameObject Pins;
    public bool pinSetterReadyState = true;
    public bool ballLeftBox = false;

    private ActionMaster actionMaster;
	private int countStanding;
    private int countFallen = 0;
    private int lastMaxFallen = 10;
	private float lastChangeTime;
	private Ball ball;
	private int roundStepCount = 1;
	private Animator animator;
    private CameraController cameraController;
    private ActionMaster.Action designedAction;

	// Use this for initialization
    void Start () {
        actionMaster = new ActionMaster ();
		animator = GetComponent<Animator> ();
		cameraController = FindObjectOfType<CameraController> ();

		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ballLeftBox) {
            CheckStanding ();
            pinCounter.color = Color.red;
		}
	}

	void CheckStanding () {
		UpdatePinCounter (CountStandingPins ());

		// pins have settled if lastStandingCount didn't change since 3 seconds
        if (lastStandingCount == countStanding) {
			StartCoroutine(PinsHaveSettled ());
		} else {
			lastStandingCount = countStanding;
			StopCoroutine (PinsHaveSettled ());
		}
	}

	void UpdatePinCounter (int pins) {
		pinCounter.color = Color.red;
		pinCounter.text = pins.ToString();
	}

	int CountStandingPins ()
	{
		countStanding = 0;
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin> ()) {
			if (pin.IsStanding ()) {
				countStanding++;
            }
		}
		return countStanding;
    }

	IEnumerator PinsHaveSettled () {
		
		yield return new WaitForSeconds (secondsToWait);
        StopAllCoroutines ();

		ballLeftBox = false;

        pinCounter.color = Color.green;

        if ((10 - countStanding) != countFallen) {
            lastMaxFallen -= countFallen;
            countFallen = lastMaxFallen - CountStandingPins ();
        } else {
            countFallen = 0;
        }

        Debug.Log ("It left " + countStanding + " pins, so there is " + countFallen + " pins falled at this turn.");

        // ActionMaster must returns actions to do
        designedAction = actionMaster.Bowl (countFallen);

        pinSetterReadyState = false;

        cameraController.ActivateSwippeCam ();

        ball.Reset ();

        switch (designedAction) {
        case ActionMaster.Action.EndGame:
            Debug.Log ("fin de la partie");
            break;
        case ActionMaster.Action.EndTurn:
            print ("endTurn");
            ResetPins ();
            break;
        case ActionMaster.Action.Reset:
            print ("Reset");
            ResetPins ();
            break;
        case ActionMaster.Action.Tidy:
            print ("Tidy");
            SwippePins ();
            break;
        }
		//ExecuteSwippe();
	}

	void ExecuteSwippe () {
		roundStepCount++;

		if (roundStepCount > maxRoundSteps) {
			ResetPins ();
		} else {
			SwippePins ();
		}
	}

	void SwippePins () {
		animator.SetTrigger ("Tidy");
	}

	void ResetPins () {
		animator.SetTrigger ("Reset");
	}

	void RaisePins () {
		foreach (Pin pin in FindObjectsOfType<Pin> ()) {
			pin.Raise ();
			// tricks to avoid unwanted rotations
			pin.pinShouldStand = true;
		}
	}

	public void LowerPins () {
		foreach (Pin pin in FindObjectsOfType<Pin> ()) {
			pin.Lower ();
			// tricks to avoid unwanted rotations
			pin.pinShouldStand = true;
		}
		StartNewLaunch ();
	}

	void StartNewLaunch ()
	{
		animator.ResetTrigger ("Reset");
		animator.ResetTrigger ("Tidy");
		pinSetterReadyState = true;
		cameraController.DisableSwippeCam ();
	}

	public void RenewPins () {
		Instantiate (Pins, new Vector3 (0f, 40f, 1890f), Quaternion.identity);
        lastMaxFallen = 10;
        countFallen = 0;
	}

	public bool IsPinSetterReady () {
		if (pinSetterReadyState) {
			return true;
		}

		return false;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.GetComponent<Ball> ()) {
			ballLeftBox = true;
			// tricks to avoid unwanted rotations
			foreach (Pin pin in FindObjectsOfType<Pin> ()) {
				pin.pinShouldStand = false;
			}
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.GetComponent<Pin> ()) {
			Destroy (collider.gameObject);
		}

		if (collider.GetComponent<Ball> ()) {
			cameraController.EndCamFocus (gameObject);
		}
	}
}
