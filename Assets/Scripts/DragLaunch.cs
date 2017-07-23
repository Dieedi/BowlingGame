using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Ball))]
public class DragLaunch : MonoBehaviour {

	public GameObject Floor;

	private Ball ball;
	private Vector3 dragStartPosition, dragEndPosition;
	private float startTimer, endTimer;
	private PinSetter pinSetter;
	private bool waitingMessagePlay = false;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
	}

	void Update () {
		if (!pinSetter.IsPinSetterReady () && !waitingMessagePlay) {
			PlayWaitingMessage ();
		}
	}

	void PlayWaitingMessage () {
		waitingMessagePlay = true;
	}

	public void DragStart () {
		dragStartPosition = Input.mousePosition;
		startTimer = Time.time;
	}

	public void DragEnd () {
		if (pinSetter.IsPinSetterReady ()) {
			dragEndPosition = Input.mousePosition;
			endTimer = Time.time;

            float dragSpeed = endTimer - startTimer;
			float dragVelocityX = (dragEndPosition.x - dragStartPosition.x) / dragSpeed;
			float dragVelocityZ = (dragEndPosition.y - dragStartPosition.y) / dragSpeed;
            if (dragVelocityZ <= 100) {
                dragVelocityZ = 100;
            }

			Vector3 DragVelocity = new Vector3 (dragVelocityX, 0, dragVelocityZ);
			if (!ball.ballLaunched) {
				ball.Launch (DragVelocity);
			}
		}
	}

	public void MoveStart(float moveValue) {
		if (!ball.ballLaunched) {
			BoxCollider floorCollider = Floor.GetComponent<BoxCollider> ();
			Vector3 bounds = floorCollider.bounds.size;
			float sizeX = bounds.x;

			float midPosValue = sizeX / 2;
			float newPosX = Mathf.Clamp(transform.position.x + moveValue, -midPosValue, midPosValue);

			Vector3 newPosition = new Vector3 (newPosX, transform.position.y, transform.position.z);
			transform.position = newPosition;
		}
	}
}
