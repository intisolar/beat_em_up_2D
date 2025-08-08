using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_fillImage != null)
        {
            _fillImage.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            _fillImage = GetComponent<Image>();
        }

    }
}
