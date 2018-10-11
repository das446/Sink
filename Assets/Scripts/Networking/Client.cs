using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sink.Network {
	public class Client : MonoBehaviour {

		bool socketReady;
		public string clientName;
		TcpClient socket;
		NetworkStream stream;
		StreamWriter writer;
		StreamReader reader;
		public List<GameClient> Players = new List<GameClient>();
		public bool Host;
		[SerializeField] string host;
		[SerializeField] int port;
		public bool GameStarted;

		public static Client client;

		void Start() {
			DontDestroyOnLoad(gameObject);
			client = this;

		}

		void Update() {
			if (socketReady) {
				if (stream.DataAvailable) {
					string data = reader.ReadLine();
					if (data != null) {
						OnIncomingData(data);
					}
				}
			}
			if (socket != null) {
				if (!socket.Connected) {
					ConnectToServer(host, port);
				}
			}

		}

		public bool ConnectToServer(string host, int port) {
			if (socketReady && writer != null) { return false; }

			try {
				socket = null;
				socket = new TcpClient(host, port);
				stream = socket.GetStream();
				writer = new StreamWriter(stream);
				reader = new StreamReader(stream);

				socketReady = true;
				this.host = host;
				this.port = port;

			} catch (Exception e) { }

			return socketReady;
		}

		void OnIncomingData(string data) {

			string[] aData = data.Split('|');

			switch (aData[0]) {
				case "SWHO":
					for (int i = 1; i < aData.Length; i++) {
						UserConnected(aData[i], false);
					}
					Send("CWHO|" + clientName);
					break;

				case "SCNN":
					UserConnected(aData[1], true);
					break;

				case "Test":
					NetworkManager.debug("Recieved Response From Server");
					break;

				case "Move":
					if(aData[1]!=clientName){
						Player player = Player.players.Where(p => p.name==aData[1]).First();
						player.RecieveMove(data);
					}
					break;

				case "Disconnect":
					UserDisconnected(aData[1]);
					break;

				case "Start":
					if (NetworkManager.Instance.gameStarted || SceneManager.GetActiveScene().buildIndex==1||Players.Count<2 ) { return; }
					NetworkManager.Instance.StartGame(aData);
					GameStarted = true;
					Send("Started|" + clientName);
					break;

				default:
					Debug.LogError("Invalid first network argument "+aData[0]);
					break;
			}
		}
		public void Send(string data) {

			/*if (Host) {
			    Server s = NetworkManager.Instance.server;
			    s.OnIncominngData(s.clients[0], data);
			    return;
			}*/

			if (!socketReady || writer == null) {
				return;

			}
			writer.WriteLine(data);
			writer.Flush();
		}

		public void CloseSocket() {
			if (!socketReady) {
				return;
			}

			writer.Close();
			reader.Close();
			socket.Close();
			socketReady = false;
		}

		void OnIncominngData(ServerClient c, string data) {
			OnIncomingData(data);
		}

		public IEnumerator RequestUntil(Func<bool> condition, string msg) {
			while (!condition()) {
				Send(msg);
				yield return new WaitForSeconds(1);
			}
		}

		public IEnumerator RequestWhile(Func<bool> condition, string msg) {
			while (condition()) {
				Send(msg);
				yield return new WaitForSeconds(1);
			}
		}

		void UserConnected(string Name, bool Host) {
			if (Name == "" || Players.Any(x => x.name == Name)) { return; }
			GameClient c = new GameClient();
			c.name = Name;
			Players.Add(c);
			Debug.Log(c.name+ "Connected");
			
		}

		void OnApplicationQuit() {
			CloseSocket();
		}

		void OnDisable() {
			CloseSocket();
		}
		void OnDestroy() {
			CloseSocket();
		}

		void UserDisconnected(string Name) {
			Debug.Log(Name + " Disconnected");
		}

	}

	public class GameClient {
		public string name;
		public bool isHost;
	}
}