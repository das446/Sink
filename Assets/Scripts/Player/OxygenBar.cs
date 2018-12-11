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
			if(text.text == "10")
			{
				this.PlaySound("OxygenRepair");
			}
			else if(text.text == "0")
			{
				this.PlaySound("OxygenRepair2");
			}
		}
	}
}