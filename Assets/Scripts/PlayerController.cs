using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(0, 100)]
    public float Speed;
    private CharacterController _characterController;

	void Start () {
        _characterController = GetComponent<CharacterController>();
	}
	
	void Update () {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        _characterController.Move(
            new Vector3(
                horizontalMovement * (Speed * 0.0020f),
                verticalMovement * (Speed * 0.0020f), 0));
	}
}
