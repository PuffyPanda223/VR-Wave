using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{

    public Pointer m_Pointer;
    public SpriteRenderer m_CircleRenderer;

    public Sprite m_OpenSprite;
    public Sprite m_ClosedSprite;

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
    }

    private void OnDestroy()
    {
        m_Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        transform.position = point;

        if(hitObject)
        {
            m_CircleRenderer.sprite = m_ClosedSprite;
        }
        else
        {
            m_CircleRenderer.sprite = m_OpenSprite;
        }
    }
}
