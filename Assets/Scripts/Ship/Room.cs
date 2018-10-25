using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sink {
	public class Room : MonoBehaviour {

		public string Name;
		public Temperature temperature;
		public OxygenLevel oxygen;
		public ElecPower power;

		public float OxLossRate;

		public List<Door> Doors;

		public List<Interactable> Interactables;

		public RoomEnterEvent Event;
		public List<Player> players=new List<Player>();
		public List<Room> rooms = new List<Room>();

		public void Awake(){
			rooms.Add(this);
			temperature = new Temperature();
			this.InvokeRepeat(LoseOxygen,OxLossRate);
		}

		public void Enter(Player player) {

			players.Add(player);
			player.EnterRoom(this);

			

			Event?.Trigger(player);

			
		}

        

        public void Exit(Player player){
			player.curRoom.players.Remove(player);
		}

		public void LoseOxygen(){
			oxygen.Adjust(-1);
		}



	}
}