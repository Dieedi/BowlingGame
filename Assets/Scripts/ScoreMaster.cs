using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster {

    private enum FrameState {Start, End};

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
        int iterator = 1;
        int strikeCount = 0;
        int spare = 0;
        int lastRoll = 0;

        foreach (int roll in rolls) {
            frameTotal += roll;

            if (strikeCount == 1 && spare == 1
                || strikeCount != 0 && iterator % 2 != 0 && spare == 1) {
                frameList.Add (20);
                frameList.Add (10 + roll);
                spare = 0;
                strikeCount = 0;
                frameTotal = roll;
            }

            if (strikeCount != 0) {
                if (strikeCount == 2 && roll == 10 && iterator % 2 != 0) {
                    strikeCount++;
                }

                if (roll == 10 && strikeCount == 1 && iterator != 21) {
                    strikeCount++;
                    if (iterator < 20) {
                        iterator++;
                    }
                } else if (iterator == 21) {
                    frameList.Add (frameTotal);
                } else if (roll == 10) {
                    frameList.Add (30);
                    frameTotal = 20;
                    strikeCount--;
                    if (iterator < 19) {
                        iterator++;
                    }
                } else if (iterator % 2 != 0 && strikeCount == 2
                    || iterator == 20 && strikeCount == 2) {
                    frameList.Add (frameTotal);
                    frameTotal = 10 + roll;
                    strikeCount--;
                } else if (iterator % 2 == 0 && iterator != 20 && (roll+lastRoll) != 10) {
                    // add strike + bonus
                    frameList.Add (frameTotal);
                    frameList.Add (lastRoll + roll);
                    frameTotal = 0;
                    strikeCount--;
                } else if (iterator % 2 == 0 && (roll + lastRoll) == 10) {
                    spare++;
                } else if (iterator == 20 && lastRoll + roll < 10) {
                    frameList.Add (frameTotal);
                    frameList.Add (lastRoll + roll);
                }
            } else if (spare != 0) {
                frameList.Add (frameTotal);
                if (roll == 10) {
                    strikeCount++;
                    if (iterator < 19) {
                        iterator++;
                    }
                }
                frameTotal = roll;
                spare = 0;
            } else {
                if (iterator % 2 != 0 && roll == 10 && iterator == 19) {
                    strikeCount++;
                } else if (iterator % 2 != 0 && roll == 10) {
                    strikeCount++;
                    iterator++;
                } else if (iterator % 2 == 0 && (roll + lastRoll) == 10) {
                    spare++;
                } else if (iterator % 2 == 0) {
                    frameList.Add (frameTotal);
                    frameTotal = 0;
                }
            }
            lastRoll = roll;

            iterator++;
        }

        return frameList;
    }

}
