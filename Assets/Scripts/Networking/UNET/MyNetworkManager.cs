using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Sink {

	public class MyNetworkManager : NetworkManager {
		public string clientName;
		string d = "";
		bool server;
		NetworkMatch cur;

		public override void OnStartHost() {
			server = true;
			clientName = "Player1";

		}

		public new void StartMatchMaker() {

		}

		public void SetMatchHost(string newHost, int port, bool https) {
			if (matchMaker == null) {
				matchMaker = gameObject.AddComponent<NetworkMatch>();
			}
			if (newHost == "localhost" || newHost == "127.0.0.1") {
				newHost = Environment.MachineName;
			}
			string prefix = "http://";
			if (https) {
				prefix = "https://";
			}

			if (LogFilter.logDebug) { Debug.Log("SetMatchHost:" + newHost); }
			string m_MatchHost = newHost;
			int m_MatchPort = port;
			matchMaker.baseUri = new Uri(prefix + m_MatchHost + ":" + m_MatchPort);
		}

		public override void OnStartClient(NetworkClient client) {
			if (!server) {
				clientName = "Player2";
			}
		}

		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
			GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			NetworkController c = player.GetComponent<NetworkController>();
			if (c != null) {
				if (server) {
					c.clientName = clientName;
					clientName = d;
				}
			}
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}

	}
}