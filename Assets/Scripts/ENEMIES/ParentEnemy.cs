using UnityEngine;
using UnityEngine.AI;

public abstract class ParentEnemy : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float attackRange = 1.5f;

    [Header("References")]
    [SerializeField] protected Transform hero;
    [SerializeField] protected NavMeshAgent agent;
    protected AnimationLinker animationLinker;

    protected float lastAttackTime;
    protected int currentHealth;
    protected bool isAttacking;

    protected virtual void Start()
    {
        animationLinker = GetComponentInChildren<AnimationLinker>();
        currentHealth = maxHealth;
        //hero = PlayerMovement._Instance.transform;

        EnemyManager.Instance.RegisterEnemy(this);
        hero = EnemyManager.Instance.GetHero(); // Centralisé
    }

    protected virtual void Update()
    {
        if (hero == null || currentHealth <= 0) return;

        float distanceToHero = Vector3.Distance(transform.position, hero.position);
        if (distanceToHero > attackRange)
        {
            Move();
        }
        else
        {
            animationLinker.Stop();
            if (!isAttacking && Time.time > lastAttackTime + attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        animationLinker.Death();
        EnemyManager.Instance.UnregisterEnemy(this);
        Destroy(gameObject, 2f);
    }

    protected abstract void Move();
    protected abstract System.Collections.IEnumerator Attack();

    //public void Die()
    //{
    //    if (isDead) return;

    //    isDead = true;
    //    // Désactiver l'IA / animation
    //    agent.enabled = false;
    //    GetComponent<Collider>().enabled = false;

    //    // Notifier le manager
    //    WaveManager.Instance.OnEnemyDeath();

    //    Destroy(gameObject, 2f); // délai avant suppression si besoin
    //}

}
