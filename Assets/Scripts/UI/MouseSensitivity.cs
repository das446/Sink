using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;


public class MouseSensitivity : MonoBehaviour 
{
	public Slider mouseSensitivity;

	public void ChangeSensitivity()
	{
		GetComponent<FirstPersonController>().ChangeMouseSensitivity(mouseSensitivity.value, mouseSensitivity.value);
		Debug.Log("Mouse sensitivity is currently set at: " + mouseSensitivity.value);
	}
}
