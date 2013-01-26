using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

    int trackedTouchId = ArTouch.NULL_ID;
	public bool isThrown = false;
    private bool canTap = false;

    private Rigidbody _heartRigidBody;

    private bool _isThrown = false;
	
	private RigidbodyConstraints constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
		RigidbodyConstraints.FreezeRotationY;

    const float TAP_TIMER = 0.20f;

    void OnEnable()
    {
        ArTouchInput.OnTap += TouchTap;
    }

    void OnDisable()
    {
        ArTouchInput.OnTap -= TouchTap;
    }

	// Use this for initialization
	void Start () {
        _heartRigidBody = rigidbody;
        _heartRigidBody.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
        ArTouchInput.GetInstance().Update();	

		if (isThrown && Input.touchCount > 0){
			Beat();	
		}

	}
	
	public void Throw()
	{
		_isThrown = true;
		_heartRigidBody.constraints = constraints;
	}
	
	void Beat()
	{
	}

    IEnumerator TapTimer(float timeLeft){
        yield return new WaitForSeconds(timeLeft);
        canTap = false;
    }

    // This logic starts coroutine on tap and if
    // heart hits wall within .2 seconds, amplify speed.
/*    void TouchTap(ref ArTouch touch){
        beenTapped = true;
        StartCoroutine(TapTimer(TAP_TIMER));
    }

    void OnCollisionEnter(Collision collision){
        if (beenTapped)
        {
            Debug.Log("Nice Timing!");
            _heartRigidBody.velocity *= 1.5f;
        }
    }*/


    // Following part sets tapped bool on collison
    // with wall. If tap is registered within .2 seconds
    // of collision, amplify speed. This feels better to me.
    void TouchTap(ref ArTouch touch)
    {
        if (canTap)
        {
            Debug.Log("Nice Timing!");
            _heartRigidBody.velocity *= 1.5f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        canTap = true;
        StartCoroutine(TapTimer(TAP_TIMER));
    }

}
