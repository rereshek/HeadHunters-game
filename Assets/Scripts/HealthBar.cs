using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int PlayerID;

    public void Start()
    {
        EventSystem.Instance.OnHealthChanged += OnHealthChanged;
    }
    public void OnHealthChanged(HealthUIData healthData)
    {
        if (healthData.playerID == PlayerID)
        {
            if (slider.maxValue == 1)
            {
                SetMaxHealth(healthData.maxHealth);
            }
            
                slider.value = healthData.health;
                Debug.Log("health set to " + healthData.health);
            
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        //slider.value = maxHealth;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnHealthChanged -= OnHealthChanged;
    }

}
