using UnityEngine;
using System.Collections;

public class LoadLevelButton : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadLevelScene()
    {
        Application.LoadLevel(levelToLoad);
    }

    public void OnClick()
    {
        LoadLevelScene();
    }
}
