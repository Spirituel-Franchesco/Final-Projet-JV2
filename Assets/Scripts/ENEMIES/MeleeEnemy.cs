using UnityEngine;
using System.Collections;

public class MeleeEnemy : ParentEnemy
{
    protected override void Move()
    {
        if (_agent == null) return;

        if (EnemyManager._Instance.ShouldAttackPlayer(this))
        {
            _agent.isStopped = false;
            _agent.SetDestination(_hero.position);
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(EnemyManager._Instance.GetExit().position);
        }

        _animationLinker.WalkAnimation();
    }

    protected override IEnumerator Attack()
    {
        _agent.isStopped = true;

        if (_attackClip != null)
            _audioSource.PlayOneShot(_attackClip);

        Debug.Log("Melee enemy attacking");

        yield return null;

        if (Vector3.Distance(transform.position, _hero.position) <= _attackRange)
        {
            HeroHealth._Instance.TakeDamage(_damage);
        }

        _agent.isStopped = false;
        _lastAttackTime = Time.time;
    }
}
