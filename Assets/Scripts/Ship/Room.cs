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

		public float OxLossRate;

		public List<Door> Doors;

		public List<Interactable> Interactables;

		public RoomEnterEvent Event;
		public List<Player> players;

		public void Start(){
			temperature = new Temperature();
			this.InvokeRepeat(LoseOxygen,OxLossRate);
		}

		public void Enter(Player player) {

			HUD hud = player.hud;

			player.curRoom = this;
			player.curRoom.players.Add(player);

			hud.temperatureBar.temperature = temperature;
			temperature.bar = player.hud.temperatureBar;
			hud.temperatureBar.update();

			hud.oxygenBar.oxygen = oxygen;
			oxygen.bar = player.hud.oxygenBar;
			hud.oxygenBar.update();

			hud.StopCoroutine("FadeRoomName");
			hud.StartCoroutine(hud.FadeRoomName(this));

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