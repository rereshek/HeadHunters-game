using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool pAttacked = false;
    //private bool eAttacked = false;

    public int eDamage;
    public int pDamage;

    public int attackDist = 2;

    public Player player1;
    public Player player2;

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
        //player = FindObjectOfType<Player>();
        foe = GetComponent<Enemy>();
        maxAttackWait = attackWait;
        enemyUIData.enemyID = foe.enemyID;
        enemyUIData.showUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool enemyFighting = false;

        if ((player1.transform.position - transform.position).magnitude <= attackDist && !player1.escaping)
        {
            foe.eState = EnemyState.Fighting;
            player1.isFighting = true;
            enemyFighting = true;
        }
        if ((player2.transform.position - transform.position).magnitude <= attackDist && !player2.escaping)
        {
            foe.eState = EnemyState.Fighting;
            player2.isFighting = true;
            enemyFighting = true;
        }
        if (!enemyFighting && foe.eState == EnemyState.Fighting)
        {
            foe.eState = EnemyState.Idle;
            enemyUIData.showUI = false;
            EventSystem.Instance.ShowEnemyUI(enemyUIData);
        }
        if (player1.pHealth <= 0)
        {
            player1.gameObject.SetActive(false);
            player1.isFighting = false;
            if(!player2.isFighting)
            {
                foe.eState = EnemyState.Idle;
                enemyUIData.showUI = false;
                EventSystem.Instance.ShowEnemyUI(enemyUIData);
            }
        }
        if (player2.pHealth <= 0)
        {
            player2.gameObject.SetActive(false);
            player2.isFighting = false;
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
            enemyUIData.showUI = true;
            EventSystem.Instance.ShowEnemyUI(enemyUIData);
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
            
            if(player1.isFighting && player1.gameObject.activeSelf)
            {
                player1.isFighting = false;
            }
            if (player2.isFighting && player2.gameObject.activeSelf)
            {
                player2.isFighting = false;
            }
           
        }
    }

    void Attack (Player player, Enemy foe)
    {
        
        int fightDamage;
        fightDamage = playerRollAttack(pDamage);
        ChangeHealth(fightDamage);
        
        fightDamage = foeRollAttack(eDamage);
        player.ChangeHealth(fightDamage);
        
    }

    int playerRollAttack (int damage)
    {
        int damageDone;
        damageDone = Random.Range(5, damage);
        return damageDone;
    }

    int foeRollAttack (int damage)
    {
        int damageDone;
        damageDone = Random.Range(1, damage);
        return damageDone;
    }

    private void ChangeHealth(int health)
    {
        HealthUIData foeHealthData;
        //Add food to the players count and update the UI
        Debug.Log("Changing health for foe");
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
        gameObject.SetActive(false);
        enemyUIData.showUI = false;
        EventSystem.Instance.ShowEnemyUI(enemyUIData);
    }
}
