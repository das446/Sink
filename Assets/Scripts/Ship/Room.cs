using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sink {
	public class Room : MonoBehaviour {

		public Temperature temperature;
		public OxygenLevel oxygen;
		public ElecPower power;

		/// <summary>
		/// loses 1 oxygen every n seconds
		/// </summary>
		public float OxLossRate = 1;

		public List<Door> Doors;

		public List<Interactable> Interactables;

		public RoomEnterEvent roomEnterEvent;
		public List<Player> players = new List<Player>();
		public List<Room> rooms = new List<Room>();

		public List<Transform> possibleSpawnLocations;

		public void Awake() {
			rooms.Add(this);
			temperature = new Temperature();
			this.InvokeRepeat(LoseOxygen, OxLossRate);
		}

		public void Enter(Player player) {

			players.Add(player);

			roomEnterEvent?.Trigger(player);

		}

		public void Exit(Player player) {
			player.curRoom.players.Remove(player);
		}

		public void LoseOxygen() {
			oxygen.Adjust(-1);
		}

		void OnDrawGizmosSelected() {
			foreach (Transform t in possibleSpawnLocations) {
				Gizmos.DrawSphere(t.position, 30);
			}
		}

	}
}