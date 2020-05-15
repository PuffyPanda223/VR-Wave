using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// simple class that stores the name and final score so it can be saved
/// </summary>
/// 
[Serializable]
public class Highscore
{
    [SerializeField]
    public string name;
    [SerializeField]
    public float score;   
}
