using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sink.Audio;

namespace Sink {

	public class DoorLock : Interactable {

		public Door door;
		public float lockedTime;
		public Item item;
		public int amnt = 1;
		public AudioSource audioSource;
		public AudioClip lockSound;
		public Light stateLight;

		public override void DoAction(Player p) {
			if (door.locked || p.role != Player.Role.Saboteur) {
				return;
			}
			door.locked = true;
			stateLight.color = Color.red;
			PlaySoundLocalOnly("LockDoor",p);
			this.DoAfterTime(() => {
				door.locked = false;
				stateLight.color = Color.green;
			}, lockedTime);
		}
	}
}