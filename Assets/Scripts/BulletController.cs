using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidBody;

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
    }
}
