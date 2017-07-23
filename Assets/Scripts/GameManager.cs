using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> bowls = new List<int> ();
    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<PinSetter> ();
        ball = FindObjectOfType < Ball> ();
        scoreDisplay = FindObjectOfType<ScoreDisplay> ();
	}

    public void StartGame () {
        Destroy(GameObject.Find ("SplashScreen").gameObject);
    }
	
    public void Bowl (int pinFall) {
        bowls.Add (pinFall);
        pinSetter.PerformAction (ActionMaster.NextAction (bowls));
        ball.Reset ();

        try {
            scoreDisplay.FillRolls (bowls);
            scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative (bowls));
        } catch {
            Debug.LogWarning ("Error in scoring");
        }
    }
}
