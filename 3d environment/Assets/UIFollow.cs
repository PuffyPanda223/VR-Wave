using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{

    public GameObject m_Player;
    public GameObject m_Reticule;
    Vector3 offset = new Vector3(5, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
        transform.LookAt(m_Player.gameObject.transform);
        transform.RotateAround(transform.position, transform.up, 180f);
        transform.position = m_Reticule.transform.position + offset; 
    }
}
