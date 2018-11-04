using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	/// <summary>
	/// This should be the only class with network command and rpc functions
	/// </summary>
	public class NetworkController : NetworkBehaviour {

		public Player PlayerPrefab;

		public Player latestPlayer;

		public string clientName;

		public static int count = 0;

		public Room StartRoom;

		public static NetworkController singleton;

		void Start() {

			if (isLocalPlayer) {
				singleton = this;
				CmdSpawnPlayer(GetComponent<NetworkIdentity>());

			}

		}

		[Command]
		void CmdSpawnPlayer(NetworkIdentity id) {

			GameObject p = Instantiate(PlayerPrefab, transform.position, Quaternion.identity).gameObject;

			NetworkServer.SpawnWithClientAuthority(p, id.connectionToClient);
			p.GetComponent<NetworkIdentity>().AssignClientAuthority(id.connectionToClient);
		}

		[Command]
		public void CmdCancelInteract(GameObject interactable, GameObject player) {
			RpcCancelInteract(interactable, player);
		}

		[ClientRpc]
		void RpcCancelInteract(GameObject interactable, GameObject player) {
			interactable.GetComponent<Interactable>()?.CancelInteract(player.GetComponent<LocalPlayer>());
		}

		[Command]
		public void CmdSendWinnerOverNetwork(string winnerRole) {
			RpcSendWinnerOverNetwork(winnerRole);
		}

		[ClientRpc]
		public void RpcSendWinnerOverNetwork(string winnerRole) {
			PlayerPrefs.SetString("WinnerS", winnerRole);
		}

		[Command]
		public void CmdUpdatePos(Vector3 v, float rotY, GameObject p) {
			RpcUpdateTargetPos(v, rotY, p);
		}

		[ClientRpc]
		private void RpcUpdateTargetPos(Vector3 v, float rotY, GameObject p) {
			p.GetComponent<Player>().UpdateTargetPos(v, rotY);
		}

		[Command]
		public void CmdInteract(GameObject i, GameObject player) {
			RpcDoAction(i, player);
		}

		[ClientRpc]
		public void RpcDoAction(GameObject i, GameObject p) {
			Interactable interactable = i.GetComponent<Interactable>();
			Player player = p.GetComponent<Player>();
			interactable.DoAction(player);
		}

	}

}