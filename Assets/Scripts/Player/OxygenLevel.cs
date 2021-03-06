using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	[System.Serializable]
	public class OxygenLevel {

		public float curOx;
		public static float min=0;
		public static float max=100; 
		public OxygenBar bar; 

		public OxygenLevel() {
			curOx = 100;
		}

		public void Adjust(int amnt){
			curOx += amnt;
			bar?.update();
		}

		public float percent(){
			return curOx/max;
		}

		public void setToMax(){
			curOx=max;
			bar?.update();
		}

		public void setToMin(){
			curOx=min;
			bar?.update();
		}
	}
}