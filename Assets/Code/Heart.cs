using UnityEngine;
using System.Collections;

[RequireComponent (typeof(DeathCondition))]
public class Heart : MonoBehaviour {

    int trackedTouchId = ArTouch.NULL_ID;
	//public bool isThrown = false;
    private bool canTap = false;
	private bool canBeat = true;
	private bool munchMUNCH = false;
	public Vector3 beatSpeed = new Vector3(1,3,0);
	
	public ParticleSystem glassGo;
	
	public float bleedPerSecond = 5f;
	
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
    private const string GLASS_TAG = "Glass";
	//You know what it does to one's heart ;)
	private const int Mattsgorgeoushair = 10;
	
	/// <summary>
	/// The splatter prefab.
	/// </summary>
	public Rigidbody splatterPrefab;
	public float splatForce = 2f;
	public float beatForce = 20f;
	
	public int beatSplats = 20;
	public int collisionSplats = 50;
	
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
		StartCoroutine(Bleed());
	}
	
	// Update is called once per frame
	void Update () {
		if ((canBeat)||(GetComponent<DeathCondition>().chargeDefibrilator))
		{
        ArTouchInput.GetInstance().Update();	
		}
		//if (canBeat&&(isThrown && ((Input.touchCount > 0)||(Input.GetMouseButtonDown(0))))){
		//	Beat();	
		//}
		//Splat ();
	}
	
	public void Throw()
	{
		_isThrown = true;
		_heartRigidBody.constraints = constraints;
	}
	
	void Beat(Vector3 point)
	{

        if (!GetComponent<DeathCondition>().chargeDefibrilator)
        {
		    rigidbody.AddForce(point.normalized * splatForce);
		    Splat(beatSplats);
        }

	}
	
	IEnumerator Bleed()
	{
		Splat();
		Debug.Log("bleeding again in " + (60f/bleedPerSecond));
		yield return new WaitForSeconds(1f / bleedPerSecond);
		StartCoroutine(Bleed());
	}

    public void Beat(int charge)
    {
        Vector3 value = beatSpeed * charge;
        if (value.magnitude > Mattsgorgeoushair)
        {
            value.Normalize();
            value *= Mattsgorgeoushair;
        }
		munchMUNCH = false;
		_heartRigidBody.transform.parent = null;
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
            GetComponent<tk2dAnimatedSprite>().ClipFps += 2;
            Debug.Log("Get Clicks: " + GetComponent<DeathCondition>().defibrilatorClicks);
        }
		else if ((canBeat)&&(!munchMUNCH))
		{
			Vector3 point = new Vector3(
				(Input.mousePosition.x - (Screen.width/2)) / Screen.width,
				(Input.mousePosition.y - (Screen.height/2)) / Screen.height,
				0);
			Beat(point);
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
        canBeat = true;
        canTap = true;
        StartCoroutine(TapTimer(TAP_TIMER));
		Splat(collisionSplats);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == SPIKE_TAG)&&(_heartRigidBody.useGravity)&&(!GetComponent<DeathCondition>().launched))
        {
            _heartRigidBody.velocity *= 0.0f;
            _heartRigidBody.angularVelocity *= 0.0f;
            _heartRigidBody.useGravity = false;
			_heartRigidBody.transform.parent = other.transform;
        	canBeat = false;
			munchMUNCH = true;
        }

        else if (other.tag == JUNK_TAG)
        {
        	 canBeat = false;
            _heartRigidBody.velocity *= .25f;
        }
		else if (other.tag == GLASS_TAG)
		{
			StartCoroutine(Shatter());
		}

    }
	
	IEnumerator Shatter(){
		glassGo.Emit(100);
		yield return new WaitForSeconds(0.1f);
	}
	
    void OnTriggerExit(Collider other)
    {
        if (other.tag == SPIKE_TAG)
        {
            _heartRigidBody.useGravity = true;
			_heartRigidBody.transform.parent = null;
			munchMUNCH = false;
        }
    }

	public void Splat()
	{
		
		Rigidbody temp = (Rigidbody) Instantiate(splatterPrefab, transform.position, Quaternion.identity);
		Vector3 dir = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1,  0);
		temp.AddForce(dir.normalized * (splatForce * Random.value));
		
	}
	
	public void Splat(int num)
	{
		for (int i = 0; i < num; i++) 
		{
			Splat();
		}
	}
	
}
