using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{

    GameObject m_Player;
    GameObject m_Reticule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Player = GameObject.Find("Player");
        m_Reticule = GameObject.Find("PR_Reticule");
        transform.LookAt(m_Player.gameObject.transform);
        transform.RotateAround(transform.position, transform.up, 180f);
        transform.position = m_Reticule.transform.position + new Vector3(5, 0, 0);
    }
}
