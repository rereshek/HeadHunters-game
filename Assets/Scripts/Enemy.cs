using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public float enemySpeed = 2.0f;
    public int eHealth = 250;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        gameObject.GetComponent<Rigidbody2D>().drag = 0;
        if (col.gameObject.CompareTag("Player1"))
        {
            //Debug.Log("Player detected");            
            gameObject.GetComponent<Rigidbody2D>().AddForce((player1.transform.position - transform.position).normalized * enemySpeed);
        }
        if (col.gameObject.CompareTag("Player2"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((player2.transform.position - transform.position).normalized * enemySpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("lost player");
        gameObject.GetComponent<Rigidbody2D>().drag = 6;
    }

    /*private void FixedUpdate()
    {
        Collider2D myCollider = gameObject.GetComponent<Collider2D>();
        int numColliders = 2;
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);

        if (colliderCount == numColliders)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * enemySpeed);
        }
        if (colliderCount < numColliders)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }

    }*/

}
