using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;
	private static readonly object _lock = new object();

	public static T Instance {
		get {
			if (_instance == null) {
				lock (_lock) {
					_instance = FindObjectOfType<T>();
					if (_instance == null) {
						Debug.LogError("No instance of " + typeof(T) + " found in the scene.");
					}
				}
			}
			return _instance;
		}
	}

	protected virtual void Awake() {
		if (_instance == null) {
			_instance = this as T;
		} else if (_instance != this) {
			Debug.LogWarning("Duplicate singleton " + typeof(T) + " found, destroying " + gameObject.name);
			Destroy(gameObject);
		}
	}
}
