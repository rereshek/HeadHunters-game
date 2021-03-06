﻿using System.Collections;
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
        EventSystem.Instance.OnFoodCollected += OnFoodCollected;
        food = gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnFoodCollected(FoodUIData foodData)
    {
        //Debug.Log("Updating Food for " + playerID + " " + foodData.playerID);
        if (playerID == foodData.playerID)
        {
           // Debug.Log("Food name " + food.gameObject.name);
            food.text = "Food: " + foodData.foodCount;
        }
    }

    private void OnDestroy()
    {
        
        EventSystem.Instance.OnFoodCollected -= OnFoodCollected;
    }
}
