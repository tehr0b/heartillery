using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadLevSelScene()
    {
        Application.LoadLevel("LevelMenu");
    }

    public void OnClick()
    {
        LoadLevSelScene();
    }
}
