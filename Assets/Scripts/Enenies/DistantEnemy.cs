using UnityEngine;

public class DistantEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab; // Projectile à lancer
    [SerializeField] private Transform _shootPoint; // Point d'origine du projectile
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _attackRange = 15f;
    [SerializeField] private float _shootRange = 50f; // Distance à laquelle l'ennemi lance un projectile
    [SerializeField] private float _attackCooldown = 2f; // Temps entre deux attaques
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
        //_hero = Hero._Instance.transform; // Trouver la référence au héros
    }

    void Update()
    {
        if (_hero == null || _currentHealth <= 0) return;

        float distanceToHero = Vector3.Distance(transform.position, _hero.position);

        if (distanceToHero > _shootRange)
        {
            FollowHero(); // Suivre le héros si il est trop loin
        }
        else
        {
            if (!_isAttacking && Time.time > _lastAttackTime + _attackCooldown)
            {
                StartCoroutine(ShootProjectile());
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

    private System.Collections.IEnumerator ShootProjectile()
    {
        _isAttacking = true;
        _animationLinker.Attack();


        yield return new WaitForSeconds(0.5f); // Laisser l'animation d'attaque jouer

        if (Vector3.Distance(transform.position, _hero.position) <= _shootRange)
        {
            LaunchProjectile(); // Lancer un projectile
        }
        _lastAttackTime = Time.time;
        _isAttacking = false;
    }

    private void LaunchProjectile()
    {
        // Créer le projectile et le lancer
        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
        Vector3 direction = (_hero.position - _shootPoint.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = direction * 10f; // Vitesse du projectile

        // Activer la destruction du projectile après un certain temps
        Destroy(projectile, 5f);
    }

    private void Die()
    {
        _animationLinker.GetComponentInChildren<AnimationLinker>();
        _animationLinker.Death();
        Destroy(gameObject, 2f); // Détruire l'ennemi après 2 secondes
    }

    public void ReceiveDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die(); // Si l'ennemi est tué
        }
    }
}
