using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingThreshold = 3;
	// tricks to avoid unwanted rotations
	public bool pinShouldStand = true;

	private float raisedPinsYPos = 40;
	private Rigidbody rigibody;

	// Use this for initialization
	void Start () {
		rigibody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		// tricks to avoid unwanted rotations
		if (pinShouldStand) {
			rigibody.angularVelocity = Vector3.zero;
		}
	}

	public bool IsStanding () {
		Vector3 currentAngles = transform.eulerAngles;

		float currentXAngle = 180 - Mathf.Abs (180 - currentAngles.x);
		float currentZAngle = 180 - Mathf.Abs (180 - currentAngles.z);

		if (currentXAngle < standingThreshold && currentZAngle < standingThreshold) {
			return true;
		}

		return false;
	}

	public void Raise () {
		if (IsStanding ()) {
			rigibody.useGravity = false;
			Vector3 newPos = new Vector3 (0, raisedPinsYPos, 0);
			transform.Translate(newPos, Space.World);
			rigibody.angularVelocity = Vector3.zero;
		}
	}

	public void Lower () {
		if (IsStanding ()) {
			Vector3 newPos = new Vector3 (0, -raisedPinsYPos, 0);
			transform.Translate(newPos, Space.World);
			rigibody.useGravity = true;
			rigibody.angularVelocity = Vector3.zero;
		}
	}
}
