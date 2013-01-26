using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Splatter : MonoBehaviour {
	
	/// <summary>
	/// The splat prefab.
	/// </summary>
	public GameObject splatPrefab;
	
	void OnCollisionEnter(Collision collision)
	{
		if (!(collision is Heart)){
			Debug.Log("POW! Right in the kisser!");
			Instantiate(splatPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
