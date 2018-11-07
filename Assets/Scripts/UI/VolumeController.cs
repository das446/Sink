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
	public AudioSource sfx1;
	public AudioSource sfx2;
	public Slider volumeSlider;
	public Slider sfxSlider;
	public Slider masterSlider;
	public Slider musicSlider;
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
		//all footstep volumes are equal in volume because it wouldn't make sense to have different volumes for different footsteps
		//if moving at a constant/set speed

		footstep1.volume = audioVolume;
		footstep2.volume = audioVolume;
		footstep3.volume = audioVolume;
		footstep4.volume = audioVolume;
	}

	public void PlayerSoundVolume(float playerSoundVol)
	{
		//audioVolume = playerSoundVol;
		footstep1.volume = volumeSlider.value;
		footstep2.volume = volumeSlider.value;
		footstep3.volume = volumeSlider.value;
		footstep4.volume = volumeSlider.value;	
	}

	public void MasterVolume(float masterVol)
	{
		//masterVol controls the volume for all music and sound effects in the game
		
		float newMasterValue = AudioListener.volume;
		masterVol = newMasterValue;
		AudioListener.volume = masterVol;
	}

	public void MusicVolume(float musicVol)
	{

	}

	public void SFXVolume(float sfxVol)
	{
		sfx1.volume = sfxSlider.value;
		sfx2.volume = sfxSlider.value;

	}
}