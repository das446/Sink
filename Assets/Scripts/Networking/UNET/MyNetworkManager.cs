using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class MyNetworkManager : MonoBehaviour {
		public GameObject treePrefab;
		NetworkClient myClient;

		// Create a client and connect to the server port
		public void ClientConnect() {
			ClientScene.RegisterPrefab(treePrefab);
			myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnClientConnect);
			myClient.Connect("127.0.0.1", 4444);
		}

		void OnClientConnect(NetworkMessage msg) {
			Debug.Log("Connected to server: " + msg.conn);
		}
	}
}