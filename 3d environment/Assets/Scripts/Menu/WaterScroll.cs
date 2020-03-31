using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroll : MonoBehaviour
{

    public float xSpeed = 0.01f;
    public float ySpeed = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = Time.time * xSpeed;
        float yOffset = Time.time * ySpeed;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(xOffset, yOffset);

    }
}
