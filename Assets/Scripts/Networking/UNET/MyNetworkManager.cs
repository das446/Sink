using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class MyNetworkManager : NetworkManager {
		public string clientName;
		string d = "";
		bool server;

		public override void OnStartHost() {
			server = true;
			clientName = "Player1";

		}

		public override void OnStartClient(NetworkClient client) {
			if (!server) {
				clientName = "Player2";
			}
		}

		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
			GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			Debug.Log(player.name+playerControllerId);
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