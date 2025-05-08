using UnityEngine;

public class AimPoint : MonoBehaviour
{
    //[SerializeField] private GameObject _enemy;
    //public Animator _animator;
    //public int _health = 100;

    //public void TakeDamage(int amount)
    //{
    //    //amount = 10; // Exemple de valeur de dégâts
    //    _health -= amount;
    //    Debug.Log(_enemy.name + " took " + amount + " damage. Remaining health: " + _health);

    //    if (_health <= 0)
    //    {
    //        Die();
    //    }
    //}

    //private void Die()
    //{
    //    Debug.Log(_enemy.name + " has been destroyed!");
    //    Destroy(_enemy, 2f);
    //}

    [SerializeField] private int _health = 100; // Santé initiale
    [SerializeField] private ParentEnemy _parentEnemy;

    public void TakeDamage(int amount)
    {
        _health -= amount;
        Debug.Log($"{_parentEnemy.gameObject.name} took {amount} damage. Remaining health: {_health}");

        if (_health <= 0)
        {
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
