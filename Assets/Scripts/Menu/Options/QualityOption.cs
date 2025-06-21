using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityOption : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _dropDownQuality;
    [SerializeField] int _quality;

    void Start()
    {
        _quality = PlayerPrefs.GetInt("QualityNumber", 3);
        _dropDownQuality.value = _quality;
        AdjustQuality();
    }

    public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(_dropDownQuality.value);
        PlayerPrefs.SetInt("QualityNumber", _dropDownQuality.value);
        _quality = _dropDownQuality.value;
    }
}