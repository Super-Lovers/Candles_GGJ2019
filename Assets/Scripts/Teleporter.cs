using UnityEngine;

public class Teleporter : MonoBehaviour {
    private GameObject player;
    private FadeController fade_transitioner;

    [SerializeField]
    private GameObject new_position;

    private bool is_teleporting = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        fade_transitioner = FindObjectOfType<FadeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (is_teleporting == true)
            return;

        fade_transitioner.StartFade();

        Invoke("Teleport", 0.5f);
    }

    private void Teleport()
    {
        var current_coords = transform.position;
        var new_coords = new_position.transform.position;
        var edited_coords = new_coords;

        if (current_coords.y > new_coords.y) {
            edited_coords.y -= 1;
        } else {
            edited_coords.y += 2.5f;
        }

        player.transform.position = edited_coords;
    }
}
