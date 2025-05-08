using UnityEngine;

public class AnimationLinker : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ResetAttack()
    {
        GetComponentInParent<ParentEnemy>()._IsAttacking = false;
        _animator.SetBool("IsAttacking", false);
    }

    public void DeathAnimation()
    {
        _animator.SetBool("IsDeath", true);
    }

    public void AttackAnimation()
    {
        _animator.SetBool("IsAttacking", true);
    }

    public void WalkAnimation()
    {
        _animator.SetBool("IsWalking", true);
    }

    public void StopAnimation()
    {
        _animator.SetBool("IsWalking", false);
    }

    public void ControlAttack() 
    {
        GetComponentInParent<ParentEnemy>().LaunchAttack();
    }
}

