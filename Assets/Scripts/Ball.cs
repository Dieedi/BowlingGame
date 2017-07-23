using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent (typeof (CameraController))]
public class Ball : MonoBehaviour {

	public Vector3 velocity;
	public GameObject BallCam2;
	public bool ballLaunched = false;
	public bool camActivated = false;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 ballStartingPos;
	private CameraController cameraController;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		cameraController = FindObjectOfType<CameraController> ();

		rigidBody.useGravity = false;

		ballStartingPos = transform.position;
	}

	public void Launch (Vector3 velocity)
	{
		ballLaunched = true;

		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;

		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z >= 1400 && !camActivated) {
			cameraController.ActivateEndCam ();
            //cameraController.DisableStartCam ();
			camActivated = true;
		}
	}

	public void Reset () {
		ballLaunched = false;

		rigidBody.useGravity = false;
        transform.rotation = Quaternion.identity;
		transform.position = ballStartingPos;
		rigidBody.velocity = rigidBody.angularVelocity = Vector3.zero;
		audioSource.Stop ();
		cameraController.DisableEndCam ();
		cameraController.EndCamFocus (gameObject);
		camActivated = false;

        //cameraController.ActivateStartCam ();
	}

    void OnCollisionEnter (Collision coll) {
        foreach (ContactPoint contact in coll.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
