using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

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
}