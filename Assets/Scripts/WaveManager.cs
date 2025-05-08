using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

//public class WaveManager : MonoBehaviour
//{
//    public static WaveManager _Instance;

//    [SerializeField] private GameObject victoryTextUI; // drag ton UI ici
//    [SerializeField] private HeroHealth heroHealth;     // référence au joueur

//    [Header("Ennemis à instancier")]
//    public GameObject meleeEnemyPrefab;
//    public GameObject rangedEnemyPrefab;
//    public GameObject tankEnemyPrefab;

//    [Header("Position d'apparition")]
//    public Transform[] spawnPoints;

//    [Header("Configuration des vagues")]
//    private int currentWave = 0;
//    private int totalWaves = 3;
//    private int enemiesPerTypePerWave = 1;
//    private int enemiesAlive = 0;
//    public float timeBetweenWaves = 1f;

//    private List<ParentEnemy> spawnedEnemies = new List<ParentEnemy>();
//    private bool gameEnded = false;
//    private bool isSpawning = false;

//    private void Awake()
//    {
//        if (_Instance == null) _Instance = this;
//        else Destroy(gameObject);
//    }

//    private void Start()
//    {
//        StartCoroutine(StartNextWave());
//    }

//    private IEnumerator StartNextWave()
//    {
//        yield return new WaitForSeconds(timeBetweenWaves);

//        currentWave++;

//        if (currentWave > totalWaves)
//            yield break;

//        enemiesPerTypePerWave = currentWave;

//        SpawnWave();

//        yield break;
//    }

//    //private IEnumerator SpawnWave()
//    //{
//    //    isSpawning = true;

//    //    // ton code de spawn ici

//    //    isSpawning = false;
//    //}


//    void SpawnWave()
//    {
//        enemiesAlive = 0;

//        for (int i = 0; i < enemiesPerTypePerWave; i++)
//        {
//            SpawnEnemy(meleeEnemyPrefab);
//            SpawnEnemy(rangedEnemyPrefab);
//            SpawnEnemy(tankEnemyPrefab);
//        }
//    }

//    void SpawnEnemy(GameObject prefab)
//    {
//        int spawnIndex = Random.Range(0, spawnPoints.Length);
//        Instantiate(prefab, spawnPoints[spawnIndex].position, Quaternion.identity);
//        enemiesAlive++;
//    }

//    public void OnEnemyDeath()
//    {
//        enemiesAlive--;

//        ResourceManager._Instance.AddGold(10); // ou autre valeur

//        if (enemiesAlive <= 0)
//        {
//            if (currentWave < totalWaves)
//            {
//                StartCoroutine(StartNextWave());
//            }
//            else
//            {
//                //CheckVictoryCondition();
//            }
//        }
//    }

//    private void Update()
//    {
//        if (isSpawning) return;

//        //if (spawnedEnemies.Count == 0 && currentWave == waves.Count)
//        //{
//        //    if (heroHealth != null && heroHealth.IsAlive()) // méthode que tu peux ajouter si elle n’existe pas
//        //    {
//        //        victoryTextUI.SetActive(true);
//        //        Debug.Log("Victoire !");
//        //        // Ici tu peux appeler LoadEndMenu() ou autre selon ton système de score
//        //    }
//        //}
//    }

//    //void CheckVictoryCondition()
//    //{
//    //    if (!gameEnded && HeroHealth._Instance.IsAlive())
//    //    {
//    //        gameEnded = true;
//    //        Debug.Log("?? Victoire !");
//    //        // ici, affiche UI victoire / transition scène
//    //    }
//    //}
//}


public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _zigzagEnemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private GameObject _youWinPanel; // Assigne dans l’inspector

    public float _timeBetweenWaves = 5f;
    public int _maxWaves = 3;
    private int _currentWave = 0;
    private List<GameObject> _aliveEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnWaves());
        _youWinPanel.gameObject.SetActive(false);
    }

    IEnumerator SpawnWaves()
    {
        while (_currentWave < _maxWaves)
        {
            _currentWave++;

            UpdateWaveText();

            for (int i = 0; i < _currentWave; i++)
            {
                SpawnEnemy(_meleeEnemyPrefab);
                SpawnEnemy(_rangedEnemyPrefab);
                SpawnEnemy(_zigzagEnemyPrefab);
                yield return new WaitForSeconds(0.5f); // petit délai entre chaque instanciation si tu veux
            }

            yield return new WaitForSeconds(_timeBetweenWaves);
        }


    }


    public void EnemyDied(GameObject enemy)
    {
        _aliveEnemies.Remove(enemy);

        if (_currentWave >= _maxWaves && _aliveEnemies.Count == 0)
        {
            _youWinPanel.gameObject.SetActive(true);
        }


    }


    void SpawnEnemy(GameObject enemyPrefab)
    {
        int spawnIndex = Random.Range(0, _spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab, _spawnPoints[spawnIndex].position, Quaternion.identity);
        _aliveEnemies.Add(newEnemy);
    }

    void UpdateWaveText()
    {
        _waveText.text = "Vague : " + _currentWave;
    }
}
