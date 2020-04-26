using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour

{

    GameObject m_Player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        m_Player = GameObject.Find("Player");
        transform.LookAt(m_Player.gameObject.transform);
    }

}
