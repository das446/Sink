using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]

public class VolumeController : MonoBehaviour {


	public AudioSource footstep1;
	public AudioSource footstep2;
	public AudioSource footstep3;
	public AudioSource footstep4;
	public Slider volumeSlider;
	public float audioVolume = 1f;

	void Start()
	{
		footstep1 = GetComponent<AudioSource>();
		footstep2 = GetComponent<AudioSource>();
		footstep3 = GetComponent<AudioSource>();
		footstep4 = GetComponent<AudioSource>();		

	}

	void Update()
	{
		footstep1.volume = audioVolume;
		footstep2.volume = audioVolume;
		footstep3.volume = audioVolume;
		footstep4.volume = audioVolume;
	}

	public void PlayerSoundVolume(float playerSoundVol)
	{
		audioVolume = playerSoundVol;
	}

	public void MasterVolume(float masterVol)
	{
		float newMasterValue = AudioListener.volume;
		masterVol = newMasterValue;
		AudioListener.volume = masterVol;
	}

	public void MusicVolume(float musicVol)
	{

	}

	public void SFXVolume(float sfxVol)
	{

	}
}
