using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth;
    [SerializeField] private Image healthBar;

    public void UpdateHealth()
    {
        float newAmount = (float)playerHealth.CurHealth / (float)playerHealth.MaxHealth;
        healthBar.fillAmount = newAmount;
        Debug.Log(newAmount);
    }
}
