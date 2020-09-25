﻿using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(0, 800)]
    public float movement_speed;
    private Rigidbody2D rigid_body;

    [SerializeField]
    private RuntimeAnimatorController human_animator;
    [SerializeField]
    private RuntimeAnimatorController ghost_animator;
    private Animator animator;

    // Player character parameters
    private Realm current_realm;
    private int realm_countdown = 15;

	void Start () {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody2D>();

        animator.SetBool("Is Player Idle", true);
    }
	
	void FixedUpdate () {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        // If the player is standing still
        if (horizontalMovement == 0 && verticalMovement == 0) {
            animator.SetBool("Is Player Idle", true);
        } else {
            animator.SetBool("Is Player Idle", false);

            if (horizontalMovement > 0) {
                animator.SetInteger("Player Direction", 1);
                animator.SetInteger("Last Player Direction", 1);
            } else if (horizontalMovement < 0) {
                animator.SetInteger("Player Direction", 0);
                animator.SetInteger("Last Player Direction", 0);
            } else if (verticalMovement > 0) {
                animator.SetInteger("Player Direction", 2);
                animator.SetInteger("Last Player Direction", 2);
            } else if (verticalMovement < 0) {
                animator.SetInteger("Player Direction", 3);
                animator.SetInteger("Last Player Direction", 3);
            }
        }

        rigid_body.AddForce(
            new Vector3(
                horizontalMovement * (movement_speed),
                verticalMovement * (movement_speed), 0));
    }
}
