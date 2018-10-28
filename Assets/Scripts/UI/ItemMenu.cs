using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Implement open and close, override open menu
//

namespace Sink
{
    public class ItemMenu : MonoBehaviour, IMenu
    {

        public List<Image> images;

        public Inventory inventory;
		public List<Item> items;

        void Start()
        {
            inventory = new Inventory();
        }
        public void Open(LocalPlayer p)
        {
            gameObject.SetActive(true);

            inventory = p.inventory; /// Temp show of it working
			items = inventory.items.Keys.ToList();
			/*
			for each item in inventory:
				set the image from images
			 */
			inventory.PrintInv();




            //Get items from player
            //You have a grid
            //put the sprites into a grid
        }
        public void Close(LocalPlayer p)
        {
            gameObject.SetActive(false);
        }


    }
}