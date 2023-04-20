using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class TileMapPlacement : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject academy;
    public GameObject armory;
    public GameObject factory;
    public GameObject farm;
    public GameObject market;
    public GameObject mine;
    public GameObject transparentObject; // Add a reference to the transparent object here
    public int selection = 1;

    public int academy_cost = 100;
    public int armory_cost = 100;
    public int factory_cost = 200;
    public int farm_cost = 200;
    public int market_cost = 200;
    public int mine_cost = 100;

    public int cost = 100;

    private Camera mainCamera;
    private GameObject currentObject;
    private GameObject lastObjectPlaced;
    private Vector3Int lastCellPosition;
    //private float rotationAngle = 0f;
    private HashSet<Vector3Int> placedPositions = new HashSet<Vector3Int>(); // Keep track of the positions where objects have been placed


    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.LookAt(Vector3.zero);
        Global.money = 200;
        Global.troops = 5;
    }

    private void Update()
    {
        if (currentObject == null) // Check if we need to create a new transparent object
        {
            currentObject = Instantiate(transparentObject); // Create the transparent object
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //hit.transform returns the transform of the object that is hit
            //If this is not tagged with "plane", then just return
            if (hit.transform.CompareTag("plane")){
                Vector3Int hitInt = Vector3Int.FloorToInt(hit.point);
            Vector3Int cellPosition = tilemap.WorldToCell(hit.point);
            Vector3 centerPosition = tilemap.GetCellCenterWorld(cellPosition);

            currentObject.transform.position = centerPosition; // Move the transparent object to the cursor position

            // // Rotate the object to be placed based on input
            // //rotationAngle += Input.GetAxis("RotateObject") * 90f;
            // if (Input.GetKey(KeyCode.O))   
            // {
            //     rotationAngle -= 90f * Time.deltaTime;
            //     currentObject.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     academy.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     armory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     factory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     farm.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     market.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     mine.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // }
            // if (Input.GetKey(KeyCode.P)) 
            // {
            //     rotationAngle += 90f * Time.deltaTime;
            //     currentObject.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     academy.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     armory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     factory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     farm.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     market.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            //     mine.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            // }

            if(Input.GetKeyDown("1")){
                selection = 1;
                cost = academy_cost;
            }
            if(Input.GetKeyDown("2")){
                selection = 2;
                cost = armory_cost;
            }
            if(Input.GetKeyDown("3")){
                selection = 3;
                cost = factory_cost;
            }
            if(Input.GetKeyDown("4")){
                selection = 4;
                cost = farm_cost;
            }
            if(Input.GetKeyDown("5")){
                selection = 5;
                cost = market_cost;
            }
            if(Input.GetKeyDown("6")){
                selection = 6;
                cost = mine_cost;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Check if there is already an object placed on the tilemap at the current position
                if (!placedPositions.Contains(cellPosition) && cost <= Global.money)
                {
                    if (selection == 1)
                    {
                        lastObjectPlaced = Instantiate(academy, centerPosition, academy.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.academies += 1;
                        Global.money -= cost;
                        Global.cityHappiness += 10;
                    }
                    if (selection == 2)
                    {
                        lastObjectPlaced = Instantiate(armory, centerPosition, armory.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.armories += 1;
                        Global.money -= cost;
                        Global.troops += 2;
                    }
                    else if (selection == 3)
                    {
                        lastObjectPlaced = Instantiate(factory, centerPosition, factory.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.factories += 1;
                        Global.money -= cost;
                        Global.moneyPerTurn += 10;
                    }
                    else if (selection == 4)
                    {
                        lastObjectPlaced = Instantiate(farm, centerPosition, farm.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.farms += 1;
                        Global.money -= cost;
                        Global.foodPerTurn += 10;
                        Global.cityHappiness += 5;
                    }
                    else if (selection == 5)
                    {
                        lastObjectPlaced = Instantiate(market, centerPosition, market.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.markets += 1;
                        Global.money -= cost;
                        Global.foodPerTurn += 10;
                        Global.cityHappiness += 10;
                    }
                    else if (selection == 6)
                    {
                        lastObjectPlaced = Instantiate(mine, centerPosition, mine.transform.rotation);
                        placedPositions.Add(cellPosition);
                        lastCellPosition = cellPosition;
                        Global.mines += 1;
                        Global.money -= cost;
                    }
                    

                    Destroy(currentObject); // Remove the transparent object
                    currentObject = null; // Reset the transparent object reference
                }
            }
            }
        }
        else
        {
            Destroy(currentObject); // Remove the transparent object if it's not needed
            currentObject = null; // Reset the transparent object reference
        }

        if (Input.GetKeyDown(KeyCode.Z)){
            if(lastObjectPlaced){
                Destroy(lastObjectPlaced);
                placedPositions.Remove(lastCellPosition);
                Global.money += cost/2;
            }
        }

    }

    public void OnClickAcademy(){
        selection = 1;
    }
    public void OnClickArmory(){
        selection = 2;
    }
    public void OnClickFactory(){
        selection = 3;
    }
    public void OnClickFarm(){
        selection = 4;
    }
    public void OnClickMarket(){
        selection = 5;
    }
    public void OnClickMine(){
        selection = 6;
    }
}
