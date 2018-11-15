using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	public GameObject start;
	public GameObject help;

	public void OpenHelp(){
		start.SetActive(false);
		help.SetActive(true);
	}

	public void Back(){
		start.SetActive(true);
		help.SetActive(false);
	}

}
