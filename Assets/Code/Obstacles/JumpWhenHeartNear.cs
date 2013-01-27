using UnityEngine;
using System.Collections;

public class JumpWhenHeartNear : MonoBehaviour {
	
	//follows the heart
	public Heart heart;
	
	//How fast it is
	public float speed = 0.01f;
	
	//How frequently it changes direction
	public float interval = 0.4f;
	
	//Welcome to the JAM!
	private bool NBAJAMZ2013;
	
	public int triggerdist = 2;
	
	// Use this for initialization
	void Start () {
		heart = FindObjectOfType(typeof(Heart)) as Heart;
	}
	
	// Update is called once per frame
	void Update () {
		if (NBAJAMZ2013)
		{
			gameObject.transform.localPosition+= new Vector3(0,speed,0);
		}else if (Mathf.Abs(gameObject.transform.localPosition.x - heart.transform.localPosition.x)<triggerdist)
		{
			speed = 0.01f;
			StartCoroutine(GoUp());
			NBAJAMZ2013 = true;
		}
	}
	
	//Timer coroutine for alternating movement
    IEnumerator GoUp() {
        yield return new WaitForSeconds(interval);
		speed = -0.01f;
		StartCoroutine(GoDown());
    }
	//Timer coroutine for alternating movement
    IEnumerator GoDown() {
        yield return new WaitForSeconds(interval);
		NBAJAMZ2013 = false;
    }
}
