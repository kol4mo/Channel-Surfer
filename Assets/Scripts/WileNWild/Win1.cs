using TMPro;
using UnityEngine;

public class Win1 : MonoBehaviour
{
    [SerializeField]int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision) {
        LevelManager.Instance.setCurrentLevel(level);
        Loader.Instance.Load(Loader.scenes.LevelSelect);
	}
}
