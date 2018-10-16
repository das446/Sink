using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class NetworkController : NetworkBehaviour {

		public Player PlayerPrefab;

		public Player latestPlayer;

		public string clientName;

		public static int count = 0;

		void Start() {

			if (isLocalPlayer) {
				Debug.Log("Call Spawn");
				CmdSpawnPlayer(GetComponent<NetworkIdentity>());
			} else {

			}

		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Q)) {
				if (isLocalPlayer) {
					Debug.Log("Local");
				} else {
					Debug.Log("Nonlocal");
				}
			}
		}

		[Command]
		void CmdSpawnPlayer(NetworkIdentity id) {

			GameObject p = Instantiate(PlayerPrefab).gameObject;

			Debug.Log(connectionToClient.connectionId);

			

			try {
				NetworkServer.SpawnWithClientAuthority(p,id.connectionToClient);
				p.GetComponent<NetworkIdentity>().AssignClientAuthority(id.connectionToClient);
			}
			catch{
				Debug.LogWarning("It says it needs local authority checks, but it only works when it isn't");
			}

			Debug.Log("SpawnPlayer");

		}

	}

}