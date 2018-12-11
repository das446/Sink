using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class MapMenu : IMenu {
		public Image mapImage;

		public Sprite[] floorImages;

		public GameObject marker;

		public override void Open(LocalPlayer player) {
			base.Open(player);
			int i = player.curFloor.number;
			mapImage.sprite = floorImages[i];
			Room r = player.curRoom;
			if (r.markerPosition != null) {
				marker.transform.position = r.markerPosition.transform.position;
			}
		}

	}

}