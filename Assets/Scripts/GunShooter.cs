using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private PlayerController _player;

    public void TryFire()
    {
        RaycastHit hit;
        if(Physics.Raycast(_player.CameraTransform.position,
            _player.CameraTransform.forward, out hit, 50f))
        {
            var distance = Vector3.Distance(_player.CameraTransform.position, hit.point);
            if(distance > 2f)
            {
                _firePoint.LookAt(hit.point);
            }
        }
        else
        {
            _firePoint.LookAt(_player.CameraTransform.position +
                                _player.CameraTransform.forward * 30f);
        }
        Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
    }
}
