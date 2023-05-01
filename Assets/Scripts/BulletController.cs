using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject _particleEffect;
    [SerializeField] private int _damageCount = 1;

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            var healthController = other.GetComponent<EnemyHealthController>();
            if(healthController != null)
            {
                healthController.DamageToHealth(_damageCount);
            }
        }

        //Effect part
        var miniDistance = transform.forward * (-_moveSpeed * Time.deltaTime);
        Instantiate(_particleEffect, transform.position + miniDistance, transform.rotation);
    }
}
