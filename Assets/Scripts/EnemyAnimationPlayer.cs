using UnityEngine;

public class EnemyAnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly string _shootTrigger = "fireShot";
    private readonly string _isMoving = "isMoving";

    public void ShotAnimationPlay()
    {
        _animator.SetTrigger(_shootTrigger);
    }

    public void MoveAnimationPlay()
    {
        _animator.SetBool(_isMoving, true);
    }

    public void StopAndPlayIdle()
    {
        _animator.SetBool(_isMoving, false);
    }
}
