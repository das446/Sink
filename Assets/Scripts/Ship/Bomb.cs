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

		public override bool CanInteract(Player p) {
			return p.curFloor.oxygen.curOx > 0;
		}

		public override void DoAction(Player p) {

			if (!CanInteract(p)) {
				bar.DisplayMessage("Too low on oxygen", "Bomb - " + amntLeft + " parts left", 1);
			} else if (p.item != item) {
				bar.DisplayMessage("Requires 1 Gear", "Bomb - " + amntLeft + " parts left", 1);
			} else if (p.role == Player.Role.Saboteur && !bar.inProgress) {
				bar.Activate(p);
				PlaySoundLocalOnly("Assembly", p);
			}
			// else if(p.role == Player.Role.Crew && bar.inProgress){
			// 	bar.Cancel(p);
			// }
		}

		public void OnBarFinish(Player p) {
			amntLeft--;
			text.text = "Bomb - " + amntLeft + " parts left";
			if (amntLeft == 0) {
				LocalPlayer.singleton.hud.chatSystem.GenerateMessage("The bomb has been activated");
				p.Win();
			} else {
				p.UseItem(item);
			}
		}
	}
}