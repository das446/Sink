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
		public virtual void Interact(LocalPlayer p) {
			p.SendInteractToServer(this);
		}

		public virtual void SendMessage(Player p){
			string message = p.name + " interacted with " + name;
		}

		/// <summary>
		/// This function gets called on the server, don't call it directly from other objects, use Interact instead
		/// </summary>
		public abstract void DoAction(Player p);

	}
}