using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : MonoBehaviourPunCallbacks {

	[SerializeField] string mainGameSceneName;
	[SerializeField] int MIN_NUM_PLAYERS=2;

	public void LeaveRoom() {
		PhotonNetwork.LeaveRoom();
	}

	public void StartGame() {
		if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().name != mainGameSceneName) {
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}
		Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
		PhotonNetwork.LoadLevel(mainGameSceneName);
	}

	public override void OnPlayerEnteredRoom(Player other) {
		Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

		if (PhotonNetwork.IsMasterClient) {
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

			StartGame();
		}
	}

	public override void OnPlayerLeftRoom(Player other) {
		Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

		if (PhotonNetwork.IsMasterClient) {
			Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
			StartGame();
		}
	}
}