using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sink {

	public class WaitingLobby : NetworkBehaviour {
		public InputField nameInput;
		public Button StartButton;

		[SerializeField] private int NUMBER_OF_PLAYERS = 2;

		void Start() {
			nameInput.onValueChanged.AddListener(ChangePlayerName);
		}

		void ChangePlayerName(string n) {
			LocalPlayer.LocalPlayerName = n;
		}

		void Update() {
			if (NetworkController.singleton.isServer && NetworkServer.connections.Count >= NUMBER_OF_PLAYERS && !StartButton.IsActive()) {
				StartButton.gameObject.SetActive(true);
			}
		}

		public void LoadGame() {
			NetworkManager.singleton.ServerChangeScene(NetworkController.gameScene);
		}
	}
}