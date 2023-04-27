using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// credit: https://www.youtube.com/watch?v=guelZvubWFY&ab_channel=GameDevTraum

public class SetPlayerName : MonoBehaviour
{
    public void SetName(string name) {
        Global.playerName = name;
    }
}
