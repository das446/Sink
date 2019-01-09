using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLauncher : MonoBehaviourPunCallbacks {

	[SerializeField]
	private byte maxPlayersPerRoom = 4;

	const string playerNamePrefKey = "PlayerName";

	public Text nameInput;

	void Awake() {
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	void Update() {

	}

	void Connect() {

		PhotonNetwork.NickName = nameInput.text;

		if (PhotonNetwork.IsConnected) {
			PhotonNetwork.JoinRandomRoom();
		} else {
			PhotonNetwork.GameVersion = "1";
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	public override void OnConnectedToMaster() {
		Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnDisconnected(DisconnectCause cause) {
		Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
	}

	public override void OnJoinRandomFailed(short returnCode, string message) {

		Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
	}
}