using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMovement : NetworkBehaviour {

	public Vector3 target;

	float ourLatency;

	float latencySmoothingFactor = 10;

	void Update() {
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * latencySmoothingFactor);
	}

	

	

}