using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreDisplayTest {

    [Test]
    public void T00PassingTest () {
        Assert.AreEqual (1, 1);
    }

    [Test]
    public void T01Bowl1 () {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T02Bowl12 () {
        int[] rolls = { 1,2 };
        string rollsString = "12";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T03Bowl12strike () {
        int[] rolls = { 10 };
        string rollsString = "X ";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T04Bowlspare82 () {
        int[] rolls = { 8,2 };
        string rollsString = "8/";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T05Bowl0 () {
        int[] rolls = { 0 };
        string rollsString = "-";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T06Bowl90 () {
        int[] rolls = { 9,0 };
        string rollsString = "9-";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T07BowlAll0 () {
        int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0 };
        string rollsString = "--------------------";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T08BowlAll0StrikeAtEnd () {
        int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 10,0,0 };
        string rollsString = "------------------X--";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T09BowlAll0StrikesAtEnd () {
        int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 10,10,10 };
        string rollsString = "------------------XXX";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void T09BowlAll0SpareAtEnd () {
        int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 8,2,10 };
        string rollsString = "------------------8/X";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }

    [Test]
    public void TG03GoldenCopyC2of3 () {
        int[] rolls = { 10, 10, 10, 10, 9,0, 10, 10, 10, 10, 10,9,1};
        string rollsString = "X X X X 9-X X X X X9/";
        Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList ()));
    }
}
