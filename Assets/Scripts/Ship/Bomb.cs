using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	public class Bomb : Interactable {

		public Item item;
		public int amntLeft;
		public ProgressBar bar;
		public TMPro.TMP_Text text;

		void Start() {
			bar.text = text;
			bar.Finish += OnBarFinish;
			text.text = "Bomb - " + amntLeft + " parts left";
		}

		public override void DoAction(Player p) {

			Debug.Log("Bomb");
			if (p.inventory[item] <= 0) {
				bar.DisplayMessage("Requires 1 Gear", "Bomb - " + amntLeft + " parts left", 1);
			} else if (p.role == Player.Role.Saboteur && !bar.inProgress) {
				bar.Activate(p);
			}
			// else if(p.role == Player.Role.Crew && bar.inProgress){
			// 	bar.Cancel(p);
			// }
		}

		public void OnBarFinish(Player p) {
			amntLeft--;
			text.text = "Bomb - " + amntLeft + " parts left";
			if (amntLeft == 0) {
				p.Win();
			} else {
				p.inventory.UseItem(item);
			}
		}
	}
}