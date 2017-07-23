using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : MonoBehaviour {

    private ReflectionProbe Probe;

    void Awake () {
        Probe = GetComponent<ReflectionProbe> ();
    }

    void Update () {
        Probe.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y * -1,
            Camera.main.transform.position.z
        );

        Probe.RenderProbe ();
    }
}
