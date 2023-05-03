using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPopUp : MonoBehaviour
{
    public GameObject popUpCanvas;
    void Awake() {
        popUpCanvas = GameObject.Find("PopUpCanvas");
    }

    public void HidePopUp() {
        popUpCanvas.SetActive(false);
    }
}
