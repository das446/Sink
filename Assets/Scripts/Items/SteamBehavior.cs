using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink
{
public class SteamBehavior : MonoBehaviour
 {
	public int rng;
	public GameObject smoke;
	public GameObject pipe;
	Room room;
	//bool isOn = true;
   //bool enter = true;
	
	// Use this for initialization
	void Start () 
	{
		rng = Random.Range(0,4);
		smoke.gameObject.SetActive(false);
		//smoke.GetComponent<ParticleSystem>().enableEmission = false;
		//gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnTriggerEnter(Collider c)
	{
        if(c.GetComponent<Player>() && rng == 0)
        {
			Debug.Log("Smoke activated. RNG = " + rng);
            smoke.gameObject.SetActive(true);
        }
		else if (rng == 1 || rng == 2 || rng == 3)
		{
			Debug.Log("Smoke did not activate. RNG = " + rng);
			smoke.gameObject.SetActive(false);
		}
	}
}
}
