using UnityEngine;
using System.Collections;

public class ZigzagEnemy : ParentEnemy
{
    [SerializeField] private GameObject _meleeEnemyPrefab; // prefab à instancier à la mort

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

        Debug.Log("Clone enemy attacking");

        if (_attackClip != null)
            _audioSource.PlayOneShot(_attackClip);

        yield return null;

        if (Vector3.Distance(transform.position, _hero.position) <= _attackRange)
        {
            HeroHealth._Instance.TakeDamage(_damage);
        }

        _agent.isStopped = false;
        _lastAttackTime = Time.time;
    }

    public override void Die()
    {
        Debug.Log("Clone enemy died, spawning 3 MeleeEnemies!");

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2f;
            spawnPosition.y = transform.position.y; // garder la même hauteur
            Instantiate(_meleeEnemyPrefab, spawnPosition, Quaternion.identity);
        }

        base.Die(); // joue animation et détruit l’objet
    }
}
