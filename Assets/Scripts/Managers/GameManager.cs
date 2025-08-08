using UnityEngine;

/***
 * Manages the global game state and transitions between levels or stages.
 * 
 * Responsibilities:
 * Manages the game state (Start, Pause, Win, Lose).
 * Controls level transitions and the conditions for progressing from one stage to another.
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
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
}
