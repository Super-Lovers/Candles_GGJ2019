using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(0, 800)]
    public float movement_speed;
    private Rigidbody2D rigid_body;

    [SerializeField]
    private RuntimeAnimatorController human_animator;
    [SerializeField]
    private RuntimeAnimatorController ghost_animator;
    private Animator animator;

    // Interaction parameters
    private bool is_within_interactable = false;
    private IInteractable current_interactable;

	void Start () {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody2D>();

        animator.SetBool("Is Player Idle", true);
    }

    private void Update() {
        if (is_within_interactable) {
            if (Input.GetKeyDown(KeyCode.E)) {
                current_interactable.Action();
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("interactable")) {
            is_within_interactable = true;

            current_interactable = collision.gameObject.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("interactable")) {
            is_within_interactable = false;

            current_interactable = null;
        }
    }
}
