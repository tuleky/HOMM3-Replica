using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _damagePowerText;
    [SerializeField] TextMeshProUGUI _healthText;

    public void UpdateDamagePowerText(int newPowerValue)
    {
        _damagePowerText.text = newPowerValue.ToString();
    }

    public void UpdateHealthText(int newHealthValue)
    {
        _healthText.text = newHealthValue.ToString();
    }
}
