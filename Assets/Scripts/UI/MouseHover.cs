using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour {

	Renderer ren;

	// Use this for initialization
	void Start () 
	{
		ren = GetComponent<Renderer>();
		ren.material.color = Color.black;
		Debug.Log("Default Font Color");	
	}
	
	// Update is called once per frame
	void Update ()
	 {
		
	}

	void OnMouseEnter()
	{
		ren.material.color = Color.red;
		Debug.Log("You are hovering over a button");
	}

	void OnMouseExit()
	{
		ren.material.color = Color.black;
		Debug.Log("Color went from red to black");
	}
}
