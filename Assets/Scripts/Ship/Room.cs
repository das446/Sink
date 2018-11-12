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

		

		public List<Door> Doors;

		public List<Interactable> Interactables;

		public RoomEnterEvent roomEnterEvent;
		public List<Player> players = new List<Player>();
		public List<Room> rooms = new List<Room>();

		public List<Transform> possibleSpawnLocations;

		public void Awake() {
			rooms.Add(this);
		}

		public void Enter(Player player) {

			players.Add(player);

			roomEnterEvent?.Trigger(player);

		}

		public void Exit(Player player) {
			player.curRoom.players.Remove(player);
		}



		void OnDrawGizmosSelected() {
			if(possibleSpawnLocations.Count==0){return;}
			foreach (Transform t in possibleSpawnLocations) {
				Gizmos.DrawSphere(t.position, 30);
			}
		}

	}
}