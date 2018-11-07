using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;
public class PauseGame : MonoBehaviour
 {

	public Transform canvas;
	//public Transform player;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	public void Pause()
	{
			if(canvas.gameObject.activeInHierarchy == false)
			{
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				//player.GetComponent<FirstPersonController>().enabled = false;
			}
			else
			{
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
				//player.GetComponent<FirstPersonController>().enabled = true;
			}
	}
	/*
		public void Close(LocalPlayer p) 
		{
			gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) 
		{
			gameObject.SetActive(true);
		}
	*/	
 }