using UnityEngine;
using System.Collections;

public class MoreTextButton : MonoBehaviour {
    public TutorialPage2Text labelChanger;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void MoreTextPlease()
    {
        labelChanger.setTextPage2();
    }

    public void OnClick()
    {
        MoreTextPlease();
    }
}
