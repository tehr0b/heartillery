using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	
	private bool _isThrown = false;
	
	private Rigidbody body;
	
	private RigidbodyConstraints constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
		RigidbodyConstraints.FreezeRotationY;
	
	
	// Use this for initialization
	void Start () {
		body = rigidbody;
		body.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_isThrown && Input.touchCount > 0){
			Beat();	
		}
	}
	
	public void Throw()
	{
		_isThrown = true;
		body.constraints = constraints;
	}
	
	void Beat()
	{
		
	}
}
