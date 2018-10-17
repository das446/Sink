using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	public abstract class Interactable : NetworkBehaviour {

		public static Dictionary<string, Interactable> Interactables = new Dictionary<string, Interactable>();

		public string guid;

		public static bool networking = false;

		public void Interact(Player p) {
			RpcDoAction(p.gameObject);
		}



		[ClientRpc]
		public void RpcDoAction(GameObject g) {
			DoAction(g.GetComponent<Player>());
		}

		public abstract void DoAction(Player p);

	}
}