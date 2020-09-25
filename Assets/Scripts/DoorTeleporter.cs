using UnityEngine;

public class DoorTeleporter : MonoBehaviour {
    private GameObject _player;
    public bool CanReturn = false;
    private FadeController _fadeTransitionerScript;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _fadeTransitionerScript = GameObject.FindGameObjectWithTag("Transitioner").GetComponent<FadeController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanReturn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanReturn)
        {
            PlayerController.IsTeleportingPlayer = true;

            gameObject.GetComponentInParent<Teleporter>().CanEnter = false;

            _fadeTransitionerScript.StartFade();

            Invoke("TeleportAndEnableMovement", 0.5f);
        }
    }

    private void TeleportAndEnableMovement()
    {
        Vector3 newPlayerPosition = _player.transform.position;
        newPlayerPosition = gameObject.transform.parent.GetChild(1).transform.position;
        _player.transform.position = newPlayerPosition;
    }
}
