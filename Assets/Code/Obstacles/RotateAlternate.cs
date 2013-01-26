using UnityEngine;
using System.Collections;

public class RotateAlternate : MonoBehaviour {
	
	//How fast it is
	public float speed = 2f;
	
	//How frequently it changes direction
	public float interval = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Do");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(0,0,speed);
	}
	
	//Timer coroutine for alternating movement
    IEnumerator Do() {
        yield return new WaitForSeconds(interval);
		speed*=-1;
		StartCoroutine("Do");
    }
}
