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

    public BattleUIManager UIReference;


    public void changeCurrentSelectedTroop(TroopParent newSelectedTroop)
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

        UIReference.endOfGamePanelRef.SetActive(true);
        UIReference.winTextRef.SetActive(false);
        UIReference.loseTextRef.SetActive(true);
    }

    public void allEnemyTroopsDead()
    {
        //Calls when all enemy troops are dead and we've won the battle!

        int moneyToGive = Random.Range(100, 250);
        Global.money += moneyToGive;

        Debug.Log(Global.money);

        Global.troops = playerTroops.Count;

        UIReference.endOfGamePanelRef.SetActive(true);
        UIReference.winTextRef.SetActive(true);
        UIReference.loseTextRef.SetActive(false);
        UIReference.goldEarnedTextRef.text = "Gold Earned: " + moneyToGive.ToString();


    }

    IEnumerator StartOfGameWait()
    {

        yield return new WaitForSeconds(15f);

        isStartingPeriod = false;
        UIReference.warmupPanelRef.SetActive(false);
    }






    // Start is called before the first frame update
    void Start()
    {

        UIReference = GameObject.Find("BattleViewUI").GetComponent<BattleUIManager>();

        SpawnPointController friendlySpawnPointRef = GameObject.Find("FriendlySpawnPoints").GetComponent<SpawnPointController>();
        SpawnPointController enemySpawnPointRef = GameObject.Find("EnemySpawnPoints").GetComponent<SpawnPointController>();

        //Spawn Player's Troops According to troops Global variable
        for (int i = 0; i < Global.troops; i++)
        {
            
            Vector3 positionToSpawnAt = friendlySpawnPointRef.spawnPoints[i].transform.position;
            Quaternion rotationToSpawnAt = friendlySpawnPointRef.spawnPoints[i].transform.rotation;

            playerTroops.Add(Instantiate(troopToSpawn, positionToSpawnAt, rotationToSpawnAt).GetComponent<TroopParent>());
            playerTroops[i].armor += Global.armorToBuff;
            playerTroops[i].attackDamage += Global.attackDamageToBuff;
        }


        //Spawn Enemy Troops According to troops Global variable + 1 or - 1
        int numEnemyTroopsToSpawn;

        if(playerTroops.Count == 1)
        {
            numEnemyTroopsToSpawn = 1;
        }
        else
        {
            numEnemyTroopsToSpawn = playerTroops.Count;
        }

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
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if(playerTroops.Count > 0)
            {
                if (playerTroops.IndexOf(currentlySelectedTroop) == 0)
                {
                    changeCurrentSelectedTroop(playerTroops[playerTroops.Count - 1]);
                }
                else
                {
                    changeCurrentSelectedTroop(playerTroops[playerTroops.IndexOf(currentlySelectedTroop) - 1]);
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(playerTroops.Count > 0)
            {
                if (playerTroops.IndexOf(currentlySelectedTroop) == playerTroops.Count - 1)
                {
                    changeCurrentSelectedTroop(playerTroops[0]);
                }
                else
                {
                    changeCurrentSelectedTroop(playerTroops[playerTroops.IndexOf(currentlySelectedTroop) + 1]);
                }
            }


        }


    }
}
