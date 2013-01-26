using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

    int trackedTouchId = ArTouch.NULL_ID;
	public bool isThrown = false;
    private bool canTap = false;
	private bool canBeat = true;
	public Vector3 beatSpeed = new Vector3(1,3,0);
	
    private Rigidbody _heartRigidBody;

    private bool _isThrown = false;
	
	private RigidbodyConstraints constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
		RigidbodyConstraints.FreezeRotationY;

    const float TAP_TIMER = 0.4f;
    const float BEAT_TIMER = 0.6f;
	
	/// <summary>
	/// The splatter prefab.
	/// </summary>
	public Rigidbody splatterPrefab;
	public float splatForce = 20f;
	
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
		if (canBeat&&(isThrown && ((Input.touchCount > 0)||(Input.GetMouseButtonDown(0))))){
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
        rigidbody.velocity+=beatSpeed;
	}

    IEnumerator TapTimer(float timeLeft){
        yield return new WaitForSeconds(timeLeft);
        canTap = false;
    }
	
	IEnumerator BeatTimer(float timeLeft){
        yield return new WaitForSeconds(timeLeft);
        canBeat = true;
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
		if (canBeat)
		{
        	rigidbody.velocity+=beatSpeed;
        	if (canTap)
        	{
           		 Debug.Log("Nice Timing!");
           		 rigidbody.velocity *= 1.2f;
        	}
			canBeat = false;
			StartCoroutine(BeatTimer(BEAT_TIMER));
		}
    }

    void OnCollisionEnter(Collision collision)
    {
        canTap = true;
        StartCoroutine(TapTimer(TAP_TIMER));
		Splat();
    }
	
	void Splat()
	{
		Rigidbody temp = (Rigidbody) Instantiate(splatterPrefab, transform.position, Quaternion.identity);
		Vector3 dir = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1,  0);
		temp.AddForce(Vector3.zero);
	}

}
