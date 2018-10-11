using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Sink {

	/// <summary>
	/// The non local player
	/// </summary>
	public class NetworkPlayer : Player {

		public Vector3 target;

		public float speed = 10;

		public override void RecieveMove(string s){
			string[] args= s.Split('|');
			float x = float.Parse(args[2], CultureInfo.InvariantCulture.NumberFormat);
			float y = float.Parse(args[3], CultureInfo.InvariantCulture.NumberFormat);
			float z = float.Parse(args[4], CultureInfo.InvariantCulture.NumberFormat);
			Vector3 v = new Vector3(x,y,z);
			target = v;
		}

		void Update()
		{
			transform.LookAt(target);
			transform.position=Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
		}


	}
}