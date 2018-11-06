using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Implement open and close, override open menu
//

namespace Sink {
    public class ItemMenu : MonoBehaviour, IMenu {

        public List<Sprite> images;

        public GameObject canvasGrid;
        public GameObject itemimage_reference;
        public Inventory inventory;
        private List<Item> items;

        public UiItemButton baseButton;

        public List<UiItemButton> buttons;

        public GameObject container;

        void Start() { }
        public void Open(LocalPlayer p) {
            gameObject.SetActive(true);
            inventory = p.inventory;
            foreach (KeyValuePair<Item, int> entry in inventory.items) {
                UiItemButton button = Instantiate(baseButton, container.transform);
                button.Init(entry.Key, entry.Value);
                buttons.Add(button);
            }

            //Get items from player
            //You have a grid
            //put the sprites into a grid
        }
        public void Close(LocalPlayer p) {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i].gameObject);
            }
            buttons = new List<UiItemButton>();
            gameObject.SetActive(false);
        }

    }
}