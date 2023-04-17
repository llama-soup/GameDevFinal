using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningText : MonoBehaviour
{
    public TextMeshProUGUI letterText;
    public TextMeshProUGUI dialogueButtonText;
    public int dialogueIndex = 0;

    public string playerName = "PLAYER";
    public string paragraph3 = "We cannot allow these traitors to continue unchecked. In exchange for your military assistance in suppressing these riots, you will receive great rewards.";
    public string paragraph4 = "As we embark upon this journey together, let us remember that our nations are stronger united than divided. May the blessings of the divine guide us and protect us in our quest for greatness. Long live the Kingdom of Drodemore and the Kingdom of Zoba!";

    public static OpeningText inst;

    void Awake() {
        inst = this;
        letterText.text = string.Format("Greetings, {0}. It is I, King Edmund Hamlin XVII, writing to thee from the great halls of my castle. I hope that thou art in good health and spirits. I have heard news of your arrival to the district of Tunstead. The Kingdom of Drodemore expresses their gratitude for our closest ally, the Kingdom of Zoba, for its assistance in our time of need.", playerName);
    }

    public void DisplayText() {
        if(dialogueIndex == 1) {
            letterText.text = string.Format("{0}, as the finest soldier of Zoba, I must ask for thy assistance in these troubled times. There are those among us who have dared to rebel against my rightful rule and sow discord and chaos throughout our lands. Their actions threaten the fabric of the Drodemore kingdom, and the strength of our alliance with it.", playerName);
        }
        if(dialogueIndex == 2) {
            letterText.text = paragraph3;
        }
        if(dialogueIndex == 3) {
            letterText.text = paragraph4;
        }
    }
        

    public void OnContinueClick() {
        dialogueIndex++;
    }
}
