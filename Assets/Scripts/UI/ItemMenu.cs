using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class ItemMenu : MonoBehaviour,IMenu {

		public void Close() {
			gameObject.SetActive(false);
		}

		public void Open() {
			gameObject.SetActive(true);
		}
	}
}