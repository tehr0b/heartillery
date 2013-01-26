using UnityEngine;
using System.Collections;

// A test class that demos how to assign functions to the
// ArTouchInput event delegates, update ArTouchInput,
// and track a touch across multiple Updates by storing its id.

// Use this script by adding it to any GameObject, such as MainCamera
public class TouchTest : MonoBehaviour {
	
	// The id of the ArTouch that is allowed to draw to the screen
	int trackedTouchId = ArTouch.NULL_ID;
	Vector2 drawPosition = Vector2.zero;
	
	// Assign event delegates to ArTouchInput in OnEnable
	void OnEnable ()
	{	
		ArTouchInput.OnTouchDown += TouchDown;
		ArTouchInput.OnTouchMove += TouchMove;
		ArTouchInput.OnTouchUp += TouchUp;
		ArTouchInput.OnPress += TouchPress;
		ArTouchInput.OnDrag += TouchDrag;
		ArTouchInput.OnTap += TouchTap;
		ArTouchInput.OnDoubleTap += TouchDoubleTap;
		ArTouchInput.OnFlick += TouchFlick;
	}
	
	// Remove event delegates from ArTouchInput in OnDisable
	// so that they're not being called when this object isn't active
	void OnDisable ()
	{
		ArTouchInput.OnTouchDown -= TouchDown;
		ArTouchInput.OnTouchMove -= TouchMove;
		ArTouchInput.OnTouchUp -= TouchUp;
		ArTouchInput.OnPress -= TouchPress;
		ArTouchInput.OnDrag -= TouchDrag;
		ArTouchInput.OnTap -= TouchTap;
		ArTouchInput.OnDoubleTap -= TouchDoubleTap;
		ArTouchInput.OnFlick -= TouchFlick;
	}
	
	// Update is called once per frame
	void Update () {
		// Need to call Update on ArTouchInput otherwise no touch controls will happen :(
		ArTouchInput.GetInstance().Update();
	}
	
	// Draw some debug stuff
	void OnGUI() 
	{
		GUI.Label(new Rect(50,50, 100, 100),""+ ArTouchInput.GetInstance().GetNumTouches());
		if (trackedTouchId != ArTouch.NULL_ID)
		{
			// Magic numbers are bad, kids.  This is example code.
			// Also, the camera's screen space (and thus the touch's) has the origin in the bottom-left,
			// but the GUI has it in the top-left, so we've got to do a conversion on the y axis.
			GUI.Box(new Rect(drawPosition.x - 25, Screen.height - (drawPosition.y+25), 50, 50), "Butts!");
		}
	}
	
	#region Touch Callbacks
	void TouchDown(ref ArTouch touch)
	{
		// We're not tracking any touches yet, so may as well track this one
		if (trackedTouchId == ArTouch.NULL_ID)
		{
			trackedTouchId = touch.id;
		}
		ArDebug.Log("Touch " + touch.id + " down at " + touch.position + "!");
	}
	
	void TouchMove(ref ArTouch touch)
	{
		// If this is the tracked touch, update using it
		if (trackedTouchId == touch.id)
		{
			drawPosition = touch.position;
		}
		ArDebug.Log("Touch " + touch.id + " moving at " + touch.position + "!");
	}
	
	void TouchUp(ref ArTouch touch)
	{
		// If this is the tracked touch, release that reference
		if (trackedTouchId == touch.id)
		{
			trackedTouchId = ArTouch.NULL_ID;	
		}
		ArDebug.Log("Touch " + touch.id + " up at " + touch.position + "!");
	}
	
	void TouchPress(ref ArTouch touch)
	{
		ArDebug.Log("Touch " + touch.id + " pressing at " + touch.position + "!");
	}
	
	void TouchDrag(ref ArTouch touch)
	{
		ArDebug.Log("Touch " + touch.id + " dragging at " + touch.position + "!");
	}
	
	void TouchTap(ref ArTouch touch)
	{
		ArDebug.Log("Touch " + touch.id + " tapped at " + touch.position + "!");
	}
	
	void TouchDoubleTap(ref ArTouch touch)
	{
		touch.isDead = true;
		ArDebug.Log("Touch " + touch.id + " double tapped at " + touch.position + "!");
	}
	
	void TouchFlick(ref ArTouch touch)
	{
		ArDebug.Log("Touch " + touch.id + " flicked at " + touch.position + "!");
	}
	#endregion
	
}
