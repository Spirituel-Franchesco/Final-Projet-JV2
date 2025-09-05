using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float _timeBetweenWaves = 10f;
    public int _maxWaves = 3;

    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _zigzagEnemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private GameObject _youWinPanel; // Assigne dans l’inspector

    private List<GameObject> _aliveEnemies = new List<GameObject>();
    private int _currentWave = 0;

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
        _waveText.text = "Wave : " + _currentWave;
    }
}
