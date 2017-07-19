using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public Text pinCounterText;
    public float secondsToWait = 4;
    public int lastStandingCount = -1;
    public bool ballLeftBox = false;

    private int countStanding;
    private int countFallen = 0;
    private int lastMaxFallen = 10;
    private PinSetter pinSetter;
    private CameraController cameraController;
    private Ball ball;
    private GameManager gameManager;
	
    void Start () {
        ball = FindObjectOfType<Ball> ();
        pinCounterText = GameObject.Find ("PinCounterText").GetComponent<Text> ();
        gameManager = FindObjectOfType<GameManager> ();
        pinSetter = FindObjectOfType<PinSetter> ();
        cameraController = FindObjectOfType<CameraController> ();
    }

    void Update () {
        if (ballLeftBox) {
            CheckStanding ();
            pinCounterText.color = Color.red;
        }
    }

    void OnTriggerExit () {
        ballLeftBox = true;
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
        pinCounterText.color = Color.red;
        pinCounterText.text = pins.ToString();
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

        pinCounterText.color = Color.green;

        if ((10 - countStanding) != countFallen) {
            lastMaxFallen -= countFallen;
            countFallen = lastMaxFallen - CountStandingPins ();
        } else {
            countFallen = 0;
        }

        Debug.Log ("It left " + countStanding + " pins, so there is " + countFallen + " pins falled at this turn.");

        gameManager.Bowl (countFallen);

        pinSetter.pinSetterReadyState = false;

        cameraController.ActivateSwippeCam ();
    }

    public void Reset () {
        lastMaxFallen = 10;
        countFallen = 0;
    }
}
