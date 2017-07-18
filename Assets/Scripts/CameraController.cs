using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

	public CinemachineVirtualCamera startCam;
	public CinemachineVirtualCamera endCam;
	public CinemachineVirtualCamera swippeCam;

	private CinemachineVirtualCamera startCamVirtualComponent;
	private CinemachineVirtualCamera endCamVirtualComponent;
	private CinemachineVirtualCamera swippeCamVirtualComponent;

	// Use this for initialization
	void Start () {
		startCamVirtualComponent = startCam.GetComponent<CinemachineVirtualCamera> ();
		endCamVirtualComponent = endCam.GetComponent<CinemachineVirtualCamera> ();
		swippeCamVirtualComponent = swippeCam.GetComponent<CinemachineVirtualCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ActivateStartCam () {
		print ("activate start cam");
		startCam.gameObject.SetActive (true);
	}

	public void DisableStartCam () {
		print ("disable start cam");
		startCam.gameObject.SetActive (false);
	}

	public void ActivateEndCam () {
		print ("activate end cam");
		endCam.gameObject.SetActive (true);
	}

	public void DisableEndCam () {
		print ("disable end cam");
		endCam.gameObject.SetActive (false);
	}

	public void ActivateSwippeCam () {
		print ("activate end cam");
		swippeCam.gameObject.SetActive (true);
	}

	public void DisableSwippeCam () {
		print ("disable end cam");
		swippeCam.gameObject.SetActive (false);
	}

	public void EndCamFocus (GameObject focus) {
		print ("set focus end cam " + focus);
		endCamVirtualComponent.LookAt = focus.transform;
	}
}
