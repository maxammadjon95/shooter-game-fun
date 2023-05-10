using UnityEngine;

public class EnemyGunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private EnemyMoveController _enemy;

    private PlayerController _player;
    private float _maxAngleToShot = 30f;

    private void Start()
    {
        _player = PlayerController.instance;
    }

    public void TryFire()
    {
        _firePoint.LookAt(_player.transform.position + new Vector3(0f, 1.2f, 0f));

        //Check the angle to the player
        Vector3 targetDirecton = _player.transform.position - _enemy.transform.position;
        float angleToPlayer = Vector3.SignedAngle(targetDirecton, _enemy.transform.forward, Vector3.up);
        if(Mathf.Abs(angleToPlayer) < _maxAngleToShot)
        {
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        }
        else
        {
            _enemy.DontShot();
        }
    }
}
