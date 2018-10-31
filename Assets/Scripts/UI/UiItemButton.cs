using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class UiItemButton : MonoBehaviour {

		public Item item;
		public int amnt;
		public Image image;
		public Text amntText;
		public void Init(Item item,int amnt){
			this.item = item;
			this.amnt = amnt;
			image.sprite = item.uiImage;
			amntText.text = amnt+"";
		}
	}
}