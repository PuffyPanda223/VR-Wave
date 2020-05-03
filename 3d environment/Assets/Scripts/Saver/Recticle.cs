using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Reticle class specifically for the level creator scene. Because we had to use different scripts to the normal pointer we had to modify the public variable m_Pointer to a class of VRDrawHitBox
public class Recticle : MonoBehaviour
{

    public VRDrawHitBox m_Pointer;
    public SpriteRenderer m_CircleRenderer;

    public Sprite m_OpenSprite;
    public Sprite m_ClosedSprite;
    public Sprite m_DrawSprite;

    public GameObject m_Player;

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Awake()
    {
        m_Pointer.OnPointerUpdate += UpdateSprite;
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(m_Player.gameObject.transform);

        if (Interactable.drawActive == true)
        {
            m_CircleRenderer.sprite = m_DrawSprite;
        }

        else
        {

        }
    }

    private void OnDestroy()
    {
        m_Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        transform.position = point;


        if (Interactable.drawActive == false)
        {
            if (hitObject)
            {
                m_CircleRenderer.sprite = m_ClosedSprite;
            }
            else
            {
                m_CircleRenderer.sprite = m_OpenSprite;
            }
        }
        else
        {
            m_CircleRenderer.sprite = m_DrawSprite;
        }
    }
}
