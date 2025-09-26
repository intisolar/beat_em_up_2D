using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }

    [SerializeField] GameObject _gameOverUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 1;
        _gameOverUI.SetActive(true);
    }
}
