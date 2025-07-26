using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject _pausePanel;

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
