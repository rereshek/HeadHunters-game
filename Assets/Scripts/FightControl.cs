using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    

    public int attackDist = 4;

    private Player player1;
    private Player player2;

    public Enemy foe;

    public float attackWait;
    private float maxAttackWait;

    private EnemyUIData enemyUIData;
    public ParticleSystem deathBlood;
    public GameObject enemyBody;
    private bool bled = false;

    void Start()
    {
        enemyBody.SetActive(true);
        foe = GetComponentInParent<Enemy>();
        maxAttackWait = attackWait;
        enemyUIData.enemyID = foe.enemyID;
        enemyUIData.showUI = false;


        player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.pHealth <= 0)
        {
            player1.PlayerDies();
            if (!player2.isFighting)
            {
                foe.eState = EnemyState.Idle;
                enemyUIData.showUI = false;
                EventSystem.Instance.ShowEnemyUI(enemyUIData);
            }
        }
        if (player2.pHealth <= 0)
        {
            player2.PlayerDies();
            if (!player1.isFighting)
            {
                foe.eState = EnemyState.Idle;
                enemyUIData.showUI = false;
                EventSystem.Instance.ShowEnemyUI(enemyUIData);
            }
        }
        if (foe.eHealth <= 0)
        {
            foe.eState = EnemyState.Dead;
            enemyUIData.showUI = false;
            EventSystem.Instance.ShowEnemyUI(enemyUIData);
        }

        if (foe.eState == EnemyState.Fighting)
        {
            //HealthUIData newHealthData;
            //Add food to the players count and update the UI
            enemyUIData.eHealth = foe.eHealth;
            enemyUIData.eMaxHealth = foe.eMaxHealth;


            enemyUIData.showUI = true;
            EventSystem.Instance.ShowEnemyUI(enemyUIData);

            //EventSystem.Instance.HealthChanged(newHealthData);
            attackWait -= Time.deltaTime;
            if (attackWait <= 0f)
            {
                attackWait = maxAttackWait;
                if (player1.isFighting)
                {
                    Attack(player1, foe);
                }
                if (player2.isFighting)
                {
                    Attack(player2, foe);
                }
            }
        }
        if (foe.eState == EnemyState.Dead)
        {
            if (!bled)
            {
                enemyBody.SetActive(false);
                deathBlood.gameObject.SetActive(true);
                deathBlood.Play();
                ParticleSystem.EmissionModule em = deathBlood.emission;
                em.enabled = true;
                bled = true;
            }

            StartCoroutine(EnemyDeathAnim());

            if (player1.isFighting && player1.gameObject.activeSelf && player1.isFightingFoeID == foe.enemyID)
            {
                player1.SetFighting(false);
            }
            if (player2.isFighting && player2.gameObject.activeSelf && player2.isFightingFoeID == foe.enemyID)
            {
                player2.SetFighting(false);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        float playerDirection = 0f;

        if (collision.CompareTag("Player1"))
        {
            foe.eState = EnemyState.Fighting;
            if (player1.transform.position.x < transform.position.x)
            {
                playerDirection = 1f;
            }
            else
            {
                playerDirection = -1f;
            }
            player1.SetFighting(true, playerDirection, foe.enemyID);
            //enemyFighting = true;
        }
        if (collision.gameObject.tag == "Player2")
        {
            foe.eState = EnemyState.Fighting;
            if (player2.transform.position.x < transform.position.x)
            {
                //Player is to left of enemy, scale should be 1
                playerDirection = 1f;
            }
            else
            {
                //Player is to right of enemy, scale should be -1
                playerDirection = -1f;
            }
            player2.SetFighting(true, playerDirection, foe.enemyID);
            //enemyFighting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            if (player2.isFighting == false)
            {
                foe.eState = EnemyState.Idle;
            }
        }
        if (collision.CompareTag("Player2"))
        {
            if (player1.isFighting == false)
            {
                foe.eState = EnemyState.Idle;
            }
        }
    }

    void Attack(Player player, Enemy foe)
    {

        int fightDamage;
        fightDamage = playerRollAttack(player.pMaxDamage);
        ChangeHealth(fightDamage);


        fightDamage = foeRollAttack(foe.eMaxDamage);
        player.ChangeHealth(fightDamage);

    }

    int playerRollAttack(int damage)
    {
        int damageDone;
        damageDone = Random.Range(5, damage);
        return damageDone;
    }

    int foeRollAttack(int damage)
    {
        int damageDone;
        damageDone = Random.Range(1, damage);
        return damageDone;
    }

    private void ChangeHealth(int health)
    {
        HealthUIData foeHealthData;
        //Add food to the players count and update the UI
        //Debug.Log("Changing health for foe");
        foe.eHealth = foe.eHealth - health;
        foeHealthData.health = foe.eHealth;
        foeHealthData.playerID = foe.enemyID;
        foeHealthData.maxHealth = foe.eMaxHealth;
        EventSystem.Instance.HealthChanged(foeHealthData);
    }

    private IEnumerator EnemyDeathAnim()
    {
        yield return new WaitForSeconds(1f);
        GameObject newFood = Instantiate(RandomFoodGen.Instance.getRandomFood(), transform.position, Quaternion.identity);
        foe.gameObject.SetActive(false);
        enemyUIData.showUI = false;
        EventSystem.Instance.ShowEnemyUI(enemyUIData);
    }

}
