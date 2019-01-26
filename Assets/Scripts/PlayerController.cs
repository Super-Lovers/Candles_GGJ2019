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

	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        IntroDialogueScript = GameObject.FindGameObjectWithTag("Intro Container").GetComponent<IntroDialogueScript>();
	}
	
	void FixedUpdate () {
        if (IsCharacterInADialogue == false &&
            IsTeleportingPlayer == false &&
            IntroDialogueScript.IsIntroEnded == true)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            _rigidbody.AddForce(
                new Vector3(
                    horizontalMovement * (Speed),
                    verticalMovement * (Speed), 0));
        }
	}
}
