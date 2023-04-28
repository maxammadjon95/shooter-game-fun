using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject _particleEffect;

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
        var miniDistance = transform.forward * (-_moveSpeed * Time.deltaTime);
        Instantiate(_particleEffect, transform.position + miniDistance, transform.rotation);
    }
}
