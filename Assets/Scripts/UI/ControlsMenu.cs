using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	public class ControlsMenu : IMenu {

		LocalPlayer player;
		Room room;
        Toggle invertX;
        Toggle invertY;

		public Slider mouseSensitivitySlider;
		public Text mouseSensitivityText;

		public override void Open(LocalPlayer p) {
			base.Open(p);
			Debug.Log(p);
			player = p;
			room = p.curRoom; //Doesn't work right now for some reason
		}

		public void ChangeSomething() {
			//player.idk
		}
/*
        public void ChangeMouseSensitivity()
        {
            ChangeSensitvity();
            Debug.Log(player);
            //Might need to add more code

        }
		
		public void InvertX()
		{
			Debug.Log(player);
			bool isToggled = false;
		}
		
		public void InvertY()
		{
			Debug.Log(player);
			bool isToggled = false;
			
		}
*/
    }
}

        /*
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
    */