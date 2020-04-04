using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int foodValue;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag.Contains("Player"))
        {
            //Work out who picked up the food (based on the collision object and update the food value
            collision.gameObject.GetComponent<Player>().AddFood(foodValue);
            Destroy(this.gameObject);
        }
    }
}
