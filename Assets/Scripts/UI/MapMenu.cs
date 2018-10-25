using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class MapMenu : MonoBehaviour, IMenu {
		public void Close(LocalPlayer p) {
			gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) {
			gameObject.SetActive(true);
		}
		
	}

}