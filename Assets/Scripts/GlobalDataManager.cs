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

    public bool p1dead;
    public bool p2dead;

    public float volume = 1;

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

    public void ResetData()
    {
        player1Food = 0;
        player2Food = 0;

        player1Health = 100;
        player2Health = 100;

        p1MaxHealth = 100;
        p2MaxHealth = 100;

        p1Maxdamage = 30;
        p2Maxdamage = 30;

        p1MinionCount = 0;
        p2MinionCount = 0;

        p1dead = false;
        p2dead = false;

        volume = 1;
    }
}
