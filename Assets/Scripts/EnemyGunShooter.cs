using UnityEngine;

public class EnemyGunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private EnemyMoveController _enemy;

    public void TryFire()
    {
        Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
    }
}
