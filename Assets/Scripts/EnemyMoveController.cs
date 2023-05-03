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
    private Vector3 _targetPoint, _startPoint;

    private void Start()
    {
        _startPoint = transform.position;
        _player = PlayerController.instance;
    }

    private void Update()
    {
        _targetPoint = _player.transform.position;
        _targetPoint.y = transform.position.y;

        if (!_isChasing)
        {
            CheckToChase();
            MoveToInitialPosition();
        }
        else
        {
            CheckToLose();
            MoveToTarget();
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
        _agent.destination = _targetPoint;
    }

    private void MoveToInitialPosition()
    {
        _agent.destination = _startPoint;
    }
}
