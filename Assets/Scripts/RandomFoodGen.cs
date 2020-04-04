using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFoodGen : MonoBehaviour
{
    public GameObject[] FoodPrefabs;

    public static RandomFoodGen Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject getRandomFood()
    {
        int rand = Random.Range(0, FoodPrefabs.Length);
        return FoodPrefabs[rand];

    }
}
