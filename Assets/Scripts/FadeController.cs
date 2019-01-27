using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {
    private Animator _animator;
    private GameObject _player;
    private GameObject _returnPoint;

	void Start () {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _returnPoint = GameObject.Find("Origin point");
	}

    public void StartFade()
    {
        _animator.SetBool("canFade", true);
        Invoke("HideFade", 0.5f);
    }

    public void HideFade()
    {
        if (PlayerController.IsTeleportingPlayer && PlayerController.IsPlayerHiding == false)
        {
            Vector3 newPlayerPosition = _player.transform.position;
            newPlayerPosition = _returnPoint.transform.position;
            _player.transform.position = newPlayerPosition;
        }

        _animator.SetBool("canFadeOut", true);
        Invoke("SetDefaultState", 0.5f);
    }

    private void SetDefaultState()
    {
        _animator.SetBool("canFade", false);
        _animator.SetBool("canFadeOut", false);

        PlayerController.IsTeleportingPlayer = false;
    }
}
