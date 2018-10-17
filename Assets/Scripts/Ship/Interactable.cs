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

		/// <summary>
		/// This function only gets called localy
		/// </summary>
		/// <param name="p"></param>
		public virtual void Interact(LocalPlayer p) {
			p.SendInteractToServer(this);
		}

		/// <summary>
		/// This function gets called on the server
		/// </summary>
		/// <param name="p"></param>
		public abstract void DoAction(Player p);

	}
}