using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    //[SerializeField] private Animator _animator;
    [SerializeField] private int _health = 100;

    public void RecieveDamage(int damage)
    {
        damage = 10;
        _health -= damage;
        Debug.Log($"Enemy Health remaining : {_health}");

        if (_health <= 0) // Vérifie si la santé tombe à 0 ou moins
        {
            Destroy(_enemy, 2f);
        }
    }
}
