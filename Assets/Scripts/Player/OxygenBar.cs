using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sink.Audio;

namespace Sink {
	public class OxygenBar : MonoBehaviour {

		public Image image;
		public Text text;
		public OxygenLevel oxygen;

		public void Start(){
			UpdateFill();
		}

		public void ChangeOxygen(OxygenLevel o){
			oxygen = o;
			o.bar = this;
			UpdateFill();
		}

		public void UpdateFill(){
			float fill = oxygen.percent();
			image.fillAmount = fill;
			text.text = fill*100 + "%";
		}
	}
}