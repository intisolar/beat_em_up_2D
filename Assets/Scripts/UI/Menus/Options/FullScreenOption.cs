using UnityEngine;
using UnityEngine.UI;

public class FullScreenOption : MonoBehaviour
{
    [SerializeField] private Toggle _toggleFullScreenMode;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            _toggleFullScreenMode.isOn = true;
        }

        else
        {
            _toggleFullScreenMode.isOn = false;
        }
    }

    public void ActivateFullScreen(bool setFullScreen)
    {
        Screen.fullScreen = setFullScreen;
    }
}
