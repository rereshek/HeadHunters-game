using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Idle, ChasingP1, ChasingP2, Fighting, Dead }

public class Enemy : MonoBehaviour
{
    private Player player1;
    private Player player2;

    public float enemySpeed = 2.0f;
    public int eHealth = 250;
    public EnemyState eState;
    public int enemyID;
    public int eMaxHealth = 250;

    public float distanceToP1;
    public float distanceToP2;

    public int eMaxDamage;

    public Animator enemyAnim;
    public GameObject enemyGFX;
    // Start is called before the first frame update
    void Start()
    {
        eState = EnemyState.Idle;
        distanceToP1 = Mathf.Infinity;
        distanceToP2 = Mathf.Infinity;
        eMaxHealth = eHealth;

        player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();

        enemyAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 enemyScale = enemyGFX.transform.localScale;
        float localScale = enemyGFX.transform.localScale.x;
        switch (eState)
        {
            case EnemyState.Fighting:
                enemyAnim.SetBool("Fighting", true);
                enemyAnim.SetBool("Walking", false);
                break;
            case EnemyState.ChasingP1:
                enemyAnim.SetBool("Fighting", false);
                enemyAnim.SetBool("Walking", true);
                break;
            case EnemyState.ChasingP2:
                enemyAnim.SetBool("Fighting", false);
                enemyAnim.SetBool("Walking", true);
                break;
            case EnemyState.Dead:
                enemyAnim.SetBool("Fighting", false);
                enemyAnim.SetBool("Walking", false);
                break;
            default:
                enemyAnim.SetBool("Fighting", false);
                enemyAnim.SetBool("Walking", false);
                break;
        }
        if (eState == EnemyState.ChasingP1)
        {
            if (player1.transform.position.x > gameObject.transform.position.x)
            {
                localScale = 1f;
            }
            else
            {
                localScale = -1f;
            }
        }
        if (eState == EnemyState.ChasingP2)
        {
            if (player2.transform.position.x > gameObject.transform.position.x)
            {
                localScale = 1f;
            }
            else
            {
                localScale = -1f;
            }
        }
        enemyScale.x = localScale;
        enemyGFX.transform.localScale = enemyScale;
    }

    void OnTriggerStay2D(Collider2D col)
    {
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
