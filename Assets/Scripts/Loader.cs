using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
	public static Loader Instance;
	private string TargetScene;

	[Serializable]
	public enum scenes {
		MainMenu,
		Loading,
		LevelSelect
	}

	private void Start() {
		Instance = this;
	}

	public void LoadString(string targetScene) {
		Loader.Instance.TargetScene = targetScene;

		SceneManager.LoadScene(Loader.scenes.Loading.ToString());
	}

	public void Load(scenes targetScene) {
		LoadString(targetScene.ToString());
	}

	public void LoaderCallback() {
		SceneManager.LoadScene(Loader.Instance.TargetScene);
	}

	public void Quit() {
		Application.Quit();
	}
}
