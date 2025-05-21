using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Loader : Singleton<Loader> {
	private string TargetScene;

	[Serializable]
	public enum scenes {
		MainMenu,
		Loading,
		LevelSelect,
		WileNWild
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
