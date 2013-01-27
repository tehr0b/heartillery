using UnityEngine;
using System.Collections;

public class ChestCavity : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Heart heart = other.GetComponent<Heart>();
		if (heart != null)
		{
			Victory(heart);
		}
	}
	
	void Victory(Heart heart)
	{
		heart.Splat(20);
		heart.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
}
