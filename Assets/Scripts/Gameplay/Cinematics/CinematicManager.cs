using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private float _delayBeforeNextScene = 2f;

    private void Start()
    {
       // Invoke(nameof(LoadNextScene), _delayBeforeNextScene);
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadLevelOne()
    {
        SceneManager.LoadScene("Level");
    }
}
