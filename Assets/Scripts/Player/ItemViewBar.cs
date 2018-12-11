using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Implement open and close, override open menu
//

namespace Sink {
    public class ItemViewBar : MonoBehaviour {
        Inventory inventory;

        public UiItemButton baseButton;

        List<UiItemButton> buttons;

        public GameObject container;


        // public int invTracker;

        public void Activate() {

            gameObject.SetActive(true);
            //invTracker = inventory.items.Count();

            //Debug.Log( inventory == null );
            buttons = new List<UiItemButton>();
            Inventory.InventoryChanged += UpdateBar;
            Debug.Log("Activate bottom bar");
        }

        public void UpdateBar() {
            inventory = LocalPlayer.singleton.inventory;
            if (inventory != null) {
                for (int i = 0; i < buttons.Count; i++) {
                    Destroy(buttons[i].gameObject);
                    buttons[i]=null;
                }

                buttons = new List<UiItemButton>();
                Debug.Log("Start INV print Loop ");
                foreach (KeyValuePair<Item, int> entry in inventory.items) {
                    UiItemButton button = Instantiate(baseButton, container.transform);
                    button.Init(entry.Key, entry.Value);
                    buttons.Add(button);
                }

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