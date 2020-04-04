using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 4.0f;
    public int pHealth;
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

    public bool isFighting = false;
    public int isFightingFoeID;
    public bool escaping;

    public float escapingTime;
    private float maxEscapingTime;

    public GameObject foodDrop;

    public int pMaxDamage;

    public int pMinionCount;

    public Animator playerAnim;

    public bool dead;
    private bool playerUIUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
        
        hasHealed = false;
        gameObject.SetActive(true);
        dead = false;
     
        if (playerID == 1)
        {
            pFoodCount = GlobalDataManager.Instance.player1Food;
            pHealth = GlobalDataManager.Instance.player1Health;
            pMaxHealth = GlobalDataManager.Instance.p1MaxHealth;
            pMaxDamage = GlobalDataManager.Instance.p1Maxdamage;
            pMinionCount = GlobalDataManager.Instance.p1MinionCount;
            dead = GlobalDataManager.Instance.p1dead;

            if (dead)
            {
                AddFood(Mathf.FloorToInt(pFoodCount / 4) * -1);
                ChangeHealth(Mathf.FloorToInt(pMaxHealth / 2));
            }
            else
            {
                AddFood(0);
                ChangeHealth(0);
            }

        }
        if (playerID == 2)
        {
            pFoodCount = GlobalDataManager.Instance.player2Food;
            pHealth = GlobalDataManager.Instance.player2Health;
            pMaxHealth = GlobalDataManager.Instance.p2MaxHealth;
            pMaxDamage = GlobalDataManager.Instance.p2Maxdamage;
            pMinionCount = GlobalDataManager.Instance.p2MinionCount;
            dead = GlobalDataManager.Instance.p2dead;

            if (dead)
            {
                AddFood(Mathf.FloorToInt(pFoodCount / 2) * -1);
                ChangeHealth(Mathf.FloorToInt(pMaxHealth / 2));
            }
            else
            {
                AddFood(0);
                ChangeHealth(0);
            }
        }

        SetFighting(false);
        escaping = false;
        maxEscapingTime = escapingTime;
        MinionCount(pFoodCount);

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerUIUpdated)
        {
            AddFood(0);
            ChangeHealth(0);
            playerUIUpdated = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            EventSystem.Instance.PauseGame();
        }
        Vector2 newDirection = Vector2.zero;

        //Only move if not fighting
        Vector3 currentScale = transform.localScale;
        bool isWalking = false;


        //Only move if not fighting
        if (!isFighting || escaping)
        {
            if (Input.GetKey(up))
            {
                newDirection += Vector2.up;
                isWalking = true;
            }
            if (Input.GetKey(right))
            {
                newDirection += Vector2.right;
                currentScale.x = Mathf.Abs(currentScale.x);
                isWalking = true;
            }
            if (Input.GetKey(down))
            {
                newDirection += Vector2.down;
                isWalking = true;
            }
            if (Input.GetKey(left))
            {
                newDirection += Vector2.left;
                currentScale.x = Mathf.Abs(currentScale.x) * -1f;
                isWalking = true;
            }
        }
        if (Input.GetKey(escape) && pFoodCount >= 5 && isFighting)
        {
            ThrowFood();
            escaping = true;
            StartStopFightingAnimation(false);
        }

        if (escaping)
        {
            escapingTime -= Time.deltaTime;
            if (escapingTime <= 0f)
            {
                escapingTime = maxEscapingTime;
                escaping = false;
                SetFighting(false);
            }
        }
        transform.localScale = currentScale;
        transform.Translate(newDirection.normalized * speed * Time.deltaTime);
        playerAnim.SetBool("isWalking", isWalking);
        if (!isFighting && !escaping)
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
            MinionCount(pFoodCount);
            GlobalDataManager.Instance.player1Food = pFoodCount;
            GlobalDataManager.Instance.player1Health = pHealth;
            //GlobalDataManager.Instance.p1MaxHealth = pMaxHealth;
            GlobalDataManager.Instance.p1Maxdamage = pMaxDamage;
            GlobalDataManager.Instance.p1MinionCount = pMinionCount;
            dead = GlobalDataManager.Instance.p1dead;
        }
        if (playerID == 2)
        {
            MinionCount(pFoodCount);
            GlobalDataManager.Instance.player2Food = pFoodCount;
            GlobalDataManager.Instance.player2Health = pHealth;
            //GlobalDataManager.Instance.p2MaxHealth = pMaxHealth;
            GlobalDataManager.Instance.p2Maxdamage = pMaxDamage;
            GlobalDataManager.Instance.p2MinionCount = pMinionCount;
            dead = GlobalDataManager.Instance.p2dead;
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

    public void MinionCount(int foodCount)
    {
        float calculate = foodCount / 5;
        int result = Mathf.FloorToInt(calculate);
        pMinionCount = result;
        pMaxDamage += pMinionCount*2;
    }

    public void SetFighting(bool isPlayerFighting, float direction = 0f, int foeID = 0)
    {
        Vector3 localScale = transform.localScale;
        isFighting = isPlayerFighting;
        isFightingFoeID = foeID;
        StartStopFightingAnimation(isPlayerFighting);
        if (direction != 0f)
        {
            localScale.x = Mathf.Abs(localScale.x) * direction;
            transform.localScale = localScale;
        }
    }

    private void StartStopFightingAnimation(bool fighting)
    {
        playerAnim.SetBool("isFighting", fighting);
    }

    public void PlayerDies ()
    {
        pHealth = Mathf.FloorToInt(pMaxHealth/4)*3;
        isFighting = false;
        dead = true;
        pFoodCount = Mathf.FloorToInt(pFoodCount / 2);
        SaveData();
        EventSystem.Instance.PlayerDead(playerID);
        gameObject.SetActive(false);
    }
    
}
