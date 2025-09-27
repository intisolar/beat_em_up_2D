using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject _pausePanel;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }

    /*public void Continue()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        Application.targetFrameRate = 120;
    }*/

    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
