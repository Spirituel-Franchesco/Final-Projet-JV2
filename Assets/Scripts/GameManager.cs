using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private AudioSource _victoryClip;
    [SerializeField] private AudioClip _defeatClip;

    private bool _isGameOver = false;

    private void Awake()
    {
        if (_Instance == null) _Instance = this;
    }

    private void Start()
    {
        _gameOverPanel.SetActive(false);
        if (_victoryPanel != null)
            _victoryPanel.SetActive(false);

        // S’abonner à l’événement de mort du joueur
        HeroHealth._Instance._OnPlayerDeath += ShowGameOver;
    }

    private void Update()
    {
        if (_isGameOver) return;

        // Vérifie s'il reste des ennemis
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            ShowVictory();
        }
    }

    public void ShowGameOver()
    {
        if (_defeatClip != null)
            AudioSource.PlayClipAtPoint(_defeatClip, Camera.main.transform.position);

        if (_isGameOver) return;

        _isGameOver = true;
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowVictory()
    {
        if (_victoryClip != null)
            _victoryClip.Play();

        if (_isGameOver) return;

        _isGameOver = true;
        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(true);
            Debug.Log("Victoire ! Tous les ennemis sont détruits.");
        }
        else
        {
            Debug.LogWarning("Victory panel non assigné !");
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
