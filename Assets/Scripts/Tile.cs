using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int state = 0; 
    [SerializeField] private Material m_onMaterial;
    [SerializeField] private Material m_offMaterial;
    public List<Vector2Int> m_neighbours = new List<Vector2Int>();
    public Vector2Int m_position;

    public GameObject m_foliage;
    // Start is called before the first frame update
    void Awake()
    {
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = state == 0 ? m_offMaterial : m_onMaterial;

        if (transform.GetChild(1))
        {
            transform.GetChild(1).gameObject.SetActive(state == 0);
        }
    }

    public void SwapTileState()
    {
        switch (state)
        {
            case 0:
                state = 1;
                break;
            case 1:
                state = 0;
                break;
        }
    }

}
