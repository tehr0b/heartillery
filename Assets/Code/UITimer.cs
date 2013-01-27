using UnityEngine;
using System.Collections;

public class UITimer : MonoBehaviour {
	
	public float time;
	public GUIText gui;
	public bool running;
	
	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (running)
		{
			time += Time.deltaTime;
		}
		gui.text = time.ToString();
	}
}
