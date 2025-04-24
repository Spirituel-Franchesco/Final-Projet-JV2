using UnityEngine;

public class MeleeEnemy : ParentEnemy
{
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

    protected override System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animationLinker.Attack();

        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(transform.position, hero.position) <= attackRange)
        {
            //HeroHealth._Instance.TakeDamage(damage);
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }
}
