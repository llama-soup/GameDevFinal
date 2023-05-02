using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public List<TroopParent> playerTroops = new List<TroopParent>();
    public List<TroopParent> enemyTroops = new List<TroopParent>();

    public TroopParent currentlySelectedTroop;

    public bool isStartingPeriod = true;

    public GameObject troopToSpawn;
    public GameObject enemyTroopToSpawn;


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


    public void allFriendlyTroopsDead()
    {
        //Calls when all player troops are dead and we've lost the battle

        Global.troops = 1;
    }

    public void allEnemyTroopsDead()
    {
        //Calls when all enemy troops are dead and we've won the battle!

        int moneyToGive = Random.Range(100, 250);
        Global.money += moneyToGive;

        Global.troops = playerTroops.Count;


    }

    IEnumerator StartOfGameWait()
    {

        yield return new WaitForSeconds(15f);

        isStartingPeriod = false;
    }






    // Start is called before the first frame update
    void Start()
    {
        SpawnPointController friendlySpawnPointRef = GameObject.Find("FriendlySpawnPoints").GetComponent<SpawnPointController>();
        SpawnPointController enemySpawnPointRef = GameObject.Find("EnemySpawnPoints").GetComponent<SpawnPointController>();

        //Spawn Player's Troops According to troops Global variable
        for (int i = 0; i < Global.troops; i++)
        {
            
            Vector3 positionToSpawnAt = friendlySpawnPointRef.spawnPoints[i].transform.position;
            Quaternion rotationToSpawnAt = friendlySpawnPointRef.spawnPoints[i].transform.rotation;

            playerTroops.Add(Instantiate(troopToSpawn, positionToSpawnAt, rotationToSpawnAt).GetComponent<TroopParent>());
        }


        //Spawn Enemy Troops According to troops Global variable + 1 or - 1
        int numEnemyTroopsToSpawn = Random.Range(Global.troops - 1, Global.troops + 1);

        for (int i = 0; i < numEnemyTroopsToSpawn; i++)
        {
            Vector3 positionToSpawnAt = enemySpawnPointRef.spawnPoints[i].transform.position;
            Quaternion rotationToSpawnAt = enemySpawnPointRef.spawnPoints[i].transform.rotation;

            enemyTroops.Add(Instantiate(enemyTroopToSpawn, positionToSpawnAt, rotationToSpawnAt).GetComponent<TroopParent>());
        }




        changeCurrentSelectedTroop(playerTroops[0]);
        StartCoroutine(StartOfGameWait());
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
