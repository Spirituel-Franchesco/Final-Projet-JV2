using System.Collections;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath; // Événement déclenché quand le joueur meurt
    public static HeroHealth _Instance;

    [SerializeField] private GameObject _shieldEffect; // Effet visuel pour l'invincibilité
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _invincibilityDuration = 10f; // Durée d'invincibilité en secondes
    [SerializeField] private float _invincibilityCooldown = 5f; // Temps de recharge en secondes
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    private bool _isInvincible = false;
    private bool _isCooldownActive = false;
    private float _cooldownTimer = 1.5f;

    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogError("Il y a déjà une instance de PlayerHealth dans la scène !");
            return;
        }
        _Instance = this;
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        if (_shieldEffect != null)
            _shieldEffect.SetActive(false); // Désactive l'effet visuel au début
    }

    void Update()
    {
        // Test de dégâts
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
            Debug.Log($"HeroHealth : {_currentHealth}");
        }

        // Activation de l'invincibilité
        if (Input.GetKeyDown(KeyCode.Space) && !_isInvincible && !_isCooldownActive)
        {
            StartCoroutine(ActivateInvincibility());
        }

        // Gestion du cooldown
        if (_isCooldownActive)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _invincibilityCooldown)
            {
                _isCooldownActive = false;
                _cooldownTimer = 0f;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (_isInvincible)
        {
            Debug.Log("Le joueur est invincible, aucun dégât reçu !");
            return; // Ignore les dégâts si invincible
        }

        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
        Debug.Log("Dégâts infligés : " + damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _animator.SetBool("IsDeath", true);
            OnPlayerDeath?.Invoke(); // Déclenche l'événement de mort du joueur
            PlayerMovement._Instance.enabled = false;
        }
    }

    public void ResetHealth()
    {
        PlayerMovement._Instance.enabled = true;
        _currentHealth = _maxHealth;
        _healthBar.SetHealth(_currentHealth);
        _animator.SetBool("IsDeath", false); // Réinitialise l'animation de mort
    }

    private IEnumerator ActivateInvincibility()
    {
        _isInvincible = true;
        _isCooldownActive = true;

        // Active l'effet visuel du bouclier
        if (_shieldEffect != null)
            _shieldEffect.SetActive(true);

        Debug.Log("Invincibilité activée !");
        yield return new WaitForSeconds(_invincibilityDuration);

        _isInvincible = false;

        // Désactive l'effet visuel du bouclier
        if (_shieldEffect != null)
            _shieldEffect.SetActive(false);

        Debug.Log("Invincibilité désactivée !");
    }
}
