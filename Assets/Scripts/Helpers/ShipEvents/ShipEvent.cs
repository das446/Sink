using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	public abstract class ShipEvent : NetworkBehaviour {

		/// <summary>
		/// Recieve trigger from server, do event
		/// </summary>
		public abstract void Activate();

		/// <summary>
		/// Send trigger to server
		/// </summary>
		public void Trigger(){
			NetworkController.singleton.CmdTriggerEvent(gameObject);
		}

	}
}