using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loser : MonoBehaviour
{
    public void OnLoseBattle(){
        Global.troops = 1;
    }
}
