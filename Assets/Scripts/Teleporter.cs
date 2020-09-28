using UnityEngine;

public class Teleporter : MonoBehaviour {
    private GameObject player;
    private Animator player_animator;
    private FadeController fade_transitioner;

    [SerializeField]
    private GameObject new_position = default;

    private bool is_teleporting = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        player_animator = player.GetComponent<Animator>();
        fade_transitioner = FindObjectOfType<FadeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (is_teleporting == true)
            return;

        fade_transitioner.StartFade();

        Invoke("Teleport", 0.5f);
        player_animator.SetBool("Is Player Idle", true);
    }

    private void Teleport() {
        player.transform.position = new_position.transform.position;
    }
}
