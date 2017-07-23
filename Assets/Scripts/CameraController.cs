using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

	public CinemachineVirtualCamera startCam;
	public CinemachineVirtualCamera endCam;
	public CinemachineVirtualCamera swippeCam;

	private CinemachineVirtualCamera endCamVirtualComponent;

	// Use this for initialization
	void Start () {
		endCamVirtualComponent = endCam.GetComponent<CinemachineVirtualCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ActivateStartCam () {
		startCam.gameObject.SetActive (true);
	}

	public void DisableStartCam () {
		startCam.gameObject.SetActive (false);
	}

	public void ActivateEndCam () {
		endCam.gameObject.SetActive (true);
	}

	public void DisableEndCam () {
		endCam.gameObject.SetActive (false);
	}

	public void ActivateSwippeCam () {
		swippeCam.gameObject.SetActive (true);
	}

	public void DisableSwippeCam () {
		swippeCam.gameObject.SetActive (false);
	}

	public void EndCamFocus (GameObject focus) {
		endCamVirtualComponent.LookAt = focus.transform;
	}
}
