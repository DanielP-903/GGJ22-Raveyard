using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    internal int state = 0; 
    internal int previousState = 0; 
    [SerializeField] private Material m_offMaterial;
    [SerializeField] private List<Material> m_discoMaterials;
    internal List<Vector2Int> m_neighbours = new List<Vector2Int>();
    internal Vector2Int m_position;

    internal GameObject m_foliage;

    private Material m_disco;
    // Start is called before the first frame update
    void Awake()
    {
        m_disco = m_discoMaterials[Random.Range(0, m_discoMaterials.Count)];
        state = 0;
        previousState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = state == 0 ? m_offMaterial : m_disco;

        if (transform.GetChild(2))
        {
            transform.GetChild(2).gameObject.SetActive(state == 0);
        }

        if (previousState != state)
        {
            transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            if (previousState == 0)
            {
               m_disco = m_discoMaterials[Random.Range(0, m_discoMaterials.Count)];
            }
        }

        previousState = state;
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
