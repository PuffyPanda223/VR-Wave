using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // this object is assigned in the editor, it is the video sphere with the video component attached to it
    public GameObject sphere;
    // if true game goes normally, if false the game is paused
    public static bool gameState = false;


    /// <summary>
    /// this controls the modification of the gamestate bool
    /// </summary>
    public static void changeGameState()
    {
        gameState = !gameState;
    }

}
