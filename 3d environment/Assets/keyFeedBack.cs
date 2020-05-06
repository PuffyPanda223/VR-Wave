using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

// script is attached to each key in the input scene
public class keyFeedBack : MonoBehaviour
{

    private soundHandler soundHandler; 
    public bool keyHit = false;
    public bool keyCanHitAgain = false;

    float originalZPosition;

    //materials for the different state of the key
    public Material standard;
    public Material pressed;

    private PlayerName playerName; 

    private void Start()
    {
        soundHandler = GameObject.FindGameObjectWithTag("soundHandler").GetComponent<soundHandler>(); 
        originalZPosition = transform.position.z;
        GameObject gamemaster = GameObject.Find("GameMaster");
        playerName = gamemaster.GetComponent<PlayerName>(); 
    }

    private void Update()
    {
        if(keyHit && keyCanHitAgain)
        {
            soundHandler.PlayClick(); 
            keyCanHitAgain = false;
            keyHit = false;
            transform.position += new Vector3(0, 0, 0.6f);
            transform.GetComponent<MeshRenderer>().material = pressed;
            Canvas Canvas = transform.GetComponentInChildren<Canvas>();
            TextMeshProUGUI txt = Canvas.GetComponentInChildren<TextMeshProUGUI>();
            playerName.addChar(txt.text);
            
        }

        if(transform.position.z > originalZPosition)
        {
            transform.position += new Vector3(0, 0, -0.01f);
        } else
        {
            keyCanHitAgain = true;
            transform.GetComponent<MeshRenderer>().material = standard;
        }
    }
}
