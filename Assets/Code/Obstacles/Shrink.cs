using UnityEngine;
using System.Collections;

public class Shrink : MonoBehaviour {
	
	//How fast it is
	public float speed = 0.025f;
	
	//How frequently it changes direction
	public float interval = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Do");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale+= new Vector3(speed,speed,speed);
	}
	
	//Timer coroutine for alternating movement
    IEnumerator Do() {
        yield return new WaitForSeconds(interval);
		speed*=-1;
		StartCoroutine("Do");
    }
}
