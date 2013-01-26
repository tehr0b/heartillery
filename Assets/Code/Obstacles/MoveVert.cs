using UnityEngine;
using System.Collections;

public class MoveVert : MonoBehaviour {
	
	//How fast it is
	public float speed = 0.05f;
	
	//How frequently it changes direction
	public float interval = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Do");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localPosition+= new Vector3(0,speed,0);
	}
	
	//Timer coroutine for alternating movement
    IEnumerator Do() {
        yield return new WaitForSeconds(interval);
		speed*=-1;
		StartCoroutine("Do");
    }
}
