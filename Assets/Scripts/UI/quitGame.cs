﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour {


	public void Quit()
	{
		Debug.Log("You have quit the game!");
		Application.Quit();

	}
}	