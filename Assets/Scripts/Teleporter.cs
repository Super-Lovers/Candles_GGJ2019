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

    private void Teleport() {
        player.transform.position = new_position.transform.position;
    }
}
