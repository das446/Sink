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

		public Room StartRoom;

		void Start() {

			if (isLocalPlayer) {
				CmdSpawnPlayer(GetComponent<NetworkIdentity>());
			}

		}

		[Command]
		void CmdSpawnPlayer(NetworkIdentity id) {

			GameObject p = Instantiate(PlayerPrefab,transform.position,Quaternion.identity).gameObject;

			NetworkServer.SpawnWithClientAuthority(p, id.connectionToClient);
			p.GetComponent<NetworkIdentity>().AssignClientAuthority(id.connectionToClient);
		}

	}

}