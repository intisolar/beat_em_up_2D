using TMPro;
using UnityEngine;

public class QualityOption : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDownQuality;
    [SerializeField] private byte _quality = 1;

    private void Start()
    {
        _quality = (byte)PlayerPrefs.GetInt("QualityNumber", _quality);
        _dropDownQuality.value = _quality;
        AdjustQuality();
    }

    public void AdjustQuality()
    {
        if (_dropDownQuality == null)
        {
            Debug.LogError("_dropDownQuality no está asignado en el inspector.");
            return;
        }

        QualitySettings.SetQualityLevel(_dropDownQuality.value);
        _quality = (byte)_dropDownQuality.value;
        PlayerPrefs.SetInt("QualityNumber", _quality);
    }
}
