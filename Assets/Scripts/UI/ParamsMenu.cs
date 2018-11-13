using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	public class ParamsMenu : IMenu {
		
		Room room;

		public Slider playerWalkSpeed;
		public Text playerWalkSpeedText;

		public Slider playerClimbSpeed;
		public Text playerClimbSpeedText;

		public override void Open(LocalPlayer p) {
			base.Open(p);
			Debug.Log(p);
			player = p;
			room = p.curRoom; //Doesn't work right now for some reason
		}

		public void ChangeSomething() {
			//player.idk
		}

		public void SetWalkSpeed() {
			Debug.Log(player);
			float speed = playerWalkSpeed.value;
			player.movement.SetSpeed(speed);
			playerWalkSpeedText.text = speed + "";
		}

		public void SetClimbSpeed() {
			Debug.Log(player);
			float speed = playerClimbSpeed.value;
			player.ClimbLadderSpeed = speed;
			playerClimbSpeedText.text = speed + "";
		}
	}
}