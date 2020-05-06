using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class displayName : MonoBehaviour
{
    // we need to access the unity action in the playerName script so we can subscribe to it. Whenever the action is initiated it sends through the name and we can update it here. 
    // this is better than simply checking the static variable player name every frame
    public PlayerName player; 

    private void Awake()
    {
        if (PlayerName.playerName != "")
        {
            // the user can change names by going back to this screen so we want to display the last name in the playername variable on scene launch
            transform.GetComponent<TextMeshProUGUI>().text = PlayerName.playerName;
        } 
        player.playerNameUpdate += updateNameOnDisplay;
    }

    private void updateNameOnDisplay(string name)
    {
        transform.GetComponent<TextMeshProUGUI>().text = name;
    }
}
