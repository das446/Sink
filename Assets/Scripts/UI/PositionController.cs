using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour {

	public GameObject testObject;

	public void changeXPosition(float newXValue)
	{
		Vector3 xPos = testObject.transform.position;
		xPos.x = newXValue;
		testObject.transform.position = xPos;
		Debug.Log("The X position of the object is at: " + xPos);

	}

	public void changeYPosition(float newYValue)
	{
		Vector3 yPos = testObject.transform.position;
		yPos.y = newYValue;
		testObject.transform.position = yPos;
		Debug.Log("The Y position of the object is at: " + yPos);

	}

	public void changeZPosition(float newZValue)
	{
		Vector3 zPos = testObject.transform.position;
		zPos.z = newZValue;
		testObject.transform.position = zPos;
		Debug.Log("The Z position of the object is at: " + zPos);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
