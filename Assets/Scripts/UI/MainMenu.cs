using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sink {
	public class MainMenu : IMenu {

		public bool isStart;
		public bool isQuit;

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

		public void LoadLevel(bool start) {
			if (start) {
				SceneManager.LoadScene(1);
			} else {
				Application.Quit();
			}
		}

		public void Close(LocalPlayer p) {
			//gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) {
			//gameObject.SetActive(true);
		}
	}
}