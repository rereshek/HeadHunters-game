using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using System;


public struct FoodUIData
{
    public int playerID;
    public int foodCount;
}

public class EventSystem : MonoBehaviour
{
    public static EventSystem Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    public event Action<FoodUIData> OnFoodCollected;

    public void FoodCollected(FoodUIData foodData)
    {
        if(OnFoodCollected != null)
        {
            OnFoodCollected(foodData);
        }
    }

    public event Action onLeavingIsland;

    public void LeavingIsland()
    {
        if (onLeavingIsland != null)
        {
            onLeavingIsland();
        }
        
    }

}
