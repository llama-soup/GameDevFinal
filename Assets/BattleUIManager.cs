using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUIManager : MonoBehaviour
{

    BattleManager battleManagerRef;

    public GameObject healthObjRef;
    TextMeshProUGUI healthTextRef;
    public GameObject inCombatObjRef;


    public GameObject endOfGamePanelRef;
    public GameObject winTextRef;
    public GameObject loseTextRef;
    public TextMeshProUGUI goldEarnedTextRef;

    public GameObject warmupPanelRef;

    // Start is called before the first frame update
    void Start()
    {
        battleManagerRef = GameObject.Find("BattleManagerObject").GetComponent<BattleManager>();
        healthTextRef = healthObjRef.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(battleManagerRef.currentlySelectedTroop.health <= 0)
        {
            healthTextRef.text = "Dead";
        }
        else
        {
            healthTextRef.text = battleManagerRef.currentlySelectedTroop.health.ToString();
        }

        

        if (battleManagerRef.currentlySelectedTroop.isAttackingCurrently == true)
        {
            inCombatObjRef.SetActive(true);
        }
        else
        {
            inCombatObjRef.SetActive(false);
        }
    }
}
