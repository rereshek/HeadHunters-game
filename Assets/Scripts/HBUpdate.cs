using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HBUpdate : MonoBehaviour
{
    public int PlayerID;
    public TextMeshProUGUI healthCount;

    public void Start()
    {
        EventSystem.Instance.OnHealthBarUpdate += OnHealthBarUpdate;
        healthCount = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnHealthBarUpdate(HealthUIData healthUI)
    {
        if (PlayerID == healthUI.playerID)
        {
            healthCount.text = healthUI.health.ToString() + " / " + healthUI.maxHealth.ToString();
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnHealthBarUpdate -= OnHealthBarUpdate;
    }
}
