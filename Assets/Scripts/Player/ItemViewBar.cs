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

        /* 
        public override void Open(LocalPlayer p) {
            gameObject.SetActive(true);
            player = p;
            inventory = p.inventory;
            foreach (KeyValuePair<Item, int> entry in inventory.items) {
                UiItemButton button = Instantiate(baseButton, container.transform);
                button.Init(entry.Key, entry.Value);
                buttons.Add(button);
            }
        }
        public override  void Close(LocalPlayer p) {
            for (int i = 0; i < buttons.Count; i++) {
                Destroy(buttons[i].gameObject);
            }
            buttons = new List<UiItemButton>();
            gameObject.SetActive(false);
        }
        */

    }
}