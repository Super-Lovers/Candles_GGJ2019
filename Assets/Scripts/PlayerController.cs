using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(0, 800)]
    public float Speed;
    private Rigidbody2D _rigidbody;
    public static bool IsCharacterInADialogue = false;
    public static bool IsTeleportingPlayer = false;
    public IntroDialogueScript IntroDialogueScript;

    public static bool IsPlayerSpooky = false;
    public RuntimeAnimatorController DefaultAnimator;
    public RuntimeAnimatorController SpookyAnimator;
    private Animator _animator;

    public int TimeUntilSentBack;

    // ****************************
    // Player Levels variables
    // *** Puzzle in Jane's room
    public bool IsJaneDoorOpened = false;
    public bool IsBedChecked = false;
    public bool IsBoxChecked = false;
    public GameObject JaneBlockedDoor;

    // ** Room where you find the box
    public bool IsNecklaceFound = false;

	void Start () {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        IntroDialogueScript = GameObject.FindGameObjectWithTag("Intro Container").GetComponent<IntroDialogueScript>();

        _animator.SetBool("Is Player Idle", true);
    }
	
	void FixedUpdate () {
        if (IsCharacterInADialogue == false &&
            IsTeleportingPlayer == false &&
            IntroDialogueScript.IsIntroEnded == true)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            // If the player is standing still
            if (horizontalMovement == 0 && verticalMovement == 0)
            {
                _animator.SetBool("Is Player Idle", true);
            } else
            {
                _animator.SetBool("Is Player Idle", false);

                if (horizontalMovement > 0)
                {
                    _animator.SetInteger("Player Direction", 1);
                    _animator.SetInteger("Last Player Direction", 1);
                } else if (horizontalMovement < 0)
                {
                    _animator.SetInteger("Player Direction", 0);
                    _animator.SetInteger("Last Player Direction", 0);
                } else if (verticalMovement > 0)
                {
                    _animator.SetInteger("Player Direction", 2);
                    _animator.SetInteger("Last Player Direction", 2);
                } else if (verticalMovement < 0)
                {
                    _animator.SetInteger("Player Direction", 3);
                    _animator.SetInteger("Last Player Direction", 3);
                }
            }


            _rigidbody.AddForce(
                new Vector3(
                    horizontalMovement * (Speed),
                    verticalMovement * (Speed), 0));
        }
	}
}
