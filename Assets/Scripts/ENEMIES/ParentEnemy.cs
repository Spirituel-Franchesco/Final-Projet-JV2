using UnityEngine;
using UnityEngine.AI;

public abstract class ParentEnemy : MonoBehaviour
{
    public bool _IsAttacking;

    [Header("Base Settings")]
    [SerializeField] protected AudioClip _deathClip;
    [SerializeField] protected AudioClip _attackClip;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected float _attackCooldown = 2f;
    [SerializeField] protected float _attackRange = 1.5f;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] private int _rewardValue = 40;

    [Header("References")]
    [SerializeField] protected Transform _hero;
    [SerializeField] protected NavMeshAgent _agent;

    protected AnimationLinker _animationLinker;
    protected float _lastAttackTime;
    protected int _currentHealth;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    protected virtual void Start()
    {
        _animationLinker = GetComponentInChildren<AnimationLinker>();
        _currentHealth = _maxHealth;
        //_hero = PlayerMovement._InstanceResource.transform;

        EnemyManager._Instance.RegisterEnemy(this);
        _hero = EnemyManager._Instance.GetHero(); // Centralisé

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        EnemyManager._Instance.RegisterEnemy(this);
        _hero = EnemyManager._Instance.GetHero();
    }

    protected virtual void Update()
    {
        if (_hero == null || _currentHealth <= 0) return;
        Debug.Log("Attacking Parent 1");

        float distanceToHero = Vector3.Distance(transform.position, _hero.position);
        if (distanceToHero > _attackRange)
        {
            Move();
            //Debug.Log($"distanceToHero : {distanceToHero} _attackRange{_attackRange}");
        }
        else
        {
            _animationLinker.StopAnimation();
            if (!_IsAttacking && Time.time > _lastAttackTime + _attackCooldown)
            {
                _animationLinker.AttackAnimation();

                _IsAttacking = true;

                //StartCoroutine(Attack());
                Debug.Log("Attacking Parent 2");
            }
        }
    }

    public virtual void LaunchAttack()
    {
        StartCoroutine(Attack());
    }

    public virtual void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        if (_deathClip != null)
            AudioSource.PlayClipAtPoint(_deathClip, transform.position);
        _animationLinker.DeathAnimation();
        EnemyManager._Instance.UnregisterEnemy(this);
        Destroy(gameObject, 3f);
        ResourceManager._InstanceResource?.AddResource(_rewardValue);
    }

    protected abstract void Move();
    protected abstract System.Collections.IEnumerator Attack();

    public virtual void ResetEnemy()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _currentHealth = _maxHealth;
        _agent.enabled = true;
        _agent.isStopped = false;
        _animationLinker.ResetAttack();
        _animationLinker.StopAnimation();
    }
}
