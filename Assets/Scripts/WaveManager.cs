using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private GameObject victoryTextUI; // drag ton UI ici
    [SerializeField] private HeroHealth heroHealth;     // référence au joueur

    [Header("Ennemis à instancier")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject tankEnemyPrefab;

    [Header("Position d'apparition")]
    public Transform[] spawnPoints;

    [Header("Configuration des vagues")]
    private int currentWave = 0;
    private int totalWaves = 3;
    private int enemiesPerTypePerWave = 1;
    private int enemiesAlive = 0;
    public float timeBetweenWaves = 1f;

    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;

        if (currentWave > totalWaves)
            yield break;

        enemiesPerTypePerWave = currentWave;

        SpawnWave();

        yield break;
    }

    void SpawnWave()
    {
        enemiesAlive = 0;

        for (int i = 0; i < enemiesPerTypePerWave; i++)
        {
            SpawnEnemy(meleeEnemyPrefab);
            SpawnEnemy(rangedEnemyPrefab);
            SpawnEnemy(tankEnemyPrefab);
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemiesAlive++;
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;

        ResourceManager.Instance.AddGold(10); // ou autre valeur

        if (enemiesAlive <= 0)
        {
            if (currentWave < totalWaves)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                //CheckVictoryCondition();
            }
        }
    }

    //void CheckVictoryCondition()
    //{
    //    if (!gameEnded && HeroHealth.Instance.IsAlive())
    //    {
    //        gameEnded = true;
    //        Debug.Log("?? Victoire !");
    //        // ici, affiche UI victoire / transition scène
    //    }
    //}
}
