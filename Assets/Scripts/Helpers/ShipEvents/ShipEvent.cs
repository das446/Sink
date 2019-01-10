using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Sink {
	public abstract class ShipEvent : MonoBehaviourPun {

		/// <summary>
		/// Recieve trigger from server, do event
		/// </summary>
		[PunRPC]
		public abstract void Activate();

		/// <summary>
		/// Send trigger to server
		/// </summary>
		public void Trigger(){
			PhotonView photonView = PhotonView.Get(this);
			photonView.RPC("Activate",RpcTarget.All);
		}

	}
}