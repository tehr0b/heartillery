using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public GameObject followObj;
	
	public Transform trans;
	public Transform followTrans;
	
	void Start()
	{
		trans = transform;
		followTrans = followObj.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (followObj != null)
		{
			trans.position = new Vector3(followTrans.position.x, followTrans.position.y, -10);
		}
	}
}
