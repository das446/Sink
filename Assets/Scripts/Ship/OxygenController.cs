using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	[RequireComponent(typeof(ProgressBar))]
	public class OxygenController : Interactable {

		public Floor floor;
		public Item refItem;
		public int refItemAmnt = 1;

		public ProgressBar bar;

		public TMPro.TMP_Text text;

		public bool hold;
		void Start() {
			bar.text = text;
			bar.Finish += OnBarFinish;
		}
/*
		void Update()
		{
			float fill = floor.oxygen.percent();
			if(fill == 10)
			{
				this.PlaySoundLocalOnly("OxygenRepair", p);
			}
			else if(fill == 0)
			{
				this.PlaySoundLocalOnly("OxygenRepair2", p);
			}
		}
*/
		public override void DoAction(Player p) {

			int size = p.inventory[refItem];

			if (size >= refItemAmnt && !bar.inProgress) {
				p.inventory.UseItem(refItem);
				bar.Activate(p);
				p.locked = true;
				PlaySoundLocalOnly("Assembly", p);

			} else if (!bar.inProgress) {
				bar.DisplayMessage("Requires " + refItemAmnt + " " + refItem.name + Plural(), "Oxygen Terminal", 3);
			}
			

			float fill = floor.oxygen.percent();
			if(fill == 10)
			{
				this.PlaySoundLocalOnly("OxygenRepair", p);
			}
			else if(fill == 0)
			{
				this.PlaySoundLocalOnly("OxygenRepair2", p);
			}

		}
		public string Plural() { return refItemAmnt == 1 ? "" : "s"; }

		public void OnBarFinish(Player p) {
			floor.oxygen.setToMax();
			text.text = "Oxygen Terminal";
			p.locked = false;
			floor.AdjustLightsToOxygen();

		}

		public override void CancelInteract(LocalPlayer p) {
			bar.Cancel();
			p.locked = false;
			text.text = "Oxygen Terminal";
			bar.bar.fillAmount = 0;
		}

	}
}