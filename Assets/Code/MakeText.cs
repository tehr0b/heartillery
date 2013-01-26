using UnityEngine;
using System.Collections;

public class MakeText : MonoBehaviour {

    public string message;

	// Use this for initialization
	void Start () {
        message = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        guiText.text = message;
    }

}
