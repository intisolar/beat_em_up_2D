using UnityEditor;
using UnityEngine;

/// <summary>
/// Description: Manages the sound effects and background music in the game.
/// Responsibilities:
/// Handles the playing of sound effects for actions such as attacks, pickups, and environmental sounds.
/// Manages background music tracks, allowing for transitions or pauses as needed.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

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
