using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public List<TroopParent> playerTroops = new List<TroopParent>();
    public List<TroopParent> enemyTroops = new List<TroopParent>();

    public TroopParent currentlySelectedTroop;


    void changeCurrentSelectedTroop(TroopParent newSelectedTroop)
    {
        Material currentTroopMat;
        if(currentlySelectedTroop != null)
        {
            currentTroopMat = currentlySelectedTroop.GetComponent<Renderer>().material;
            currentTroopMat.color = new UnityEngine.Color(currentTroopMat.color.r, currentTroopMat.color.g, currentTroopMat.color.b, 0.0f);
        }


        currentlySelectedTroop = newSelectedTroop;

        currentTroopMat = currentlySelectedTroop.GetComponent<Renderer>().material;
        currentTroopMat.color = new UnityEngine.Color(currentTroopMat.color.r, currentTroopMat.color.g, currentTroopMat.color.b, 0.05f);
    }


    void allFriendlyTroopsDead()
    {

    }

    void allEnemyTroopsDead()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        changeCurrentSelectedTroop(playerTroops[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
