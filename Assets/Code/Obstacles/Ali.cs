using UnityEngine;
using System.Collections;

[RequireComponent (typeof(tk2dAnimatedSprite))]
[RequireComponent (typeof(JumpWhenHeartNear))]
public class Ali : MonoBehaviour {
	
	tk2dAnimatedSprite sprite;
	JumpWhenHeartNear jump;
	
	public bool cheer = false;
	
	// Use this for initialization
	void Start () 
	{
		sprite = GetComponent<tk2dAnimatedSprite>();
		jump = GetComponent<JumpWhenHeartNear>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (cheer){
			sprite.Play("Cheering");
		}
		else if (jump.NBAJAMZ2013)
		{
			sprite.Play("Jumping");
		}
		else
		{
			sprite.Play("Standing");
		}
		
	}
	
	public void DoCheer(){
		cheer = true;	
	}
}
