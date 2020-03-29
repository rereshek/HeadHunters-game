using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 4.0f;
    public int pHealth = 100;
    int pFoodCount = 0;
    public int playerID;
    public int pMaxHealth;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode escape;
    public KeyCode heal;

    public bool hasHealed;

    public bool isFighting;
    public bool escaping;

    public float escapingTime;
    private float maxEscapingTime;

    public GameObject foodDrop;

    // Start is called before the first frame update
    void Start()
    {
        hasHealed = false;
       
        gameObject.SetActive(true);
        if (playerID == 1)
        {
            pFoodCount = GlobalDataManager.Instance.player1Food;
            pHealth = GlobalDataManager.Instance.player1Health;
            pMaxHealth = GlobalDataManager.Instance.p1MaxHealth;
            AddFood(0);
            ChangeHealth(0);
            
        }
        if (playerID == 2)
        {
            pFoodCount = GlobalDataManager.Instance.player2Food;
            pHealth = GlobalDataManager.Instance.player2Health;
            pMaxHealth = GlobalDataManager.Instance.p2MaxHealth;
            AddFood(0);
            ChangeHealth(0);
        }

        isFighting = false;
        escaping = false;
        maxEscapingTime = escapingTime;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newDirection = Vector2.zero;

        //Only move if not fighting
        if (!isFighting || escaping)
        {
            if (Input.GetKey(up))
            {
                newDirection += Vector2.up;
            }
            if (Input.GetKey(right))
            {
                newDirection += Vector2.right;
            }
            if (Input.GetKey(down))
            {
                newDirection += Vector2.down;
            }
            if (Input.GetKey(left))
            {
                newDirection += Vector2.left;
            }
            
            transform.Translate(newDirection.normalized * speed * Time.deltaTime);
        }
        else if(Input.GetKey(escape) && pFoodCount >= 5 && isFighting)
        {
            ThrowFood();
            escaping = true;
        }

        if (escaping)
        {
            escapingTime -= Time.deltaTime;
            if (escapingTime <= 0f)
            {
                escapingTime = maxEscapingTime;
                escaping = false;
                isFighting = false;
            }
        }
        if(!isFighting && !escaping)
        {
            if (Input.GetKey(heal) && pFoodCount >= 15 && !hasHealed)
            {
                DropFood(15);
                Heal();
                hasHealed = true;
            }
        }
    }

    public void AddFood(int food)
    {
        FoodUIData newFoodData;
        //Add food to the players count and update the UI
        pFoodCount += food;
        newFoodData.foodCount = pFoodCount;
        newFoodData.playerID = playerID;
        EventSystem.Instance.FoodCollected(newFoodData);

    }

    public void DropFood(int food)
    {
        //Delete food and update the UI
        //Also need to stop fighting - tell the enemy as well so they pause while they pick up the food
        FoodUIData newFoodData;
        //Add food to the players count and update the UI
        pFoodCount -= food;
        newFoodData.foodCount = pFoodCount;
        newFoodData.playerID = playerID;
        EventSystem.Instance.FoodCollected(newFoodData);
    }

    private void ThrowFood()
    {
        Vector3 spawnpos = new Vector3(0, 3, 0);

        //Instantiate new food object away from player
        GameObject newFoodDrop = Instantiate(foodDrop, this.gameObject.transform, false);
        newFoodDrop.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
        spawnpos = newFoodDrop.transform.position;
        spawnpos.y += 3f;
        newFoodDrop.transform.position = spawnpos;
        DropFood(5);
    }
    public void SaveData()
    {
        if (playerID == 1)
        {
            GlobalDataManager.Instance.player1Food = pFoodCount;
            GlobalDataManager.Instance.player1Health = pHealth;
            GlobalDataManager.Instance.p1MaxHealth = pMaxHealth;
        }
        if (playerID == 2)
        {
            GlobalDataManager.Instance.player2Food = pFoodCount;
            GlobalDataManager.Instance.player2Health = pHealth;
            GlobalDataManager.Instance.p2MaxHealth = pMaxHealth;
        }

    }

    public void ChangeHealth(int health)
    {
        HealthUIData newHealthData;
        //Add food to the players count and update the UI
        pHealth -= health;
        newHealthData.health = pHealth;
        newHealthData.playerID = playerID;
        newHealthData.maxHealth = pMaxHealth;
        EventSystem.Instance.HealthChanged(newHealthData);
    }

    public void Heal()
    {
        pHealth += 50;
        if (pHealth > pMaxHealth)
        {
            pHealth = pMaxHealth;
        }
        HealthUIData newHealthData;
              
        newHealthData.health = pHealth;
        newHealthData.playerID = playerID;
        newHealthData.maxHealth = pMaxHealth;
        EventSystem.Instance.HealthChanged(newHealthData);
    }
}
