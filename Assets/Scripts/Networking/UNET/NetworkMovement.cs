using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	/// <summary>
	/// Get's position, then lerps towards it so there aren't any jumps
	/// </summary>
	public class NetworkMovement : NetworkBehaviour {

		public Vector3 target;
		public float rotY;

		float ourLatency;

		float latencySmoothingFactor = 10;

		public static bool lookWhereGoing = true;

		public Animator animator;

		public Player player;

		void Update() {
			float move = transform.position != target ? 1 : 0;
			player.animator.SetSpeed(move);
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * latencySmoothingFactor);;
			if (lookWhereGoing) {
				Vector3 newTarget = new Vector3(target.x, transform.position.y, target.z);
				transform.LookAt(newTarget);
			} else {
				transform.rotation.eulerAngles.Set(0, rotY, 0);
			}

		}

	}
}