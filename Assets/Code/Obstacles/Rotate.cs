using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	//How fast it is
	public float speed = 2f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(0,0,speed);
	}
	
}
