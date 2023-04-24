using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;

    private Vector3 _moveInput;

    private void Update()
    {
        _moveInput.x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        _moveInput.z = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        _characterController.Move(_moveInput);
    }
}
