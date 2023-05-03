using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject _particleEffect;
    [SerializeField] private int _damageCount = 1;
    [SerializeField] private PlayerType _playerType;

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_playerType == PlayerType.Player)
        {
            if (other.CompareTag("Enemy"))
            {
                var healthController = other.GetComponent
                    <EnemyHealthController>();
                if (healthController != null)
                {
                    healthController.DamageToHealth(_damageCount);
                }
            }
        }
        if(_playerType == PlayerType.Enemy)
        {
            Debug.Log("Enemy Hitted Us");
        }

        //Effect part
        var miniDistance = transform.forward * (-_moveSpeed * Time.deltaTime);
        Instantiate(_particleEffect, transform.position + miniDistance, transform.rotation);
    }
}
