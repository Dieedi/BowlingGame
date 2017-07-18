using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutter : MonoBehaviour {

    private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<PinSetter> ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit () {
        pinSetter.ballLeftBox = true;
    }
}
