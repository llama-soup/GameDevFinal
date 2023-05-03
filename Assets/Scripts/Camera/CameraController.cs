using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraTransform;


    public float normalSpeed;
    public float fastSpeed;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public float xLeftBound = -100f;
    public float xRightBound = 30f;
    public float zBottomBound = -50f;
    public float zTopBound = 70f;

    public float maxZoom = 45f;
    public float minZoom = 80f;

    public BattleManager battleManagerRef;


    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;


        if(GameObject.Find("BattleManagerObject") != null)
        {
            battleManagerRef = GameObject.Find("BattleManagerObject").GetComponent<BattleManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    void HandleMovement()
    {
        if(battleManagerRef != null){
            //Handle Zoom to Unit
            if (Input.GetKey(KeyCode.F))
            {
                if (battleManagerRef.currentlySelectedTroop != null)
                {
                    Vector3 pointToTravelTo = new Vector3(battleManagerRef.currentlySelectedTroop.transform.position.x,
                        battleManagerRef.currentlySelectedTroop.transform.position.y - 33f, battleManagerRef.currentlySelectedTroop.transform.position.z + 33f);


                    newPosition = pointToTravelTo;
                    newZoom = new Vector3(newZoom.x, 50f, -50f);
                }
            }
        }


        //Camera Position Management

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.z <= zTopBound)
            {
                newPosition += (transform.forward * movementSpeed);
            }

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if(transform.position.z >= zBottomBound)
            {
                newPosition += (transform.forward * -movementSpeed);
            }
            
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x <= xRightBound)
            {
                newPosition += (transform.right * movementSpeed);
            }

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x >= xLeftBound)
            {
                newPosition += (transform.right * -movementSpeed);
            }
            
        }


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        


        //Camera Rotation Management
        /**
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);

        **/
        //Camera Zoom Management

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(newZoom.y > maxZoom)
            {
                newZoom += zoomAmount;
            }

            
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(newZoom.y < minZoom)
            {
                newZoom -= zoomAmount;
            }
            
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);


    }
}
