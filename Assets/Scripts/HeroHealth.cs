using System.Collections;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler _OnPlayerDeath; // Événement déclenché quand le joueur meurt  
    public static HeroHealth _Instance;

    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _isCooldownActive = false;

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
            _isCooldownActive = false;
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
