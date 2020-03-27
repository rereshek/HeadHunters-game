using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControl : MonoBehaviour
{
    // Start is called before the first frame update
    private bool pAttacked = false;
    private bool eAttacked = false;

    public int eDamage;
    public int pDamage;

    public int attackDist = 2;

    public Player player;
    public Enemy foe;

    void Start()
    {
        player = FindObjectOfType<Player>();
        foe = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.magnitude - foe.transform.position.magnitude) <= attackDist)
        {
            while(player.pHealth > 0 && foe.eHealth > 0)
            {
                Attack(player, foe);
            }
            if (player.pHealth <= 0)
            {
                Debug.Log("Player Died");
            }
            if (foe.eHealth <= 0)
            {
                Debug.Log("Enemy Died");
            }
        }
    }

    void Attack (Player player, Enemy foe)
    {
        if(pAttacked == false && eAttacked == true)
        {
            pDamage = playerRollAttack(pDamage);
            foe.eHealth = foe.eHealth - pDamage;
            pAttacked = true;
        }
        else if (pAttacked == true && eAttacked == false)
        {
            eDamage = foeRollAttack(eDamage);
            player.pHealth = player.pHealth - eDamage;
            pAttacked = false;
        }
    }

    int playerRollAttack (int damage)
    {
        damage = Random.Range(1, 12);
        return damage;
    }

    int foeRollAttack (int damage)
    {
        damage = Random.Range(1, 20);
        return damage;
    }
}
