using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI[] levelText;
	[SerializeField] Button[] levelButtons;
	[SerializeField] string[] levels;
	public static LevelManager instance { private set; get; }
	private int levelIndex = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		if (instance == null) {
			instance = this;
		} else {
			levelIndex = instance.levelIndex;
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void setCurrentLevel(int level) {
		levelIndex = level;
	}

	private void OnLevelWasLoaded(int level) {
		levelIndex = instance.levelIndex;
		for (int i = 0; i <= levelIndex; i++) {
			levelText[i].text = levels[i];
			levelButtons[i].enabled = true;
		}
	}

}
