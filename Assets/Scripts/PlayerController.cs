using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTransform;

    private Vector3 _moveInput;

    private void Update()
    {
        _moveInput.x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        _moveInput.z = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        _characterController.Move(_moveInput);

        //camera part
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //for player rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);

        //for camera rotation
        _cameraTransform.rotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles 
                                                    + new Vector3(-mouseInput.y, 0f, 0f));
    }
}
