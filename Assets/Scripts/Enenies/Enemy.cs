using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 2f; // Temps entre deux attaques
    [SerializeField] private int _damage = 10; // Dégâts infligés au héros
    [SerializeField] private int _maxHealth = 100; // Vie maximale de l'ennemi

    [SerializeField] private Transform _hero;
    private AnimationLinker _animationLinker;
    private float _lastAttackTime;
    private int _currentHealth;
    private bool _isAttacking;

    void Start()
    {
        _animationLinker = GetComponentInChildren<AnimationLinker>();
        _currentHealth = _maxHealth;
        //_hero = PlayerMovement._Instance.transform; // Trouver la référence au héros
    }

    void Update()
    {
        if (_hero == null || _currentHealth <= 0) return;

        float distanceToHero = Vector3.Distance(transform.position, _hero.position);

        if (distanceToHero > _attackRange)
        {
            FollowHero();
        }
        else
        {
            _animationLinker.Stop();
            if (!_isAttacking && Time.time > _lastAttackTime + _attackCooldown)
            {
                StartCoroutine(AttackHero());
            }
        }
    }

    private void FollowHero()
    {
        Vector3 direction = (_hero.position - transform.position).normalized;
        transform.LookAt(_hero.position);

        transform.position += direction * _movementSpeed * Time.deltaTime;
        _animationLinker.Walk();
    }

    private System.Collections.IEnumerator AttackHero()
    {
        _isAttacking = true;
        _animationLinker.Attack();

        yield return new WaitForSeconds(0.5f); // Laisser l'animation d'attaque jouer

        if (Vector3.Distance(transform.position, _hero.position) <= _attackRange)
        {
            //HeroHealth._Instance.TakeDamage(_damage);
        }
        _lastAttackTime = Time.time;
        _isAttacking = false;
    }

    private void Die()
    {
        _animationLinker.Death();
        Destroy(gameObject, 2f); // Détruire l'ennemi après 2 secondes
    }
}
