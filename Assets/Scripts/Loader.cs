using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class Loader {
	private static scenes TargetScene;

	public enum scenes {
		MainMenu,
		Loading
	}

	public static UnityEvent<scenes> loadEvent;

	public static void Load(scenes targetScene) {
		Loader.TargetScene = targetScene;

		SceneManager.LoadScene(Loader.scenes.Loading.ToString());
	}

	public static void LoaderCallback() {
		SceneManager.LoadScene(Loader.TargetScene.ToString());
	}
}
