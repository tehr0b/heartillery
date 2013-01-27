using UnityEngine;
using System.Collections;

public class SplatTextKilla : MonoBehaviour {
    public float duration = 3.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitThenSuicide());
	}

    IEnumerator WaitThenSuicide()
    {
        yield return new WaitForSeconds(duration);
        Object.Destroy(gameObject);
    }
}
