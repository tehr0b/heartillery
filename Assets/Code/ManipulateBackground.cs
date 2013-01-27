using UnityEngine;
using System.Collections;

public class ManipulateBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position =  new Vector3(Camera.main.transform.position.x * .75f, transform.position.y, transform.position.z);

	}
}
