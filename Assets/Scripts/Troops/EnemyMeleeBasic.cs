using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBasic : MeleeBasic
{

    public EnemyMeleeBasic(int healthToSet, int dmg, int armorToSet, bool isShielded, int chargeBonusToSet, float magicResistPerc, GameObject troopToSet, float moveSpeed, float attackSpeedToSet)
    : base(healthToSet, dmg, armorToSet, isShielded, chargeBonusToSet, magicResistPerc, troopToSet, moveSpeed, attackSpeedToSet)
    {
        health = healthToSet;
        attackDamage = dmg;
        armor = armorToSet;
        hasShield = isShielded;
        chargeBonus = chargeBonusToSet;
        magicResistPercent = magicResistPerc;
        troopObject = troopToSet;
        movementSpeed = moveSpeed;
        attackSpeed = attackSpeedToSet;
    }

    IEnumerator checkNearbyPlayerTroops()
    {

            while (battleManagerRef.playerTroops.Count > 0)
            {

                yield return new WaitForSeconds(3.5f);

                if(battleManagerRef.isStartingPeriod == false)
            {
                FindPlayerToAttack();
            }


            }



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
        animatorRef = troopObject.GetComponentInChildren<Animator>(true);

        agent.speed = movementSpeed;

        attackSphere.radius = attackDistance;

        StartCoroutine(checkNearbyPlayerTroops());

        healthBarScriptRef = troopObject.GetComponentInChildren<Bar>(true);



    }

    // Update is called once per frame
    void Update()
    {

        healthBarScriptRef.UpdateWidth(health);

        //Communicating speed to Animator
        if (agent.velocity != Vector3.zero)
        {

            if (animatorRef.GetBool("IsMoving") != true)
            {
                animatorRef.SetBool("IsMoving", true);
            }
        }
        else
        {
            //If current speed is 0 and we're not already known stopped, then say we're stopped.
            if (animatorRef.GetBool("IsMoving") != false)
            {
                animatorRef.SetBool("IsMoving", false);
            }
        }


        // Attack Logic
        if (attackingUnit != null)
        {

            if (attackingUnit.isAlive == true)
            {
                float distToEnemy = Vector3.Distance(attackingUnit.troopObject.transform.position, troopObject.transform.position);

                if (distToEnemy <= attackDistance)
                {
                    if(isAttackingCurrently == false)
                    {
                        agent.SetDestination(troopObject.transform.position);

                        AttackUnit(attackingUnit);
                    }
                }
                else
                {
                    isAttackingCurrently = false;
                }
                if(isAttackingCurrently == false)
                {
                    agent.SetDestination(attackingUnit.troopObject.transform.position);
                }
                

                //Once reached point of moveToPoint
                float distanceToEnemy = Vector3.Distance(troopObject.transform.position, attackingUnit.troopObject.transform.position);

            }
            else
            {
                FindPlayerToAttack();
            }


        }
        else
        {
            if(battleManagerRef.isStartingPeriod == false)
            {
                FindPlayerToAttack();
            }
            
        }

        
    }


}
