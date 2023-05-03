using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _distanceToChase = 10f;
    [SerializeField] private float _distanceToLose = 20f;
    [SerializeField] private float _distanceToStop = 2f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _keepChasingTime = 3f;
    [SerializeField] private Transform _emptyPoint;
    [SerializeField] private EnemyGunShooter _gun;
    [SerializeField] private float _shotRate = 0.5f;

    private bool _isChasing, _isShooting;
    private PlayerController _player;
    private Vector3 _targetPoint, _startPoint;

    private bool _justLostPlayer, _tooNear;
    private float _distanceFromPlayer;

    private Tweener _waitTweener, _shotRateTweener;


    private void Start()
    {
        _startPoint = transform.position;
        _player = PlayerController.instance;
    }

    private void Update()
    {
        _targetPoint = _player.transform.position;
        _targetPoint.y = transform.position.y;

        _distanceFromPlayer = Vector3.Distance(transform.position, _targetPoint);

        if (!_isChasing)
        {
            if (_justLostPlayer)
            {
                WaitAndGoToInitialPosition();
            }
            CheckToChase();
        }
        else
        {
            CheckToLose();
            CheckToStop();
            if(_tooNear)
            {
                StopMoving();
            }
            else
            {
                MoveToTarget();
            }
            if(!_isShooting)
            {
                Shot();
            }
        }
    }

    private void CheckToChase()
    {
        if (_distanceFromPlayer < _distanceToChase)
        {
            _isChasing = true;
            BreakWaiting();
        }
    }

    private void CheckToLose()
    {
        if (_distanceFromPlayer > _distanceToLose)
        {
            _justLostPlayer = true;
            _isChasing = false;
        }
    }

    private void CheckToStop()
    {
        _tooNear = _distanceFromPlayer < _distanceToStop;
    }

    private void MoveToTarget()
    {
        _agent.destination = _targetPoint;
    }

    private void MoveToInitialPosition()
    {
        _agent.destination = _startPoint;
    }

    private void StopMoving()
    {
        _agent.destination = transform.position;
    }

    private void WaitAndGoToInitialPosition()
    {
        _justLostPlayer = false;
        StopMoving();
        _waitTweener?.Kill();
        _waitTweener = _emptyPoint.DOMoveX(0, _keepChasingTime).OnComplete(
            () =>
            {
                MoveToInitialPosition();
            });
    }

    private void Shot()
    {
        _isShooting = true;
        _gun.TryFire();
        _shotRateTweener?.Kill();
        _shotRateTweener = _emptyPoint.DOMoveX(0, _shotRate).OnComplete(
            () =>
            {
                _isShooting = false;
            });
    }

    private void BreakWaiting()
    {
        _justLostPlayer = false;
        _waitTweener?.Kill();
    }
}
