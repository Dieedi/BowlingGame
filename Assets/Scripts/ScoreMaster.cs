using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    public static List<int> ScoreCumulative (List<int> rolls) {
        List<int> cumulativeScores = new List<int> ();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames (rolls)) {
            runningTotal += frameScore;
            cumulativeScores.Add (runningTotal);
        }

        return cumulativeScores;
    }

    public static List<int> ScoreFrames (List<int> rolls) {
        List<int> frameList = new List<int> ();
        int frameTotal = 0;
        int iterator = 0;
        int strikeCount = 0;
        int spare = 0;
        int scoreKeeper = 0;
        int strikePoints = 0;

        foreach (int roll in rolls) {
            frameTotal += roll;

            if (strikeCount > 0 && roll != 10) {
                scoreKeeper += roll;
            }

            if (strikeCount > 0 && iterator % 2 != 0) {
                for (int i = strikeCount; i > 0; i--) {
                    strikePoints = i * 10;

                    if (strikePoints > 30) {
                        strikePoints = 30;
                        frameTotal = strikePoints;
                    } else if (strikePoints == 20){
                        frameTotal = strikePoints + (scoreKeeper - roll);
                    } else {
                        frameTotal = strikePoints + scoreKeeper;
                    }
                    frameList.Add (frameTotal);
                    frameTotal = scoreKeeper;
                }
                strikeCount = 0;
                scoreKeeper = 0;
            } else if (spare > 0 && iterator % 2 == 0) {
                frameList.Add (frameTotal);
                spare = 0;
                frameTotal = roll;
            }

            if (iterator % 2 == 0 && frameTotal == 10) {
                strikeCount++;
                frameTotal = 0;
                if (iterator >= 19) {
                    iterator++;
                } else {
                    iterator += 2;
                }
            } else if (iterator % 2 != 0 && frameTotal == 10) {
                spare++;
                iterator++;
            } else if (iterator % 2 != 0) {
                frameList.Add (frameTotal);
                frameTotal = 0;
                iterator++;
            } else {
                iterator++;
            }
        }

        return frameList;
    }

}
