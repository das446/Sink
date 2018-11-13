using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	public class VolumeMenu : IMenu {

		LocalPlayer player;
		Room room;

		public Slider playerSoundSlider;
		public Text playerSoundText;

        public Slider masterSlider;
        public Text masterText;

        public Slider sfxSlider;
        public Text sfxText;

		public Slider musicSlider;
		public Text musicText;

		public override void Open(LocalPlayer p) {
			base.Open(p);
			Debug.Log(p);
			player = p;
			room = p.curRoom; //Doesn't work right now for some reason
		}

		public void ChangeSomething() {
			//player.idk
		}

        public void SetPlayerSoundVolume()
        {
            Debug.Log(player);
            float playerSoundVolume = playerSoundSlider.value;
            playerSoundText.text = playerSoundVolume + "";

        }

        public void SetMasterVolume()
        {
            Debug.Log(player);
            float masterVolume = masterSlider.value;
            masterText.text = masterVolume + "";            

        }

        public void SetSFXVolume()
        {
            Debug.Log(player);
            float sfxVolume = sfxSlider.value;
            sfxText.text = sfxVolume + "";
        }

        public void SetMusicSlider()
        {
            Debug.Log(player);
            float musicVolume = musicSlider.value;
            musicText.text = musicVolume + "";
        }
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