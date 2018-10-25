using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class OxygenBar : MonoBehaviour {

		public Image image;
		public Text text;
		public OxygenLevel oxygen;

		public void Start(){
			update();
		}

		public void ChangeOxygen(OxygenLevel o){
			oxygen = o;
			o.bar = this;
			update();
		}

		public void update(){
			float fill = oxygen.percent();
			image.fillAmount = fill;
			text.text = fill*100 + "%";
		}
	}
}