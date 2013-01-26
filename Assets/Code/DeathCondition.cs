using UnityEngine;
using System.Collections;

public class DeathCondition : MonoBehaviour {

    private Heart heart;
    private Rigidbody dcRigidbody;
    private Transform transform;

    public float deathTriggerVelocity;
    public float deathTriggerHeight;
    public const int numsplats = 3; //might make modifiable
    public Vector3[] splatvecs;

	// Use this for initialization
	void Start () {
        heart = (Heart)GetComponent<Heart>();
        dcRigidbody = (Rigidbody)GetComponent<Rigidbody>();
        transform = (Transform)GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("con1: " + heart.isThrown + ", con2: " +
        //    (dcRigidbody.velocity.magnitude < deathTriggerVelocity) + ", con3: " +
        //    (transform.position.y < deathTriggerHeight));

	    if (heart.isThrown &&
            dcRigidbody.velocity.magnitude < deathTriggerVelocity &&
            transform.position.y < deathTriggerHeight)
        {
            BeginDeath();
        }
	}

    void BeginDeath()
    {
        CreateSplatter();
        Debug.Log("YOU'VE DIED. GOOD JOB.");
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
