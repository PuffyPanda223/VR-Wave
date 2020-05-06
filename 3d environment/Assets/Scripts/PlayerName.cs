using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// stores the player name from the input scene
public class PlayerName : MonoBehaviour
{
    public static string playerName = "";
    public UnityAction<string> playerNameUpdate = null;

    public void addChar(string character)
    {
        switch (character)
        {
            case "SPACE":
                playerName += " ";
                break;
            case "BACK":
                delChar();
                break;
            case "SAVE":
                if (playerName.Length > 0)
                {
                    SceneManager.LoadScene(0);
                }
                break;
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
        // player name becomes all the characters except for the last one
        if (playerName.Length > 0)
        {
            playerName = playerName.Substring(0, playerName.Length - 1);
            

        }
        
    }
}
