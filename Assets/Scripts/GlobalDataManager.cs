using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{

    public static GlobalDataManager Instance;

    public int player1Food;
    public int player2Food;

    public int player1Health;
    public int player2Health;

    public int p1MaxHealth;
    public int p2MaxHealth;

    public int p1Maxdamage;
    public int p2Maxdamage;

    public int p1MinionCount;
    public int p2MinionCount;

    public List<string> Levels = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
