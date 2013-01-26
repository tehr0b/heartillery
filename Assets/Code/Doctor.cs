using UnityEngine;
using System.Collections;

public class Doctor : MonoBehaviour {
	
	public Heart heart;
	
	
	
	public Camera camera;
	
	public float throwForce = 15f;
	
	bool hasThrown = false;
	
	// Use this for initialization
	void Start () 
	{
		camera = FindObjectOfType(typeof(Camera)) as Camera;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!hasThrown && Input.GetMouseButtonDown(0)){
			hasThrown = true;
			Vector3 point = new Vector3(
				(Input.mousePosition.x - (Screen.width/2)) / Screen.width,
				(Input.mousePosition.y - (Screen.height/2)) / Screen.height,
				0);
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
