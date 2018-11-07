using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink
{
public class OptionsMenu : MonoBehaviour, IMenu 
{

	public OptionsMenu optionsMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.activeSelf);
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