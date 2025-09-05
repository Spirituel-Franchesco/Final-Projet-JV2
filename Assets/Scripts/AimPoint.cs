using UnityEngine;

public class AimPoint : MonoBehaviour
{
    [SerializeField] private ParentEnemy _parentEnemy;
    [SerializeField] private int _health = 100; // Santé initiale

    private bool _isDead = false; //  nouveau flag

    public void TakeDamage(int amount)
    {
        if (_isDead) return;

        _health -= amount;
        Debug.Log($"{_parentEnemy.gameObject.name} took {amount} damage. Remaining health: {_health}");

        if (_health <= 0)
        {
            _isDead = true; // active le flag de mort
            Debug.Log($"{_parentEnemy.gameObject.name} is dead.");

            if (_parentEnemy != null)
            {
                _parentEnemy.Die();
            }
            else
            {
                Debug.LogWarning("ParentEnemy reference is missing on " + gameObject.name);
                Destroy(gameObject, 2f);
            }
        }
    }
}
