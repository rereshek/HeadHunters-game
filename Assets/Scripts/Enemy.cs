using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Idle, ChasingP1, ChasingP2, Fighting, Dead }

public class Enemy : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public float enemySpeed = 2.0f;
    public int eHealth = 250;
    public EnemyState eState;
    public int enemyID;
    public int eMaxHealth;

    public float distanceToP1;
    public float distanceToP2;

    // Start is called before the first frame update
    void Start()
    {
        eState = EnemyState.Idle;
        distanceToP1 = Mathf.Infinity;
        distanceToP2 = Mathf.Infinity;
        eMaxHealth = eHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("eState: " + eState.ToString());
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //gameObject.GetComponent<Rigidbody2D>().drag = 0;
        float distanceToTarget = (col.gameObject.transform.position - transform.position).magnitude;
        if (col.gameObject.CompareTag("Player1"))
        {
            distanceToP1 = distanceToTarget;
            //Debug.Log("Player detected");            
            //gameObject.GetComponent<Rigidbody2D>().AddForce((player1.transform.position - transform.position).normalized * enemySpeed);
            if (distanceToP1 < distanceToP2 && eState != EnemyState.Fighting && !player1.escaping)
            {
                transform.position = Vector3.MoveTowards(transform.position, col.gameObject.transform.position, enemySpeed * Time.deltaTime);
                eState = EnemyState.ChasingP1;
            }

        }
        if (col.gameObject.CompareTag("Player2"))
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce((player2.transform.position - transform.position).normalized * enemySpeed);
            distanceToP2 = distanceToTarget;
            if (distanceToP2 < distanceToP1 && eState != EnemyState.Fighting && !player2.escaping)
            {
                transform.position = Vector3.MoveTowards(transform.position, col.gameObject.transform.position, enemySpeed * Time.deltaTime);
                eState = EnemyState.ChasingP2;
                //Debug.Log("eState: " + eState.ToString());
            }

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            distanceToP1 = Mathf.Infinity;
        }
        if (col.gameObject.CompareTag("Player2"))
        {
            distanceToP2 = Mathf.Infinity;
        }
        if (distanceToP1 == Mathf.Infinity && distanceToP2 == Mathf.Infinity)
        {
            eState = EnemyState.Idle;
        }
    }

}
