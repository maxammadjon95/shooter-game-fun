using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _lifeTime;
    [SerializeField] private GameObject _particleEffect;

    private void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        var miniDistance = transform.forward * (-_moveSpeed * Time.deltaTime);
        Instantiate(_particleEffect, transform.position + miniDistance, transform.rotation);
    }
}
