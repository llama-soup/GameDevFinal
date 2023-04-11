using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

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
    public float attackDistance = 5.0f;

    public bool isAlive = true;
    public bool isEnemy = false;
    public bool isAttackingCurrently = false;

    private bool isOverlappingEnemy;
    

    public GameObject troopObject;
    public TroopParent attackingUnit;
    public BattleManager battleManagerRef;


    bool inRangeOfEnemy;

    private float attackSpeed = 1.0f;

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
    }

    public TroopParent(int healthToSet, int dmg, int armorToSet, bool isShielded, int chargeBonusToSet, float magicResistPerc, GameObject troopToSet, float moveSpeed)
    {
        health = healthToSet;
        attackDamage = dmg;
        armor = armorToSet;
        hasShield = isShielded;
        chargeBonus = chargeBonusToSet;
        magicResistPercent = magicResistPerc;
        troopObject = troopToSet;
        movementSpeed = moveSpeed;

    }

    

    void AttackUnit(TroopParent unitToAttack)
    {
        agent.SetDestination(unitToAttack.troopObject.transform.position);


        //Once reached point of moveToPoint
        attackingUnit = unitToAttack;
        float distanceToEnemy = Vector3.Distance(troopObject.transform.position, unitToAttack.troopObject.transform.position);
        if(distanceToEnemy <= attackDistance)
        {
            isAttackingCurrently = true;
            StartCoroutine(damageEnemy());
        }
        

    }

    IEnumerator damageEnemy()
    {

        while(isAttackingCurrently)
        {
            attackingUnit.health -= attackDamage;

            yield return new WaitForSeconds(attackSpeed);

            float distanceToEnemy = Vector3.Distance(troopObject.transform.position, attackingUnit.troopObject.transform.position);
            if (attackingUnit.isAlive == false || distanceToEnemy > attackDistance)
            {
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

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {

                agent.SetDestination(hitInfo.point); 
            }
        }
    }


    void Die()
    {
        Debug.Log("I am dead.");
        Destroy(troopObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }



}