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

    private Camera mainCamera;
    private GameObject currentObject;
    private float rotationAngle = 0f;
    private HashSet<Vector3Int> placedPositions = new HashSet<Vector3Int>(); // Keep track of the positions where objects have been placed


    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.LookAt(Vector3.zero);
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
            Vector3Int hitInt = Vector3Int.FloorToInt(hit.point);
            Vector3Int cellPosition = tilemap.WorldToCell(hit.point);
            Vector3 centerPosition = tilemap.GetCellCenterWorld(cellPosition);

            currentObject.transform.position = centerPosition; // Move the transparent object to the cursor position

            // Rotate the object to be placed based on input
            //rotationAngle += Input.GetAxis("RotateObject") * 90f;
            if (Input.GetKey(KeyCode.O))   
            {
                rotationAngle -= 90f * Time.deltaTime;
                currentObject.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                academy.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                armory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                factory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                farm.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                market.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                mine.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }
            if (Input.GetKey(KeyCode.P)) 
            {
                rotationAngle += 90f * Time.deltaTime;
                currentObject.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                academy.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                armory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                factory.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                farm.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                market.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                mine.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }

            if(Input.GetKeyDown("1")){
                selection = 1;
            }
            if(Input.GetKeyDown("2")){
                selection = 2;
            }
            if(Input.GetKeyDown("3")){
                selection = 3;
            }
            if(Input.GetKeyDown("4")){
                selection = 4;
            }
            if(Input.GetKeyDown("5")){
                selection = 5;
            }
            if(Input.GetKeyDown("6")){
                selection = 6;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Check if there is already an object placed on the tilemap at the current position
                if (!placedPositions.Contains(cellPosition))
                {
                    if (selection == 1)
                    {
                        Instantiate(academy, centerPosition, academy.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.academies += 1;
                    }
                    if (selection == 2)
                    {
                        Instantiate(armory, centerPosition, armory.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.armories += 1;
                    }
                    else if (selection == 3)
                    {
                        Instantiate(factory, centerPosition, factory.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.factories += 1;
                    }
                    else if (selection == 4)
                    {
                        Instantiate(farm, centerPosition, farm.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.farms += 1;
                    }
                    else if (selection == 5)
                    {
                        Instantiate(market, centerPosition, market.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.markets += 1;
                    }
                    else if (selection == 6)
                    {
                        Instantiate(mine, centerPosition, mine.transform.rotation);
                        placedPositions.Add(cellPosition);
                        Global.mines += 1;
                    }
                    

                    Destroy(currentObject); // Remove the transparent object
                    currentObject = null; // Reset the transparent object reference
                }
            }
        }
        else
        {
            Destroy(currentObject); // Remove the transparent object if it's not needed
            currentObject = null; // Reset the transparent object reference
        }
    }
}