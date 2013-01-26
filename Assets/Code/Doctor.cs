using UnityEngine;
using System.Collections;

public class Doctor : MonoBehaviour {
	
	public Heart heart;
	
	public Camera camera;
	
	public float throwForce = 10f;
	
	bool touchStarted = false;
	
	// Use this for initialization
	void Start () {
		camera = FindObjectOfType(typeof(Camera)) as Camera;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!touchStarted && Input.GetMouseButtonDown(0)){
			//touchStarted = true;
			Vector3 point = new Vector3(
				(Input.mousePosition.x - (Screen.width/2)) / Screen.width,
				(Input.mousePosition.y - (Screen.height/2)) / Screen.height,
				0);
			//Vector3 point = (Input.GetMouseButton(0) ? Input.mousePosition : new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
			ThrowAt(point);
			Debug.Log(point);
		}
	}
	
	void ThrowAt(Vector3 point)
	{
		heart.Throw();
		heart.rigidbody.AddForce(point.normalized * throwForce);
	}
}
