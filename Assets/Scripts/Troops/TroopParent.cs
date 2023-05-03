using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class TroopParent : MonoBehaviour
{

    

    public NavMeshAgent agent;
    GameObject playerCamObj;
    Camera playerCam;
    public SphereCollider attackSphere;

    public int health;
    public int attackDamage;
    public int armor;
    public bool hasShield;
    public int chargeBonus;
    public float magicResistPercent;
    public float movementSpeed;
    public float attackDistance = 2.5f;
    public bool dealsMagicDamage = false;
    public float attackSpeed = 1.0f;
    public int maxHealth = 100;

    public bool isAlive = true;
    public bool isEnemy = false;
    public bool isAttackingCurrently = false;

    private bool isOverlappingEnemy;
    

    public GameObject troopObject;
    public TroopParent attackingUnit;
    public BattleManager battleManagerRef;
    public Animator animatorRef;

    [SerializeField]
    public Bar healthBarScriptRef;

    bool inRangeOfEnemy;



    public TroopParent()
    {
        health = 100;
        attackDamage = 5;
        armor = 30;
        hasShield = false;
        chargeBonus = 5;
        magicResistPercent = 0f;
        isAlive = true;
        movementSpeed = 3.5f;
        attackDistance = 5.0f;
    }

    public TroopParent(int healthToSet, int dmg, int armorToSet, bool isShielded, int chargeBonusToSet, float magicResistPerc, GameObject troopToSet, float moveSpeed, float attackSpeedToSet)
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

    

    public void AttackUnit(TroopParent unitToAttack)
    {

        //Once reached point of moveToPoint
        float distanceToEnemy = Vector3.Distance(troopObject.transform.position, unitToAttack.troopObject.transform.position);
        if(distanceToEnemy <= attackDistance && isAlive == true)
        {

            isAttackingCurrently = true;
            if(animatorRef != null)
            {
                animatorRef.SetBool("IsAttacking", true);

            }
            
            StartCoroutine(damageEnemy());
        }
        else
        {
            isAttackingCurrently = false;
            animatorRef.SetBool("IsAttacking", false);
            StopCoroutine(damageEnemy());
        }

    }

    public IEnumerator damageEnemy()
    {


        while(isAttackingCurrently)
        {

            if(isAlive == true)
            {

                if (dealsMagicDamage == true)
                {
                    attackingUnit.health -= attackDamage * Mathf.RoundToInt((1f - ((float)attackingUnit.magicResistPercent / 100f)));
                }
                else
                {
                    attackingUnit.health -= Mathf.RoundToInt((float)attackDamage * (1f - ((float)attackingUnit.armor / 100.0f)));

                }

                if (attackingUnit.health <= 0)
                {
                    attackingUnit.Die();
                }

                yield return new WaitForSeconds(attackSpeed);

                if (attackingUnit == null)
                {
                    isAttackingCurrently = false;
                    animatorRef.SetBool("IsAttacking", false);
                    break;
                }
                else
                {
                    float distanceToEnemy = Vector3.Distance(troopObject.transform.position, attackingUnit.troopObject.transform.position);
                    if (attackingUnit.isAlive == false || distanceToEnemy > attackDistance)
                    {
                        break;
                    }
                }
            }
            else
            {
                attackingUnit = null;
                isAttackingCurrently = false;
                break;
            }
        }
    }

    void Start()
    {
        playerCamObj = GameObject.Find("Main Camera");
        Camera playerCam = playerCamObj.GetComponent<Camera>();

        if(isEnemy == true)
        {
            gameObject.tag = "Enemy";
        }

        agent.speed = movementSpeed;

        battleManagerRef = GameObject.Find("BattleManagerObject").GetComponent<BattleManager>();

        attackSphere.radius = attackDistance;

        animatorRef = troopObject.GetComponentInChildren<Animator>(true);

        healthBarScriptRef = troopObject.GetComponentInChildren<Bar>(true);

    }

    private void Update()
    {
        //Update Health every tick
        healthBarScriptRef.UpdateWidth(health);

        //Communicating speed to Animator
        if(agent.velocity != Vector3.zero)
        {

            if(animatorRef.GetBool("IsMoving") != true)
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



        // Attacking Logic
        if (attackingUnit != null && attackingUnit.isAlive == true && isAlive == true)
        {
            

            float distToEnemy = Vector3.Distance(attackingUnit.troopObject.transform.position, troopObject.transform.position);

            if (distToEnemy <= attackDistance)
            {

                Debug.Log("We're in attack distance!!!!");

                if (isAttackingCurrently == false)
                {
                    agent.isStopped = true;

                    AttackUnit(attackingUnit);


                }
            }
            else
            {
                isAttackingCurrently = false;
                animatorRef.SetBool("IsAttacking", false);
                StopCoroutine(damageEnemy());
            }
            
        }
        else
        {
            isAttackingCurrently = false;
            animatorRef.SetBool("IsAttacking", false);
            agent.isStopped = false;
            StopCoroutine(damageEnemy());
        }


        //Troop Movement Mechanics

        if (Input.GetMouseButtonDown(0) && battleManagerRef.currentlySelectedTroop == this && battleManagerRef.isStartingPeriod == false)
        {

            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {

                agent.SetDestination(hitInfo.point);

                if(hitInfo.transform.gameObject.tag == "Enemy")
                {
                    attackingUnit = hitInfo.transform.gameObject.GetComponent<TroopParent>();
                }


            }
        }
    }


    void Die()
    {
        if(isAlive == true)
        {

            StopCoroutine(damageEnemy());

            agent.speed = 0;
            isAlive = false;
            Debug.Log("I am dead.");

            animatorRef.SetBool("IsDead", true);

            StartCoroutine(DeathCoroutine());
            if(troopObject.tag == "Enemy")
            {
                battleManagerRef.enemyTroops.Remove(this);
                if(battleManagerRef.enemyTroops.Count == 0)
                {
                    //Do if we have killed all enemy troops!
                    battleManagerRef.allEnemyTroopsDead();
                }
            }
            else
            {
                battleManagerRef.playerTroops.Remove(this);
                if(battleManagerRef.playerTroops.Count == 0)
                {
                    //Do if we have lost all of our troops!
                    battleManagerRef.allFriendlyTroopsDead();
                }
            }
            
            if(battleManagerRef.playerTroops.Count > 0 && this.tag != "Enemy")
            {
                battleManagerRef.changeCurrentSelectedTroop(battleManagerRef.playerTroops[0]);
            }

        }

    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Destroy(troopObject);
    }

}