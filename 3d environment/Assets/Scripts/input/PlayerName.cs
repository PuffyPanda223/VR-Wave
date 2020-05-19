using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// stores the player name from the input scene
public class PlayerName : MonoBehaviour
{
    // variable can be used anywhere in the game, the function below adds to this static variable
    public static string playerName = "";


    // unity actions are events that other scripts can subscribe to and get data from, 
    public UnityAction<string> playerNameUpdate = null;

    public void addChar(string character)
    {
        // check which character
        switch (character)
        {
            case "SPACE":
                playerName += " ";
                break;
            case "BACK":
                delChar();
                break;
            case "OK":
                // only go to the next scene if they actually have put in a name
                Debug.Log("got here aye");
                if (playerName.Length > 0)
                {
                    SceneManager.LoadScene(0);
                }
                break;
            // if it isnt any of the special keys than it is a normal key `
            default:
                playerName += character;
                break;
        }
    
        // checks to see if another script has subscribed to the unity action, if not there is no reason to call it
       if (playerNameUpdate != null)
        {
            // this method is being used by the displayName script. Whenever the name is updated it will send the updated name which the display will than update the text mesh pro to.
            playerNameUpdate(playerName);
        }
       
    }

    public static void delChar()
    {
        // player name becomes all the characters except for the last one but only if there is a character
        if (playerName.Length > 0)
        {
            playerName = playerName.Substring(0, playerName.Length - 1);
        }
        
    }
}
