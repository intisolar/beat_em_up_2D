using TMPro;
using UnityEngine;

/// <summary>
/// Description: Manages the user interface (UI) of the game, including the HUD and menus.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _textUI;

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

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _textUI.text = _score.ToString();
    }
}
