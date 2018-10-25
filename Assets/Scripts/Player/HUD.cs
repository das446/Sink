using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class HUD : MonoBehaviour {

		public OxygenBar oxygenBar;
		public TemperatureBar temperatureBar;
		public Text RoomName;

		public Text role;

		public float RoomNameFadeInRate;
		public float RoomNameFadeOutRate;

		public MenuController Menu;

		public IEnumerator FadeRoomName(Room room)
        {
			RoomName.text = room.Name;
            Color c = RoomName.color;
			c.a=0;
			RoomName.color=c;
			while(c.a<1){
				c.a += RoomNameFadeInRate;
				RoomName.color=c;
				yield return new WaitForSeconds(0.01f);
			}
			while(c.a>0){
				c.a -= RoomNameFadeOutRate;
				RoomName.color=c;
				yield return new WaitForSeconds(0.01f);
			}
        }
	}
}