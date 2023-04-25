using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CityUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI wealthText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI happinessText;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI troopsText;

    public static CityUI inst;

    void Awake() {
        inst = this;
    }

    // Update is called once per frame
    void Update() {
        Global.cityWealth = Global.farms * 2000 + Global.markets * 6000;
        //Global.cityHappiness = Global.academies * 0.4f + Global.armories * -0.1f + Global.factories * -0.1f + Global.farms * 0.3f + Global.markets * 0.3f + Global.mines * -0.2f;
        //Global.money = Global.armories * 10 + Global.factories * 30 + Global.farms * 10 + Global.markets * 10 + Global.mines * 30;
        // TO DO: decrease food proportionate to population somehow
        // number of troops lost after being used in battle will be updated somewhere else
        //Global.troops = Global.armories * 8;
        
        // or, do we subtract from wealth if we want the buildings to have maintenance costs
        // statsText.text = string.Format("Day: {0}   City Wealth: {1}   Food: {2}   Happiness: {3}", new object[4] {Global.cityWealth, Global.food, Global.factories});
        wealthText.text = string.Format("City Wealth: {0}", Global.cityWealth);
        foodText.text = string.Format("City Food: {0}", Global.cityFood);
        happinessText.text = string.Format("City Happiness: {0}", Global.cityHappiness);
        moneyText.text = string.Format("Player Money: {0}", Global.money);
        troopsText.text = string.Format("Player Troops: {0}", Global.troops);

        if(Global.cityHappiness < 0){
            Global.cityHappiness = 0;
        }
        if(Global.cityFood < 0){
            Global.cityFood = 0;
        }
    }

    public void OnEndTurn(){
        Global.money += Global.moneyPerTurn;

        //Citizens grow more unhappy if the turn is ended with 0 food.
        if(Global.cityFood <= 0){
            Global.cityHappiness -= 5;
        }

        //Each troop needs 1 food per turn maybe
        Global.cityFood += Global.foodPerTurn;
        Global.cityFood -= Global.troops;
        
        //Citizens grow more and more unhappy each turn
        Global.cityHappiness -= 5;

        Global.battleCounter += 1;

        if (Global.cityHappiness <= 0){
            Debug.Log("City happiness reached 0, BATTLE STARTED");
            Global.battleCounter = 0;
            SceneManager.LoadScene("BattleView");
        }


        if (Global.battleCounter >= 6){
            Debug.Log("Too many turns without Battle, BATTLE STARTED");
            Global.battleCounter = 0;
            SceneManager.LoadScene("BattleView");
        }
    }
}
