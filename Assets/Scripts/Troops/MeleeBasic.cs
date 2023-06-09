using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBasic : TroopParent

{

    public MeleeBasic(int healthToSet, int dmg, int armorToSet, bool isShielded, int chargeBonusToSet, float magicResistPerc, GameObject troopToSet, float moveSpeed, float attackSpeedToSet)
        : base(healthToSet,dmg,armorToSet,isShielded,chargeBonusToSet,magicResistPerc, troopToSet, moveSpeed, attackSpeedToSet)
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


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
