using System.Collections;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler _OnPlayerDeath; // Événement déclenché quand le joueur meurt  
    public static HeroHealth _Instance;

    //[SerializeField] private Animator _animator;  
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _invincibilityDuration = 10f; // Durée d'invincibilité en secondes  
    [SerializeField] private float _invincibilityCooldown = 5f; // Temps de recharge en secondes  
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

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
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        // Test de dégâts  
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
            Debug.Log($"HeroHealth : {_currentHealth}");
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

        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
        Debug.Log("Dégâts infligés : " + damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            //_animationLinker.Death();  
            //_animator.SetBool("IsDeath", true);  
            _OnPlayerDeath?.Invoke(); // ça déclenchera ShowGameOver() 
            GetComponent<PlayerMovement>().enabled = false; // Désactiver le mouvement du joueur
            Debug.Log("Le joueur est mort !");  
        }
    }

    public void ResetHealth()
    {
        GetComponent<PlayerMovement>().enabled = true; // Réactiver le mouvement du joueur
        _currentHealth = _maxHealth;
        _healthBar.SetHealth(_currentHealth);
        //_animator.SetBool("IsDeath", false); // Réinitialise l'animation de mort  
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public void ResetPlayer()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        // Remettre la vie au max :  
        HeroHealth._Instance.ResetHealthToMax();
    }

    // Renommé pour éviter le conflit avec la méthode existante  
    public void ResetHealthToMax()
    {
        _currentHealth = _maxHealth;
    }
}
