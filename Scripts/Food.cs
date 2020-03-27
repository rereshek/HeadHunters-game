using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int foodValue;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Work out who picked up the food (based on the collision object and update the food value
        collision.gameObject.GetComponent<Player>().AddFood(foodValue);
        //EventSystem.Instance.FoodCollected(foodValue);
        Debug.Log("Picked up food worth " + foodValue);
        Destroy(this.gameObject);
    }
}
