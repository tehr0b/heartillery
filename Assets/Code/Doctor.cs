using UnityEngine;
using System.Collections;

public class Doctor : MonoBehaviour {
	
    int trackedTouchId = ArTouch.NULL_ID;
	tk2dAnimatedSprite anim;
	public Heart heart;
	
	public Camera camera;
	
	public float throwForce = 150f;
	
	bool hasThrown = false;
	
    void OnEnable()
    {
        ArTouchInput.OnTap += TouchTap;
    }

    void OnDisable()
    {
        ArTouchInput.OnTap -= TouchTap;
    }
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<tk2dAnimatedSprite>();
		camera = FindObjectOfType(typeof(Camera)) as Camera;
		heart = FindObjectOfType(typeof(Heart)) as Heart;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!hasThrown)
		{
			anim.Play("Pretoss");
		}else{
			anim.Play("Aftertoss");
		}
		if (!hasThrown && ((Input.touchCount > 0)||(Input.GetMouseButtonDown(0)))){
			hasThrown = true;
			Vector3 point = new Vector3(
				(Input.mousePosition.x - (Screen.width/2)) / Screen.width,
				(Input.mousePosition.y - (Screen.height/2)) / Screen.height,
				0);
			ThrowAt(point);
			Debug.Log(point);
			(FindObjectOfType(typeof(UITimer)) as UITimer).running = true;
		}
	}
	
	// Following part sets tapped bool on collison
    // with wall. If tap is registered within .2 seconds
    // of collision, amplify speed. This feels better to me.
    void TouchTap(ref ArTouch touch)
    {
		if (!hasThrown && Input.touchCount > 0){
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
