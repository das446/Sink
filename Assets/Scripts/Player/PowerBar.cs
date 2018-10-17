using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	public class PowerBar : MonoBehaviour {

		public Image image;
		public Text text;
		public ElecPower power;

		public void Start() {
			update();
		}

		public void ChangePower(ElecPower e) {
			power = e;
			e.bar = this;
			update();
		}

		public void update() {
			float fill = power.percent();
			image.fillAmount = fill;
			text.text = fill * 100 + "%";
		}
	}
}