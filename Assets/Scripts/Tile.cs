using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    internal int state = 1; 
    internal int previousState = 0; 
    [SerializeField] private Material m_offMaterial;
    [SerializeField] private List<Material> m_discoMaterials;
    internal List<Vector2Int> m_neighbours = new List<Vector2Int>();
    internal Vector2Int m_position;

    internal GameObject m_foliage;
    [SerializeField] private GameObject m_skele;

    private Material m_disco;
    // Start is called before the first frame update
    void Awake()
    {
        m_disco = m_discoMaterials[Random.Range(0, m_discoMaterials.Count)];
        m_skele.GetComponent<Animator>().SetInteger("DanceMove", Random.Range(0, 5));
        m_skele.transform.position = m_skele.GetComponent<Animator>().GetInteger("DanceMove") == 0 ? new Vector3(transform.position.x, .8f, transform.position.z) : new Vector3(transform.position.x, 1.15f, transform.position.z);
        state = 1;
        previousState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = state == 0 ? m_offMaterial : m_disco;

        if (transform.GetChild(3))
        {
            transform.GetChild(3).gameObject.SetActive(state == 0);
        }

        if (m_skele)
        {
            m_skele.SetActive(state == 1);
        }

        if (previousState != state)
        {
            transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            if (previousState == 0)
            {
               m_disco = m_discoMaterials[Random.Range(0, m_discoMaterials.Count)];
               m_skele.GetComponent<Animator>().SetInteger("DanceMove", Random.Range(0,5));
               m_skele.transform.position = m_skele.GetComponent<Animator>().GetInteger("DanceMove") == 0 ? new Vector3(transform.position.x, .8f, transform.position.z) : new Vector3(transform.position.x, 1.15f, transform.position.z);
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
