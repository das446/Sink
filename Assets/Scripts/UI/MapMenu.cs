using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class MapMenu : MonoBehaviour, IMenu {
		public void Close() {
			gameObject.SetActive(false);
		}

		public void Open() {
			gameObject.SetActive(true);
		}
		
	}

}