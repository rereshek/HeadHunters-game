using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FoodListener : MonoBehaviour
{
    public TextMeshProUGUI food;
    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        food = gameObject.GetComponent<TextMeshProUGUI>();
        food.text = "Food: 0";
        EventSystem.Instance.OnFoodCollected += OnFoodCollected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFoodCollected(FoodUIData foodData)
    {
        if (playerID == foodData.playerID)
        {
            food.text = "Food: " + foodData.foodCount.ToString();
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.OnFoodCollected -= OnFoodCollected;
    }
}
