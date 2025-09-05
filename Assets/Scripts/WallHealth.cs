using UnityEngine;

public class WallHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
