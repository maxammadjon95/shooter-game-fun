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
    [SerializeField] private float _shotRate = 0.3f;
    [SerializeField] private int _fireCount = 3;
    [SerializeField] private float _timeBetweenShots = 3f;
    [SerializeField] private EnemyAnimationPlayer _animation;

    private bool _isChasing, _isWaitingForShot, _isShooting;
    private PlayerController _player;
    private Vector3 _targetPoint, _startPoint;

    private bool _justLostPlayer, _tooNear;
    private float _distanceFromPlayer;

    private Tweener _waitTweener;
    private Tweener _shotContinuationTweener;

    private WaitForSeconds _rateWaiter;

    private void Start()
    {
        _startPoint = transform.position;
        _player = PlayerController.instance;
        _rateWaiter = new WaitForSeconds(_shotRate);
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
            else if(!_isShooting)
            {
                MoveToTarget();
            }

            if(!_isWaitingForShot)
            {
               BeginWaitingForShot();
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
        _animation.MoveAnimationPlay();
    }

    private void MoveToInitialPosition()
    {
        _agent.destination = _startPoint;
        _animation.MoveAnimationPlay();
    }

    private void StopMoving()
    {
        _agent.destination = transform.position;
        _animation.StopAndPlayIdle();
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

    private IEnumerator ShotCoroutine()
    {
        _isShooting = true;
        _animation.ShotAnimationPlay();

        for (int i = 0; i < _fireCount; i++)
        {
            _gun.TryFire();
            yield return _rateWaiter;
        }
        _isWaitingForShot = false;
        _isShooting = false;
        MoveToTarget();
    }

    private void BeginWaitingForShot()
    {
        _isWaitingForShot = true;
        _shotContinuationTweener?.Kill();
        _shotContinuationTweener = _emptyPoint.DOMoveX(0, _timeBetweenShots).OnComplete(
            () =>
            {
                StopMoving();
                StartCoroutine(ShotCoroutine());
            });
    }

    private void BreakWaiting()
    {
        _justLostPlayer = false;
        _waitTweener?.Kill();
    }

    public void DontShot()
    {
        _shotContinuationTweener?.Kill();
        StopCoroutine(ShotCoroutine());
        _isShooting = false;
        MoveToTarget();
    }
}
