using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	[RequireComponent(typeof(ProgressBar))]
	public class OxygenController : Interactable {

		public Room room;
		public Item refItem;
		public int refItemAmnt = 1;

		public ProgressBar bar;

		public TMPro.TMP_Text text;

		public bool hold;
		void Start() {
			bar.text = text;
			bar.Finish += OnBarFinish;
		}

		public override void DoAction(Player p) {

			Debug.Log(refItem);
			int size = p.inventory[refItem];

			if (size >= refItemAmnt && !bar.inProgress) {
				Debug.Log(size);
				p.inventory.UseItem(refItem);
				bar.Activate(p);
				p.locked=true;
				
			} else if(!bar.inProgress) {
				text.text = "Requires " + refItemAmnt + " " + refItem.name + Plural();
				this.DoAfterTime(
					() => text.text = "Oxygen", 3
				);
			}

		}
		public string Plural() { return refItemAmnt == 1 ? "" : "s"; }

		public void OnBarFinish(Player p) {
			Debug.Log("OnBarFinish");
			room.oxygen.setToMax();
			text.text = "Oxygen";
			p.locked=false;

		}

		public override void CancelInteract(LocalPlayer p){
			bar.Cancel();
			LocalPlayer.singleton.AutoMove=false;
			text.text = "Oxygen";
			bar.bar.fillAmount=0;
		}

	}
}