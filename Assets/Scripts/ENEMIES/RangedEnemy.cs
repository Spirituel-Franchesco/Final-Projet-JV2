using UnityEngine;
using System.Collections;

public class RangedEnemy : ParentEnemy
{
    [Header("Ranged Settings")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootRange = 50f;          // Distance maximale pour tirer
    [SerializeField] private float _attackCooldown = 2f;       // Temps entre les tirs
    //[SerializeField] private int _maxHealth = 100;             // Vie max

    //private int _currentHealth;

    protected override void Start()
    {
        base.Start();
        //currentHealth = maxHealth;
    }

    protected override void Update()
    {
        if (hero == null || currentHealth <= 0) return;

        float distanceToHero = Vector3.Distance(transform.position, hero.position);

        if (distanceToHero > _shootRange)
        {
            Move();
        }
        else
        {
            agent.SetDestination(transform.position); // Stop movement
            animationLinker.Stop();

            if (!isAttacking && Time.time > lastAttackTime + _attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }

    protected override void Move()
    {
        if (agent == null) return;

        if (EnemyManager.Instance.ShouldAttackPlayer(this))
        {
            agent.SetDestination(hero.position);
        }
        else
        {
            agent.SetDestination(EnemyManager.Instance.GetExit().position);
        }

        animationLinker.Walk();
    }

    protected override IEnumerator Attack()
    {
        isAttacking = true;
        animationLinker.Attack();

        yield return new WaitForSeconds(0.5f); // Attente pour l'animation

        if (Vector3.Distance(transform.position, hero.position) <= _shootRange)
        {
            LaunchProjectile();
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
        Vector3 direction = ((hero.position + Vector3.up * 1.5f) - _shootPoint.position).normalized;
        projectile.transform.forward = direction;
        projectile.GetComponent<Rigidbody>().velocity = direction * 10f;
        Destroy(projectile, 5f);
    }
}
