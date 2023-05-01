using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _distanceToChase = 10f;
    [SerializeField] private float _distanceToLose = 20f;
    [SerializeField] private NavMeshAgent _agent;

    private bool _isChasing;
    private PlayerController _player;
    private Vector3 _targetPoint;

    private void Start()
    {
        _player = PlayerController.instance;
    }

    private void Update()
    {
        _targetPoint = _player.transform.position;
        _targetPoint.y = transform.position.y;

        if (!_isChasing)
        {
            CheckToChase();
        }
        else
        {
            MoveToTarget();
            CheckToLose();
        }
    }

    private void CheckToChase()
    {
        var distanceFromPlayer = Vector3.Distance(transform.position,
            _targetPoint);
        if(distanceFromPlayer < _distanceToChase) 
        { 
            _isChasing = true;
        }
    }

    private void CheckToLose()
    {
        var distanceFromPlayer = Vector3.Distance(transform.position,
            _targetPoint);
        if (distanceFromPlayer > _distanceToLose)
        {
            _isChasing = false;
        }
    }

    private void MoveToTarget()
    {
        //transform.LookAt(_targetPoint);
        //_rigidbody.velocity = transform.forward * _moveSpeed;

        _agent.destination = _targetPoint;
    }
}
