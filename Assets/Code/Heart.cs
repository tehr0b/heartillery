using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	
	public bool isThrown = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isThrown && Input.touchCount > 0){
			Beat();	
		}
	}
	
	void Beat(){
		
	}

    void OnCollisionEnter(Collision collision)
    {
        rigidbody.velocity *= 1.1f;
    }

}
