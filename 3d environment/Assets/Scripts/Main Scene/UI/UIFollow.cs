using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// used on the score and timer UI in the vr main scene
public class UIFollow : MonoBehaviour
{

    public GameObject m_Player;
    public GameObject m_Reticule;
    // Update is called once per frame
    Vector3 offset3 = new Vector3(-5, 0, 5);
    Vector3 offset2 = new Vector3(5, 0, 5);
    Vector3 offset1 = new Vector3(5, 0, 5);
    void Update()
    {
        transform.LookAt(m_Player.gameObject.transform);
        
        // depending on where in the game world the users reticle is adjust the UI so it is slightly infront of it. Otherwise the text will appear behind the game world
        if (m_Reticule.transform.position.z > 0)
        {
            if (m_Reticule.transform.position.x < 0 )
            {
                //transform.position = m_Reticule.transform.position - offset2;
                transform.position = m_Reticule.transform.position - offset3;
            } else
            {
                transform.position = m_Reticule.transform.position - offset2;
            }
            
        } else
        {
            if (m_Reticule.transform.position.x < 0)
            {
                transform.position = m_Reticule.transform.position + offset1;
            } else
            {
                transform.position = m_Reticule.transform.position + offset3;
            }
        }

        transform.RotateAround(transform.position, transform.up, 180f);
    }
}
