using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour {

	public GameObject testPlayer;
	//LocalPlayer Player;

	public void changeXPosition(float newXValue)
	{
		Vector3 xPos = testPlayer.transform.position; 
		xPos.x = newXValue;
		testPlayer.transform.position = xPos;
		Debug.Log("The X position of the object is at: " + xPos);

	}

	public void changeYPosition(float newYValue)
	{
		Vector3 yPos = testPlayer.transform.position;
		yPos.y = newYValue;
		testPlayer.transform.position = yPos;
		Debug.Log("The Y position of the object is at: " + yPos);

	}

	public void changeZPosition(float newZValue)
	{
		Vector3 zPos = testPlayer.transform.position;
		zPos.z = newZValue;
		testPlayer.transform.position = zPos;
		Debug.Log("The Z position of the object is at: " + zPos);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}