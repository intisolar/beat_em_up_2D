using UnityEngine;

public class SceneMusicStarter : MonoBehaviour
{

    [Header("Wwise Music Event")]
    public AK.Wwise.Event musicEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (musicEvent != null)
        {
            // Usás tu AudioManager para manejar el crossfade
            AudioManager.Instance.PlayMusic(musicEvent);
        }
        else
        {
            Debug.Log("music event is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
