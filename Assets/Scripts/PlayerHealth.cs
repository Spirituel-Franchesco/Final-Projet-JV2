using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider _healthSlider;
    public int _maxHealth = 100;
    public int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = (float)_currentHealth / _maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Le joueur est mort !");
        // Gère la mort du joueur (réinitialisation, écran de défaite, etc.)
    }
}