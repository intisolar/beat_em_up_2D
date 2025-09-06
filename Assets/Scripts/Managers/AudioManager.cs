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

    // �ltimo evento y su playingId (para poder frenarlo con fade)
    private AK.Wwise.Event _currentEvent;

    [SerializeField] private int fadeMs = 600; // tiempo de crossfade

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

    public void PlayMusic(AK.Wwise.Event nextEvent)
    {
        // 1) Arranco la nueva m�sica (se superpone con la anterior)
        nextEvent.Post(gameObject);

        // 2) Si hab�a m�sica sonando, la apago con "Stop" que incluye un fade-out (transitionDuration)
        if (_currentEvent != null)
        {
            AkSoundEngine.ExecuteActionOnEvent(
                _currentEvent.Id,
                AkActionOnEventType.AkActionOnEventType_Stop,
                gameObject,
                fadeMs, // <- este es el "stop diferido" = FADE-OUT del tema anterior
                AkCurveInterpolation.AkCurveInterpolation_Sine
            );
        }

        // 3) Actualizo punteros
        _currentEvent = nextEvent;
    }

}
