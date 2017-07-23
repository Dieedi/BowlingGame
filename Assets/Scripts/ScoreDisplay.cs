using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] RollTexts, ScoreTexts;

	// Use this for initialization
	void Start () {
        
	}

    public void FillRolls (List<int> rolls) {
        string formattedRolls = FormatRolls (rolls);
        for (int i = 0; i < formattedRolls.Length; i++) {
            print (formattedRolls [i]); 
            RollTexts [i].text = formattedRolls [i].ToString ();
        }
    }

    public void FillFrames (List<int> frames) {
        for (int i = 0; i < frames.Count; i++) {
            ScoreTexts [i].text = frames [i].ToString ();
        }
    }

    public static string FormatRolls (List<int> rolls) {
        string output = "";
        for (int i = 0; i < rolls.Count; i++) {
            int roll = output.Length + 1;

            if (rolls[i] == 0) {
                output += "-";
            } else if ((roll % 2 == 0 || roll == 21) && rolls[i - 1] + rolls[i] == 10 && rolls[i - 1] != 10) {    // SPARE
                output += "/";
            } else if (roll >= 19 && rolls [i] == 10) { //STRIKE
                output += "X";
            } else if (rolls [i] == 10) {
                output += "X ";
            } else {
                output += rolls [i].ToString ();
            }
        }
        return output;
    }
}
