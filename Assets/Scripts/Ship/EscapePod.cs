using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
    public class EscapePod : Interactable {

        public Item refItem;
        public int refItemAmnt = 1;
        public ProgressBar bar;
        public TMPro.TMP_Text text;

        // Use this for initialization
        void Start() {
            bar.text = text;
            bar.Finish += OnBarFinish;
        }

        // Update is called once per frame
        void Update() {

        }
        public override void DoAction(Player p) {
            
            int size = p.inventory[refItem];

            if (p.role == Player.Role.Saboteur && !bar.inProgress) {
                bar.Activate(p);
            }
        }
        public void OnBarFinish(Player p) {
            refItemAmnt--;
            text.text = "Escape Pod" + refItemAmnt + " parts left";
            if (refItemAmnt == 0) {
                p.Win();
            } else {
                p.inventory.UseItem(refItem);
            }
        }
    }
}
