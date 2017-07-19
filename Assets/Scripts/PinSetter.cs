using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent (typeof (CameraController))]
public class PinSetter : MonoBehaviour {

	public int maxRoundSteps = 2;
	public GameObject Pins;
    public bool pinSetterReadyState = true;

	private float lastChangeTime;
	private int roundStepCount = 1;
	private Animator animator;
    private CameraController cameraController;
    private PinCounter pinCounter;

	// Use this for initialization
    void Start () {
		animator = GetComponent<Animator> ();
		cameraController = FindObjectOfType<CameraController> ();
        pinCounter = FindObjectOfType<PinCounter> ();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PerformAction (ActionMaster.Action action) {
        switch (action) {
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
        pinCounter.Reset ();
	}

	public bool IsPinSetterReady () {
		if (pinSetterReadyState) {
			return true;
		}

		return false;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.GetComponent<Ball> ()) {
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
