using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Ladder : Interactable {

		public Room upperRoom, lowerRoom;
		public Floor upperFloor, lowerFloor;
		public Transform top, bottom;
		
		//Temporary rng system
		public int rng;
		public GameObject smoke;
		public GameObject pipe;

		void Start()
		{
			rng = Random.Range(0,4);
		}

		public override void DoAction(Player p)
		 {
			if (p.curFloor == lowerFloor && rng == 0) 
			{
				p.StartCoroutine(p.ClimbLadder(this, upperRoom, upperFloor));
				Debug.Log("Smoke activated. RNG = " + rng);
				smoke.gameObject.SetActive(true);
			}
			 else if(p.curFloor == lowerFloor && (rng == 1 || rng == 2 || rng == 3))
			 {
				p.StartCoroutine(p.ClimbLadder(this, upperRoom, upperFloor));
				Debug.Log("Smoke not activated. RNG = " + rng);
				smoke.gameObject.SetActive(false);				 
			 }
			 else if(p.curFloor == upperFloor && rng == 0)
			 {
				p.StartCoroutine(p.ClimbLadder(this, lowerRoom, lowerFloor));
				Debug.Log("Smoke activated. RNG = " + rng);
				smoke.gameObject.SetActive(true);
			 }
			 else if(p.curFloor == upperFloor && (rng == 1 || rng == 2 || rng == 3)) 
			 {
				p.StartCoroutine(p.ClimbLadder(this, lowerRoom, lowerFloor));
				Debug.Log("Smoke not activated. RNG = " + rng);
				smoke.gameObject.SetActive(false);
			}
			
		}
	}
}