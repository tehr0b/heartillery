using UnityEngine;
using System.Collections;

public class DiamondMove : MonoBehaviour {
	
	//How horizontally fast it is
	public float xspeed = 0.025f;
	
	//How vertically fast it is
	public float yspeed = 0.05f;
	
	//What phase the movement is in
	public int phase = 0;
	
	//How frequently it changes direction
	public float interval = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Do");
	}
	
	// Update is called once per frame
	void Update () {
		//Down Left
		if (phase == 0)
		{
			gameObject.transform.localPosition+= new Vector3(-xspeed,-yspeed,0);
		}else if (phase == 1)//Up Left
		{
			gameObject.transform.localPosition+= new Vector3(-xspeed,yspeed,0);
		}else if (phase == 2)//Up Right
		{
			gameObject.transform.localPosition+= new Vector3(xspeed,yspeed,0);
		}else//Down Right
		{
			gameObject.transform.localPosition+= new Vector3(xspeed,-yspeed,0);
		}
	}
	
	//Timer coroutine for alternating movement
    IEnumerator Do() {
        yield return new WaitForSeconds(interval);
		if (phase < 3)
		{
			phase++;
		}else{
			phase = 0;
		}
		StartCoroutine("Do");
    }
}
