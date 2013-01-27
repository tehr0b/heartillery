using UnityEngine;
using System.Collections;

public class WinBehavior : MonoBehaviour {

    public const string HEART_TAG = "Player";
    public const string GUI_NAME = "GUI Text";

    private GameObject _gui;

	// Use this for initialization
	void Start () {
        _gui = GameObject.Find(GUI_NAME);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("You hit the win thing!");
        if (other.tag == HEART_TAG)
        {
            _gui.GetComponent<MakeText>().message = "You Win!";
			Heart heart = other.GetComponent<Heart>();
			heart.Splat(100);
			heart.gameObject.SetActive(false);
			(FindObjectOfType(typeof(UITimer)) as UITimer).running = false;
        }
    }

}
