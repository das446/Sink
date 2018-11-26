using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sink;

public class PlayerSpeedController : Player {
	
	//idk if inheritence is needed, but better to be safe than sorry

	public float moveSpeed = 2;
	public Slider speedSlider;


	public void ChangePlayerSpeed(float newSpeed)
	{
		moveSpeed = newSpeed;
		newSpeed = speedSlider.value;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
