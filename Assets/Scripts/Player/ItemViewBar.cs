using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Implement open and close, override open menu
//

namespace Sink {
    public class ItemViewBar : MonoBehaviour {

        public Image icon;

        void Start() {
            Player.ItemChange += UpdateBar;
        }

        void UpdateBar(Player p, Item i) {
            Debug.Log("localPlayer="+ (p==LocalPlayer.singleton));
            if (p != LocalPlayer.singleton) {
                return;
            }
            if (i == null) {
                icon.enabled = false;
            } else {
                Debug.Log("icon");
                icon.enabled = true;
                icon.sprite = i.uiImage;
            }
        }

    }
}