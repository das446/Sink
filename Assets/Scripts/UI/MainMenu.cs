using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {


	public bool isStart;
	public bool isQuit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
		if(isStart)
		{
			Application.LoadLevel(1);
			GetComponent<Renderer>().material.color = Color.cyan;
			Debug.Log("You have started the game!");
		}

		if(isQuit)
		{
			Application.Quit();
			Debug.Log("You have quit the game!");
		}
	}
}
