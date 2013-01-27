using UnityEngine;
using System.Collections;

public class BackToMenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadMenuScene()
    {
        Application.LoadLevel("MainMenu");
    }

    public void OnClick()
    {
        LoadMenuScene();
    }
}
