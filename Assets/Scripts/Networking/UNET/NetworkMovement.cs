using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMovement : NetworkBehaviour {

	public Vector3 target;
	public float rotY;

	float ourLatency;

	float latencySmoothingFactor = 10;

	public static bool lookWhereGoing = true;

	void Update() {
		target = new Vector3(target.x,transform.position.y,target.z);
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * latencySmoothingFactor);
		if (lookWhereGoing) {
			transform.LookAt(target);
		} else {
			transform.rotation.eulerAngles.Set(0, rotY, 0);
		}

	}

}