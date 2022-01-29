using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private Ray m_ray;
    private RaycastHit m_hit;
    private float m_inputTimer = 0.3f;

    private GameManager m_gameManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManagerRef = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_inputTimer = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        // Decrement input delay timer
        m_inputTimer = m_inputTimer > 0 ? m_inputTimer - Time.deltaTime : 0;
        if (!m_gameManagerRef.m_isPaused)
        {
            // Check if click occurs and is past input delay
            if (Mouse.current.leftButton.wasPressedThisFrame && m_inputTimer <= 0.0f)
            {
                m_ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(),
                    Mouse.current.position.y.ReadValue(), 0));
                if (Physics.Raycast(m_ray, out m_hit))
                {
                    // I hit something! now do something...
                    print(m_hit.collider.name);
                    m_gameManagerRef.HandleInteraction(m_hit.transform.gameObject.GetComponent<Tile>().m_position);
                    m_gameManagerRef.m_moves++;
                    //m_hit.transform.gameObject.GetComponent<Tile>().SwapTileState();
                }

            }
        }
    }
}
