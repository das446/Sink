using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class Door : Interactable {

		public Room room1, room2;

		public Image room1O, room2O, room1P, room2P, room1T, room2T; // these got deleted at some point

		public Text Room1Name, Room2Name;

		public GameObject Canvas1, Canvas2;

		public bool locked=false;

		// void Start(){
		// 	Room1Name.text = room1.Name;
		// 	Room2Name.text = room2.Name;
		// }

		public override void DoAction(Player p) {
			if(locked){
				//Locked sound effect?
				return;
			}

			if (p.curRoom == room1) {
				p.StartCoroutine(p.WalkThroughDoor(this,room2));
			} else if (p.curRoom == room2) {
				p.StartCoroutine(p.WalkThroughDoor(this,room1));
			}
		}

		// void Update() {
		// 	room1O.fillAmount = room1.oxygen.percent();
		// 	room2O.fillAmount = room2.oxygen.percent();

		// 	room1P.fillAmount = room1.power.percent();
		// 	room2P.fillAmount = room2.power.percent();

		// 	room1T.fillAmount = room1.temperature.percent();
		// 	room2T.fillAmount = room2.temperature.percent();
		// }

		public void Lock(Player locker){

		}

		public void TurnOnUI() {
			Canvas1.SetActive(true);
			Canvas2.SetActive(true);
		}

		public void TurnOffUI() {
			Canvas1.SetActive(true);
			Canvas2.SetActive(true);
		}
	}
}