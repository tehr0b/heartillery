using UnityEngine;
using System.Collections;

public class TutorialPage2Text : MonoBehaviour {

    public UILabel label;
    public string text2 = "You are that heart! It's your job to make it to the surgery room and into that patient's chest. Tap to launch yourself out the window and on your way. Tap anywhere to throb and give yourself a boost. Throb when you hit the ground for some extra bounce.";
    public string text3 = "Watch out for obstacles, especially the kind that might want to eat you. If you stop moving, you'll die. But you can defibrilate yourself once! Tap as fast as you can to defibrilate and shoot back onto your journey.";
	void Start () {
        label = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setTextPage2()
    {
        label.text = text2;
    }

    public void setTextPage3()
    {
        label.text = text3;
    }
}
