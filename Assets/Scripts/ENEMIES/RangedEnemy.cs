using UnityEngine;
using System.Collections;

public class RangedEnemy : ParentEnemy
{
    [Header("Ranged Settings")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootRange = 35f;

    protected override void Update()
    {
        if (_hero == null || _currentHealth <= 0) return;

        float distanceToHero = Vector3.Distance(transform.position, _hero.position);

        if (EnemyManager._Instance.ShouldAttackPlayer(this))
        {
            if (distanceToHero > _shootRange)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_hero.position);
                _animationLinker.WalkAnimation();
                //Move();
            }
            else
            {
                _agent.isStopped = true;
                _animationLinker.StopAnimation();

                if (!_IsAttacking && Time.time > _lastAttackTime + _attackCooldown)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(EnemyManager._Instance.GetExit().position);
            _animationLinker.WalkAnimation();
        }
    }

    protected override void Move()
    {
        _agent.SetDestination(_hero.position);
        _animationLinker.WalkAnimation();
    }

    protected override IEnumerator Attack()
    {
        _IsAttacking = true;
        _animationLinker.AttackAnimation();
        Debug.Log("Ranged attack triggered");

        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(transform.position, _hero.position) <= _shootRange)
        {
            LaunchProjectile();
        }

        _animationLinker.ResetAttack();
        _lastAttackTime = Time.time;
        _IsAttacking = false;
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
        Vector3 direction = (_hero.position + Vector3.up * 1.5f - _shootPoint.position).normalized;
        projectile.transform.forward = direction;
        projectile.GetComponent<Rigidbody>().velocity = direction * 10f;
        Destroy(projectile, 5f);
    }
}
