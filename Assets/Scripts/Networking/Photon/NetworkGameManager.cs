using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : MonoBehaviourPunCallbacks {

	[SerializeField] string mainGameSceneName;
	[SerializeField] int MIN_NUM_PLAYERS = 2;

	public GameObject playerPrefab;
	public Transform startPos;

	[SerializeField] bool startSolo;

	void Start() {
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	public void LeaveRoom() {
		PhotonNetwork.LeaveRoom();
	}

	public void StartGame() {
		if (CanStart()) {
			//Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
			PhotonNetwork.LoadLevel(mainGameSceneName);
		}
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		Debug.Log(scene.name);
		if (scene.name == "SampleScene") {
			PhotonNetwork.Instantiate(playerPrefab.name, startPos.position, Quaternion.identity);
		}
	}

	private bool CanStart() {
		bool validScene = SceneManager.GetActiveScene().name != mainGameSceneName;
		bool startSoloInEditor = startSolo && Application.isEditor;
		bool validPlayers = PhotonNetwork.IsMasterClient || startSoloInEditor;
		bool canStart = validScene && validPlayers;
		Debug.Log("Can start=" + canStart);
		return canStart;
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