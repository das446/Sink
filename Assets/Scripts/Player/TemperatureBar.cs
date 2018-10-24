using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class TemperatureBar : MonoBehaviour {

		public Image image;
		public Text text;
		public Temperature temperature;

		public void Start(){
			temperature = new Temperature();
			update();
		}

		public void ChengeTemperature(Temperature t){
			temperature = t;
			t.bar = this;
			update();
		}

		public void update(){
			float fill = temperature.percent();
			image.fillAmount = fill;
			text.text = temperature.curTemp + "° F";
		}
	}
}