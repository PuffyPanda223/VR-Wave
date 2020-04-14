using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
// this class will save an incoming list of Gameobjects (hitboxes) into a file. When we load a game all that is happening is the game master will get the list and instantiate all the hitboxess
[Serializable]
public class SaveScene : MonoBehaviour
{
    // find all the hitboxes with the hitbox script attached and save them
    public void GetHitBox()
    {

        SaveData.Save();   
    }
}
