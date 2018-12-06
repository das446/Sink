using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Sink {
	/// <summary>
	/// This should be the only class with network command and rpc functions
	/// </summary>
	public class NetworkController : NetworkBehaviour {

		public Player PlayerPrefab;

		public string clientName;

		public static int count = 0;

		public Room StartRoom;

		public static NetworkController singleton;

		public static string gameScene = "SampleScene";

		public Player player;

		public Player.Role role;

		public static List<NetworkController> controllers = new List<NetworkController>();

		void Start() {
			if (isLocalPlayer) {
				singleton = this;
			}
			if (isLocalPlayer && SceneManager.GetActiveScene().name == gameScene) {

				CmdSpawnPlayer(GetComponent<NetworkIdentity>(), LocalPlayer.LocalPlayerName, role);

			}

		}

		[Command]
		void CmdSpawnPlayer(NetworkIdentity id, string name, Player.Role role) {
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
		public void CmdChangePlayerName(GameObject p, string n) {
			RpcChangePlayerName(p, n);
		}

		[ClientRpc]
		private void RpcChangePlayerName(GameObject p, string n) {
			p.GetComponent<Player>().OnChangeName(n);
		}

		[Command]
		public void CmdChangePlayerRole(GameObject p, Player.Role r) {
			RpcChangePlayerRole(p, r);
		}

		[ClientRpc]
		private void RpcChangePlayerRole(GameObject p, Player.Role r) {
			p.GetComponent<Player>().OnChangeRole(r);
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

		[Command]
		public void CmdTriggerEvent(GameObject e) {
			RpcTriggerEvent(e);
		}

		[ClientRpc]
		public void RpcTriggerEvent(GameObject e) {
			ShipEvent ev = e.GetComponent<ShipEvent>();
			ev.Activate();
		}

	}

}