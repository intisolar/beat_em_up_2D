using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private int _currentIndex;
    [SerializeField] private Image _characterImage;
    [SerializeField] private CharacterData _characterData;

    void Start()
    {
        _currentIndex = 0;
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        PlayerPrefs.SetInt("PlayerIndex", _currentIndex);
        if (_characterData != null && _characterData.Characters.Count > 0)
        {
            _characterImage.sprite = _characterData.Characters[_currentIndex].Image;
        }
    }

    public void NextCharacter()
    {
        // Circular al siguiente índice
        if (_currentIndex == _characterData.Characters.Count - 1)
        {
            _currentIndex = 0;
        }
        else
        {
            _currentIndex += 1;
        }

        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        // Circular al anterior índice
        if (_currentIndex == 0)
        {
            _currentIndex = _characterData.Characters.Count - 1;
        }
        else
        {
            _currentIndex -= 1;
        }

        UpdateCharacterDisplay();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
