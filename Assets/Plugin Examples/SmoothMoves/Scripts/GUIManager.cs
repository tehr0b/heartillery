using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls some GUI elements
/// </summary>
public class GUIManager : MonoBehaviour 
{
    /// <summary>
    /// Reference to the game manager
    /// </summary>
    public GameManager gameManager;
	
	public Camera guiCamera;
	
    /// <summary>
    /// Reference to the health bar of the chef
    /// </summary>
    public ProgressBar chefHealthBar;

    /// <summary>
    /// Reference to the total score
    /// </summary>
    public Score totalScore;

    /// <summary>
    /// Reference to the total kills
    /// </summary>
    public Score pizzaCountScore;

    /// <summary>
    /// Reference to a sprite that shows how to play
    /// </summary>
    public SmoothMoves.BoneAnimation instructionsAnimation;

    /// <summary>
    /// Reference to the game over animation
    /// </summary>
    public SmoothMoves.BoneAnimation gameOverAnimation;

    /// <summary>
    /// Reference to the new weapon available animation
    /// </summary>
    public SmoothMoves.BoneAnimation newWeaponAvailableAnimation;

    /// <summary>
    /// Whether or not the instructions have gone away
    /// </summary>
    public bool InstructionsDismissed { get; set; }

    /// <summary>
    /// Called once before other scripts
    /// </summary>
    void Awake()
    {
        // initialize that the instructions are visible
        InstructionsDismissed = false;

        // register the user trigger for the game animation
        gameOverAnimation.RegisterUserTriggerDelegate(GameOver_UserTrigger);

        // register the user trigger for the instructions animation
        instructionsAnimation.RegisterUserTriggerDelegate(Instructions_UserTrigger);

        // register the user trigger for the new weapon available animation
        newWeaponAvailableAnimation.RegisterUserTriggerDelegate(NewWeaponAvailable_UserTrigger);

        // hide the game over text
        ShowGameOver(false);

        // show the instructions sprite
        ShowInstructions(true);

        // move the instructions and game over to the screen
        // (we have this offset in the editor so that we can see what we are doing)
        gameOverAnimation.gameObject.transform.localPosition = new Vector3(0, 0, gameOverAnimation.gameObject.transform.localPosition.z);
        instructionsAnimation.gameObject.transform.localPosition = new Vector3(0, 0, instructionsAnimation.gameObject.transform.localPosition.z);
    }

    /// <summary>
    /// User Trigger delegate that is fired from the game over animation
    /// </summary>
    /// <param name="utEvent">Event sent from the animation</param>
    public void GameOver_UserTrigger(SmoothMoves.UserTriggerEvent utEvent)
    {
        // done event was sent
        if (utEvent.tag == "Done")
        {
            gameManager.State = GameManager.STATE.WaitingForInput;
        }
    }

    /// <summary>
    /// User Trigger delegate that is fired from the instructions animation
    /// </summary>
    /// <param name="utEvent">Event sent from the animation</param>
    public void Instructions_UserTrigger(SmoothMoves.UserTriggerEvent utEvent)
    {
        if (utEvent.tag == "Done")
        {
            instructionsAnimation.gameObject.active = false;
            InstructionsDismissed = true;
        }
    }

    /// <summary>
    /// User Trigger delegate that is fired from the new weapon available animation
    /// </summary>
    /// <param name="utEvent">Event sent from the animation</param>
    public void NewWeaponAvailable_UserTrigger(SmoothMoves.UserTriggerEvent utEvent)
    {
        if (utEvent.tag == "Done")
        {
            newWeaponAvailableAnimation.gameObject.active = false;
        }
    }

    /// <summary>
    /// Shows or hides the instructions
    /// </summary>
    /// <param name="show"></param>
    public void ShowInstructions(bool show)
    {
        if (show)
        {
            instructionsAnimation.gameObject.active = true;
        }
        else
        {
            if (instructionsAnimation.gameObject.active)
                instructionsAnimation.Play("Dismiss");
        }
    }

    /// <summary>
    /// Shows or hides the game over text
    /// </summary>
    public void ShowGameOver(bool show)
    {
        if (show)
        {
            gameOverAnimation.gameObject.SetActiveRecursively(true);
            gameOverAnimation.Play("Game_Over");
        }
        else
        {
            gameOverAnimation.gameObject.SetActiveRecursively(false);
        }
    }

    /// <summary>
    /// Animates when a new weapon is available
    /// </summary>
    public void ShowNewWeaponAvailable(bool show)
    {
        if (show)
        {
            newWeaponAvailableAnimation.gameObject.active = true;
            newWeaponAvailableAnimation.Play("Show");
        }
        else
        {
            newWeaponAvailableAnimation.gameObject.active = false;
        }
    }

    /// <summary>
    /// Resets the GUI back to a starting state
    /// </summary>
    public void ResetToStart()
    {
        ShowGameOver(false);
        ShowInstructions(false);
        ShowNewWeaponAvailable(false);

        totalScore.Value = "0";
        pizzaCountScore.Value = "0";

        chefHealthBar.ResetToStart();
    }
}
