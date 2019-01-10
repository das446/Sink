using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Sink {
	/// <summary>
	/// This class is used so that you can test the main scene directly from the editor without having to first go to the lobby
	/// Doesn't work right now
	/// </summary>
	public class NonNetworkPlayer : LocalPlayer {

		void Start() {
			if (!Application.isEditor || NetworkServer.active) {
				Destroy(gameObject);
			} else {
				Invoke("FixStart", 1);
			}
		}

		void FixStart() {
			gameObject.SetActive(true);
			enabled = true;
			curRoom = GameObject.Find("Tools").GetComponent<Room>();
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>();
			curRoom.Enter(this);
		}

		protected override void OnEnable() {

		}

		void OnDisable() {
			Start();
		}

		public override void SetupNetworking() {

		}

	}
}