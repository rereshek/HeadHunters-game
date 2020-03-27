using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 4.0f;
    public int pHealth = 100;
    int pFoodCount = 0;
    public int playerID;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        if (playerID == 1)
        {
            pFoodCount = GlobalDataManager.Instance.player1Food;
            pHealth = GlobalDataManager.Instance.player1Health;
            AddFood(0);
        }
        if (playerID == 2)
        {
            pFoodCount = GlobalDataManager.Instance.player2Food;
            pHealth = GlobalDataManager.Instance.player2Health;
            AddFood(0);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newDirection = Vector2.zero;

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
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    EventSystem.Instance.FoodCollected(pFoodCount);
        //    pFoodCount++;
        //}
        transform.Translate(newDirection.normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("Hit");
        }
        if (gameObject.CompareTag("Edge"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
    }

    public void SaveData()
    {
        if (playerID == 1)
        {
            GlobalDataManager.Instance.player1Food = pFoodCount;
            GlobalDataManager.Instance.player1Health = pHealth;
        }
        if (playerID == 2)
        {
            GlobalDataManager.Instance.player2Food = pFoodCount;
            GlobalDataManager.Instance.player2Health = pHealth;
        }

    }
}
