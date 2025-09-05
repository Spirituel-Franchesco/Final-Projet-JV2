using UnityEngine;

public class AnimationLinker : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    void Start()
    {
        //_animator = GetComponent<Animator>();

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            _animator = GetComponentInParent<Animator>();
        }

        if (_animator == null)
        {
            Debug.LogError("Animator non trouvé sur " + gameObject.name);
        }
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
        _animator.SetBool("IsWalking", false);
    }

    public void WalkAnimation()
    {
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsAttacking", false);
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

