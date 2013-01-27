using UnityEngine;
using System.Collections;

[RequireComponent (typeof(DeathCondition))]
public class Heart : MonoBehaviour {

    int trackedTouchId = ArTouch.NULL_ID;
	//public bool isThrown = false;
    private bool canTap = false;
	private bool canBeat = true;
	public Vector3 beatSpeed = new Vector3(2,3,0);
	
    private Rigidbody _heartRigidBody;

    private bool _isThrown = false;

    public bool isThrown
    {
        get { return _isThrown; }
        private set { }
    }
	
	private RigidbodyConstraints constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
		RigidbodyConstraints.FreezeRotationY;

    const float TAP_TIMER = 0.4f;
    const float BEAT_TIMER = 0.1f;

    private const string SPIKE_TAG = "Spike";
    private const string JUNK_TAG = "Slower";
	
	/// <summary>
	/// The splatter prefab.
	/// </summary>
	public Rigidbody splatterPrefab;
	public float splatForce = 5f;
	
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

        if (!GetComponent<DeathCondition>().chargeDefibrilator)
        {
            rigidbody.AddForce(beatSpeed.normalized * splatForce);
        }

	}

    public void Beat(int charge)
    {
        Vector3 value = beatSpeed * charge;
        if (value.magnitude > 20)
        {
            value.Normalize();
            value *= 20;
        }
        rigidbody.velocity += value;
            
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
        if (GetComponent<DeathCondition>().chargeDefibrilator)
        {
            GetComponent<DeathCondition>().defibrilatorClicks += 1;
            Debug.Log("Get Clicks: " + GetComponent<DeathCondition>().defibrilatorClicks);
        }

		else if (canBeat)
		{
        	rigidbody.velocity+=beatSpeed;
			canBeat = false;
        	if (canTap)
        	{
           		 Debug.Log("Nice Timing!");
				 rigidbody.AddForce(beatSpeed.normalized * splatForce);
				 StartCoroutine(BeatTimer(BEAT_TIMER));
        	}
		}
    }

    void OnCollisionEnter(Collision collision)
    {
        canTap = true;
        StartCoroutine(TapTimer(TAP_TIMER));
		Splat();
        canBeat = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == SPIKE_TAG)
        {
            _heartRigidBody.velocity *= 0.0f;
            _heartRigidBody.useGravity = false;
        }

        else if (other.tag == JUNK_TAG)
        {
            _heartRigidBody.velocity *= .25f;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == SPIKE_TAG)
        {
            _heartRigidBody.useGravity = true;
        }
    }

	void Splat()
	{
		Rigidbody temp = (Rigidbody) Instantiate(splatterPrefab, transform.position, Quaternion.identity);
		Vector3 dir = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1,  0);
		temp.AddForce(dir.normalized * splatForce);
	}

}
