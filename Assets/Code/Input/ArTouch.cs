using UnityEngine;

// Asterogue's class representing a touch on the screen
// Class not struct so that it's passed by reference, not value
public class ArTouch
{
	/// <summary>
	/// The touch id used for ArTouches spawned by the mouse
	/// </summary>
	public const int MOUSE_ID = 0;

	/// <summary>
	/// The touch id for null ArTouches
	/// </summary>
	public const int NULL_ID = -1;

	/// <summary>
	/// A unique identifier to track this ArTouch over multiple callbacks
	/// In Unity, this tends to be bounded to the number of fingers on the
	/// screen at once, with 1 assigned to the first finger placed down,
	/// 2 to the next, etc.
	/// </summary>
	public int id;

	/// <summary>
	/// The ArTouch's current screen coordinates on the game window.
	/// Unity's screen space has (0,0) at the bottom-left corner.
	/// </summary>
	public Vector2 position;

	/// <summary>
	/// How far the ArTouch has moved in screen coordinates since the last update
	/// </summary>
	public Vector2 deltaPosition;

	/// <summary>
	/// The screen coordinates that the ArTouch began at
	/// </summary>
	public Vector2 startPosition;

	/// <summary>
	/// The system time when this ArTouch began
	/// </summary>
	public float startTime;

	/// <summary>
	/// Whether the ArTouch has moved far enough from its startPosition to
	/// throw movement events instead of stationary events.
	/// This is set by ArTouchInput and probably shouldn't be messed with.
	/// </summary>
	public bool hasMoved;
	
	/// <summary>
	/// Stops this ArTouch from throwing events
	/// </summary>
	public bool isDead;

	/// <summary>
	/// Initializes a new instance of the <see cref="ArTouch"/> class.
	/// </summary>
	/// <param name='id'>
	/// Touch identifier.
	/// </param>
	/// <param name='pos'>
	/// Screen position.
	/// </param>
	public ArTouch (int id, Vector2 pos)
	{
		this.id = id;
		position = pos;
		deltaPosition = Vector2.zero;
		startPosition = pos;
		startTime = Time.time;
		hasMoved = false;
		isDead = false;
	}
	
	/// <summary>
	/// Gets the age of the touch (how long it has
	/// been held to the screen) in seconds.
	/// </summary>
	/// <returns>
	/// The age.
	/// </returns>
	public float GetAge()
	{
		return (Time.time - startTime);	
	}
}