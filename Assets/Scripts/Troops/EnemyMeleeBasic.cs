using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBasic : MeleeBasic
{

    public EnemyMeleeBasic(int healthToSet, int dmg, int armorToSet, bool isShielded, int chargeBonusToSet, float magicResistPerc, GameObject troopToSet, float moveSpeed, bool magicDamage, float attackDistanceToSet)
    : base(healthToSet, dmg, armorToSet, isShielded, chargeBonusToSet, magicResistPerc, troopToSet, moveSpeed, magicDamage, attackDistanceToSet)
    {
        health = healthToSet;
        attackDamage = dmg;
        armor = armorToSet;
        hasShield = isShielded;
        chargeBonus = chargeBonusToSet;
        magicResistPercent = magicResistPerc;
        troopObject = troopToSet;
        movementSpeed = moveSpeed;
        attackDistance = attackDistanceToSet;
    }

    void FindPlayerToAttack()
    {

        float smallestDist = 1000000.0f;
        TroopParent closestEnemy = null;

        foreach (TroopParent unit in battleManagerRef.playerTroops)
        {

            float currentDist = Vector3.Distance(unit.troopObject.transform.position, troopObject.transform.position);
            if (currentDist < smallestDist)
            {
                smallestDist = currentDist;
                closestEnemy = unit;
            }
        }
        attackingUnit = closestEnemy;

        if(closestEnemy == null)
        {
            Debug.Log("Warning: No enemies found to attack!");
        }
    }
        
    

    // Start is called before the first frame update
    void Start()
    {
        battleManagerRef = GameObject.Find("BattleManagerObject").GetComponent<BattleManager>();

        agent.speed = movementSpeed;

        FindPlayerToAttack();

        attackSphere.radius = attackDistance;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(attackingUnit != null)
        {

            if (attackingUnit.isAlive == true)
            {
                agent.SetDestination(attackingUnit.troopObject.transform.position);

                //Once reached point of moveToPoint
                float distanceToEnemy = Vector3.Distance(troopObject.transform.position, attackingUnit.troopObject.transform.position);
                Debug.Log(attackDistance);
                if (distanceToEnemy <= attackDistance)
                {

                    //This might call a bunch but once we get in the script we instantly check to see 
                    AttackUnit(attackingUnit);
                }

            }
            else
            {
                FindPlayerToAttack();
            }


        }

        
    }


}
