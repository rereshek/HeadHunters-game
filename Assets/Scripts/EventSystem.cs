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

public struct HealthUIData
{
    public int playerID;
    public int health;
    public int maxHealth;
}

public struct EnemyUIData
{
    public int enemyID;
    public bool showUI;
    public int eHealth;
    public int eMaxHealth;
}

public struct MinionUIData
{
    public int minionCount;
    public int playerID;
}
public class EventSystem : MonoBehaviour
{
    public static EventSystem Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public event Action<FoodUIData> OnFoodCollected;

    public void FoodCollected(FoodUIData foodData)
    {
        if (OnFoodCollected != null)
        {
            Debug.Log("EventSystem::FoodCollected::foodData.playerID " + foodData.playerID + " EventSystem::FoodCollected::foodData.playerID " + foodData.foodCount);
            Debug.Log("EventSystem::OnFoodCollected.Count " + OnFoodCollected.GetInvocationList().Length);
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

    public event Action<HealthUIData> OnHealthChanged;

    public void HealthChanged(HealthUIData healthData)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(healthData);
        }
    }


    public event Action<EnemyUIData> OnEnemyCombatChange;

    public void ShowEnemyUI(EnemyUIData enemyUIData)
    {
        if (OnEnemyCombatChange != null)
        {
            OnEnemyCombatChange(enemyUIData);
        }
    }

    public event Action<HealthUIData> OnHealthBarUpdate;

    public void UpdateHealthCount(HealthUIData healthUI)
    {
        if (OnHealthBarUpdate != null)
        {
            OnHealthBarUpdate(healthUI);
        }
    }

    public event Action<MinionUIData> OnMinionUIUpdate;

    public void UpdateMinionCount(MinionUIData minionUI)
    {
        if(OnMinionUIUpdate != null)
        {
            OnMinionUIUpdate(minionUI);
        }
    }

    public event Action<int> OnPlayerDead;

    public void PlayerDead(int ID)
    {
        if(OnPlayerDead != null)
        {
            OnPlayerDead(ID);
        }
    }

    public event Action OnPauseGame;

    public void PauseGame()
    {
        if(OnPauseGame != null)
        {
            OnPauseGame();
        }
    }
}
