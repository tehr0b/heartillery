using UnityEngine;
using System.Collections;

public class DeathCondition : MonoBehaviour {

    private Heart heart;
    private Rigidbody dcRigidbody;
    private Transform transform;
    public GameObject _gui;
    private bool zoom = false;
    private bool zoomed = false;
    public bool launched = false;
    private float originalCameraSize;
    private float zoomCameraSize = .2f;
    private const float ZOOM_INCREMENT = .1f;
    private const string GUI_NAME = "GUI Text";

    public int defibrilatorClicks = 0;
    public bool chargeDefibrilator = false;

    public float deathTriggerVelocity;
    public float deathTriggerHeight;
    public float deathTriggerTimer;
    public const int numsplats = 3; //might make modifiable
    public int numLives = 1; // "lives"
    public Vector3[] splatvecs;

	// Use this for initialization
	void Start () {
        heart = (Heart)GetComponent<Heart>();
        dcRigidbody = (Rigidbody)GetComponent<Rigidbody>();
        transform = (Transform)GetComponent<Transform>();
        originalCameraSize = Camera.main.orthographicSize;
        _gui = GameObject.Find(GUI_NAME);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("con1: " + heart.isThrown + ", con2: " +
        //    (dcRigidbody.velocity.magnitude < deathTriggerVelocity) + ", con3: " +
        //    (transform.position.y < deathTriggerHeight));

        if (numLives <= 0 && launched)
        {
            zoom = false;
        }

        if (zoom)
        {
            if (Camera.main.orthographicSize > zoomCameraSize)
            {
                Camera.main.orthographicSize -= ZOOM_INCREMENT;
            }

            if (Camera.main.orthographicSize < .3f) zoomed = true;

        }
        else
        {
            if (Camera.main.orthographicSize < originalCameraSize)
            {              
                Camera.main.orthographicSize += ZOOM_INCREMENT;
            }

            if (Camera.main.orthographicSize >= 1.8f) 
            {
                zoomed = false;
            }

        }

	    if (heart.isThrown && !chargeDefibrilator &&
            heart.rigidbody.velocity.magnitude < deathTriggerVelocity &&
            transform.position.y < deathTriggerHeight)
        {
            zoom = true;
            StartCoroutine(WaitForDeath());

            if (numLives <= 0 && launched && !zoomed)
            {
                _gui.GetComponent<MakeText>().message = "";
                BeginDeath();
            }

        }else if (!chargeDefibrilator){
		zoom = false;
		}
	}

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(deathTriggerTimer);

		if (heart.isThrown && !chargeDefibrilator &&
            heart.rigidbody.velocity.magnitude < deathTriggerVelocity &&
            transform.position.y < deathTriggerHeight)
        {
        	Defibrilate();
		}

    }

    IEnumerator AccumulateClicks()
    {
        yield return new WaitForSeconds(deathTriggerTimer * 2);
		if (numLives <= 0)
        {
			BeginDeath();
    	}else
		{
       	numLives--;
		}
		gameObject.transform.parent = null;
        GetComponent<Heart>().Beat(defibrilatorClicks);
        _gui.GetComponent<MakeText>().message = "";
        zoom = false;
        launched = true;
		StartCoroutine(EndLaunch());
    }
	
	IEnumerator EndLaunch()
	{
        yield return new WaitForSeconds(deathTriggerTimer);
        chargeDefibrilator = false;
		launched = false;
	}
	
    void Defibrilate()
    {
        Debug.Log("NOT TODAY");
		
        chargeDefibrilator = true;
        _gui.GetComponent<MakeText>().message = "NOT TODAY!";

        StartCoroutine(AccumulateClicks());

    }

    void BeginDeath()
    {
        CreateSplatter();
        //Debug.Log("YOU'VE DIED. THIS IS WHY YOU'RE HOMELESS ADAM.");
        Object.Destroy(gameObject);
    }

    void CreateSplatter()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y);
        Quaternion rot = new Quaternion();
        Rigidbody[] splats = new Rigidbody[splatvecs.Length];

        for (int i = 0; i < splats.Length; ++i)
        {
            splats[i] = (Rigidbody)Instantiate(heart.splatterPrefab, pos, rot);
            splats[i].velocity = splatvecs[i];
        }
    }
}
