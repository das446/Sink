using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	[System.Serializable]
	public class Temperature {


		public float curTemp;
		public static float min=0;
		public static float max=100; 
		public TemperatureBar bar; 

		public Temperature() {
			curTemp = 60;

		}

		public void Adjust(int amnt){
			curTemp += amnt;
			bar.update();
		}

		public float percent(){
			return curTemp/max;
		}
	}
}