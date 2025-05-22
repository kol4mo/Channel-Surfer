using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance { get; private set; }
	[SerializeField] private SoundsSO sounds;
	private float volume = 1f;
	private const string PLAYER_PREF_SFX_VOLUME = "SFXVOLUME";

	private void Awake() {
		instance = this;
		volume = PlayerPrefs.GetFloat(PLAYER_PREF_SFX_VOLUME, 1f);
	}

	private void Start() {
		PlayerController.OnJump += Player_OnJumpl;
	}

	//play sounds
	private void Player_OnJumpl(object sender, System.EventArgs e) {
		PlayerController player = sender as PlayerController;
		PlaySound(sounds.onjump, player.transform.position, sounds.onJumpVolume);
	}

	//sound manager functions
	private void PlaySound(AudioClip audio, Vector3 position, float VolumeMultiplier = 1f) {
		AudioSource.PlayClipAtPoint(audio, position, VolumeMultiplier * volume);
	}

	public void ChangeVolume() {
		volume += .1f;
		if (volume > 1.05f) {
			volume = 0f;
		}

		PlayerPrefs.SetFloat(PLAYER_PREF_SFX_VOLUME, volume);
		PlayerPrefs.Save();
	}

	public float GetVolume() {
		return volume;
	}

	private void OnDestroy() {

		PlayerController.OnJump -= Player_OnJumpl;
	}
}
