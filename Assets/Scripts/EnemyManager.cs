using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _Instance;

    [SerializeField] private Transform _hero;
    [SerializeField] private Transform _exitZone;
    [SerializeField] private float _detectionRange = 35f;

    private List<ParentEnemy> _allEnemies = new List<ParentEnemy>();
    private List<ParentEnemy> _attackers = new List<ParentEnemy>();
    private const int _maxAttackers = 3;

    private void Awake()
    {
        _Instance = this;
    }

    public void RegisterEnemy(ParentEnemy enemy)
    {
        if (!_allEnemies.Contains(enemy))
        {
            _allEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(ParentEnemy enemy)
    {
        _allEnemies.Remove(enemy);
        _attackers.Remove(enemy);
    }

    private void Update()
    {
        UpdateAttackers();
    }

    private void UpdateAttackers()
    {
        _attackers.Clear();

        List<ParentEnemy> potentialAttackers = new List<ParentEnemy>();

        foreach (var enemy in _allEnemies)
        {
            if (enemy == null) continue;

            float distToHero = Vector3.Distance(enemy.transform.position, _hero.position);
            float distToExit = Vector3.Distance(enemy.transform.position, _exitZone.position);

            if (distToHero < distToExit && distToHero <= _detectionRange)
            {
                potentialAttackers.Add(enemy);
            }
        }

        potentialAttackers.Sort((a, b) =>
        {
            float distA = Vector3.Distance(a.transform.position, _hero.position);
            float distB = Vector3.Distance(b.transform.position, _hero.position);
            return distA.CompareTo(distB);
        });

        for (int i = 0; i < Mathf.Min(_maxAttackers, potentialAttackers.Count); i++)
        {
            _attackers.Add(potentialAttackers[i]);
        }
    }

    public bool ShouldAttackPlayer(ParentEnemy enemy)
    {
        return _attackers.Contains(enemy);
    }

    public Transform GetHero() => _hero;
    public Transform GetExit() => _exitZone;
}
