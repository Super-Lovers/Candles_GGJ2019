using UnityEngine;

public class Teleporter : MonoBehaviour {
    private GameObject _player;
    private GameObject _newDestination;
    private GameObject _originalDestination;
    public bool CanEnter = true;
    private FadeController _fadeTransitionerScript;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _fadeTransitionerScript = GameObject.FindGameObjectWithTag("Transitioner").GetComponent<FadeController>();
        _newDestination = transform.GetChild(0).gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanEnter = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanEnter)
        {
            PlayerController.IsTeleportingPlayer = true;

            _fadeTransitionerScript.StartFade();

            Invoke("TeleportAndEnableMovement", 0.5f);
        }
    }

    private void TeleportAndEnableMovement()
    {
        _fadeTransitionerScript.HideFade();

        Vector3 newPlayerPosition = _player.transform.position;
        newPlayerPosition = _newDestination.transform.position;
        _player.transform.position = newPlayerPosition;

        gameObject.GetComponentInChildren<DoorTeleporter>().CanReturn = false;
        CanEnter = false;

        PlayerController.IsTeleportingPlayer = false;
    }
}
