using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	public GameObject start;
	public GameObject help;
	public GameObject[] helpScreens;
	public int helpIndex;

	public void OpenHelp(){
		start.SetActive(false);
		help.SetActive(true);
	}

	public void Back(){
		start.SetActive(true);
		help.SetActive(false);
	}

	public void NextHelp(){
		helpScreens[helpIndex].SetActive(false);
		helpIndex++;
		if(helpIndex>=helpScreens.Length){
			helpIndex=0;
		}
		helpScreens[helpIndex].SetActive(true);
		
	}

}
