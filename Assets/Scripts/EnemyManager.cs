using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private Transform hero;
    [SerializeField] private Transform exitZone;
    [SerializeField] private float detectionDistance = 50f;

    private List<ParentEnemy> allEnemies = new List<ParentEnemy>();
    private List<ParentEnemy> attackers = new List<ParentEnemy>();
    private const int maxAttackers = 3;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(ParentEnemy enemy)
    {
        if (!allEnemies.Contains(enemy))
        {
            allEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(ParentEnemy enemy)
    {
        allEnemies.Remove(enemy);
        attackers.Remove(enemy);
    }

    private void Update()
    {
        UpdateAttackers();
    }

    private void UpdateAttackers()
    {
        attackers.Clear();

        List<ParentEnemy> potentialAttackers = new List<ParentEnemy>();

        foreach (var enemy in allEnemies)
        {
            if (enemy == null) continue;

            float distToHero = Vector3.Distance(enemy.transform.position, hero.position);
            float distToExit = Vector3.Distance(enemy.transform.position, exitZone.position);

            if (distToHero < distToExit && distToHero <= detectionDistance)
            {
                potentialAttackers.Add(enemy);
            }
        }

        potentialAttackers.Sort((a, b) =>
        {
            float distA = Vector3.Distance(a.transform.position, hero.position);
            float distB = Vector3.Distance(b.transform.position, hero.position);
            return distA.CompareTo(distB);
        });

        for (int i = 0; i < Mathf.Min(maxAttackers, potentialAttackers.Count); i++)
        {
            attackers.Add(potentialAttackers[i]);
        }
    }

    public bool ShouldAttackPlayer(ParentEnemy enemy)
    {
        return attackers.Contains(enemy);
    }

    public Transform GetHero() => hero;
    public Transform GetExit() => exitZone;
}
