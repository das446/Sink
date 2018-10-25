using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

	//Menu States
	public enum MenuStates {Main, Pause, Options, Parameters, Sound, Controls}
	public MenuStates currentState;
	public Transform canvas;

	public GameObject mainMenu;
	public GameObject pauseMenu;
	public GameObject optionsMenu;
	public GameObject paramsMenu;
	public GameObject soundMenu;
	public GameObject controlsMenu;

	void Awake()
	{
		currentState = MenuStates.Pause;
	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			OnPause();

		}


		switch(currentState)
		{
			case MenuStates.Main:
				mainMenu.SetActive(true);
				pauseMenu.SetActive(false);
				optionsMenu.SetActive(false);
				paramsMenu.SetActive(false);
				soundMenu.SetActive(false);
				controlsMenu.SetActive(false);
				break;

			case MenuStates.Pause:
				mainMenu.SetActive(false);
				pauseMenu.SetActive(true);
				optionsMenu.SetActive(false);
				paramsMenu.SetActive(false);
				soundMenu.SetActive(false);
				controlsMenu.SetActive(false);
				break;

			case MenuStates.Options:
				mainMenu.SetActive(false);
				pauseMenu.SetActive(false);
				optionsMenu.SetActive(true);
				paramsMenu.SetActive(false);
				soundMenu.SetActive(false);
				controlsMenu.SetActive(false);	
				break;			

			case MenuStates.Parameters:
				mainMenu.SetActive(false);
				pauseMenu.SetActive(false);
				optionsMenu.SetActive(false);
				paramsMenu.SetActive(true);
				soundMenu.SetActive(false);
				controlsMenu.SetActive(false);
				break;

			case MenuStates.Sound:
				mainMenu.SetActive(false);
				pauseMenu.SetActive(false);
				optionsMenu.SetActive(false);
				paramsMenu.SetActive(false);
				soundMenu.SetActive(true);
				controlsMenu.SetActive(false);
				break;

			case MenuStates.Controls:
				mainMenu.SetActive(false);
				pauseMenu.SetActive(false);
				optionsMenu.SetActive(false);
				paramsMenu.SetActive(false);
				soundMenu.SetActive(false);
				controlsMenu.SetActive(true);
				break;

		}


	}

	public void OnMain()
	{
		Debug.Log("You are on the Main Menu");
		currentState = MenuStates.Main;
	}

	public void OnOptions()
	{
		Debug.Log("You clicked on Options");
		currentState = MenuStates.Options;
	}

	public void OnPause()
	{

		Debug.Log("You have paused the game");

			if(canvas.gameObject.activeInHierarchy == false)
			{
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				currentState = MenuStates.Pause;
				//player.GetComponent<FirstPersonController>().enabled = false;
			}
			else
			{
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
				//player.GetComponent<FirstPersonController>().enabled = true;
			}
	}

	public void OnParameters()
	{
		Debug.Log("You clicked on Parameters");
		currentState = MenuStates.Parameters;
	
	}

	public void OnSound()
	{
		Debug.Log("You clicked on Sound");
		currentState = MenuStates.Sound;
	}

	public void onControls()
	{
		Debug.Log("You clicked on Controls");
		currentState = MenuStates.Controls;
	}
}
