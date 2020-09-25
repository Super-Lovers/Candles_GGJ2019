using UnityEngine;

public class Teleporter : MonoBehaviour {
    private GameObject player;
    private FadeController fade_transitioner;

    [SerializeField]
    private GameObject new_position;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        fade_transitioner = FindObjectOfType<FadeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        fade_transitioner.StartFade();

        Invoke("TeleportAndEnableMovement", 0.5f);
    }

    private void TeleportAndEnableMovement()
    {
        fade_transitioner.HideFade();

        player.transform.position = new_position.transform.position;
    }
}
