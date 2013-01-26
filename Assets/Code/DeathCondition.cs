using UnityEngine;
using System.Collections;

public class DeathCondition : MonoBehaviour {

    private Heart heart;
    private Rigidbody rigidbody;
    private Transform transform;

    public float deathTriggerVelocity;
    public float deathTriggerHeight;

	// Use this for initialization
	void Start () {
        heart = (Heart)GetComponent<Heart>();
        rigidbody = (Rigidbody)GetComponent<Rigidbody>();
        transform = (Transform)GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("isThrown: " + heart.isThrown);
	    if (heart.isThrown &&
            rigidbody.velocity.magnitude < deathTriggerVelocity &&
            transform.position.y < deathTriggerHeight)
        {
            BeginDeath();
        }
	}

    void BeginDeath()
    {
        Debug.Log("YOU'VE DIED. GOOD JOB.");
        Object.Destroy(gameObject);
    }
}
