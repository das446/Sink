using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Implement open and close, override open menu
//

namespace Sink {
    public class ItemViewBar : MonoBehaviour {

        public List<Sprite> images;

        public GameObject canvasGrid;
        public GameObject itemimage_reference;
        public Inventory inventory;
        private List<Item> items;

        public UiItemButton baseButton;

        public List<UiItemButton> buttons;

        public GameObject container;

        public LocalPlayer player;

        // public int invTracker;

        public void Activate() {

            gameObject.SetActive(true);
            inventory = player.inventory;
            //invTracker = inventory.items.Count();

            //Debug.Log( inventory == null );
            buttons = new List<UiItemButton>();
        }

        public void Update() {
            inventory = player.inventory;
            if (inventory != null) {
                for (int i = 0; i < buttons.Count; i++) {
                    Destroy(buttons[i].gameObject);
                }

                buttons = new List<UiItemButton>();
                Debug.Log("Start INV print Loop ");
                foreach (KeyValuePair<Item, int> entry in inventory.items) {
                    UiItemButton button = Instantiate(baseButton, container.transform);
                    button.Init(entry.Key, entry.Value);
                    buttons.Add(button);
                    Debug.Log("End INV print Loop ");
                }

            }
        }

        public void GetLocalPlayer(LocalPlayer P) {

            player = P;
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