using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	[RequireComponent(typeof(NetworkIdentity))]
	public abstract class Interactable : NetworkBehaviour {

		public static Dictionary<string, Interactable> Interactables = new Dictionary<string, Interactable>();

		public string guid;

		public static bool networking = false;

		/// <summary>
		/// This function only gets called localy, DoAction gets called by it
		/// </summary>
		/// <remarks>
		/// If you don't want it sent to the server immediately override it and call Send later 
		/// </remarks>
		public virtual void Interact(LocalPlayer p) {
			NetworkController.singleton.CmdInteract(gameObject,p.gameObject);
		}

		public virtual void NetworkCancelInteract(LocalPlayer p){
			NetworkController.singleton.CmdCancelInteract(gameObject,p.gameObject);
		}

		public virtual void SendMessage(Player p) {
			string message = p.name + " interacted with " + name;
		}

		/// <summary>
		/// This function gets called on the server, don't call it directly from other objects, use Interact instead
		/// </summary>
		public abstract void DoAction(Player p);

		protected virtual void CancelInteract(LocalPlayer p){

		}

	}
}