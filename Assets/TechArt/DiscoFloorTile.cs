using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiscoFloorTile : MonoBehaviour
{
    BoxCollider boxCollider;

    private float _width;
    public float width { get { return _width; } }

    public int id;

    [SerializeField]
    Material[] discoTileMaterialVariants = new Material[4];



    public void SetWidth(float _width)
    {
        gameObject.transform.localScale = Vector3.one * _width;
    }

    public void SetID(int _id)
    {
        id = _id;
        gameObject.name = "disco tile " + id.ToString();
        SetOffset();
    }

    private void SetOffset()
    {
        gameObject.GetComponent<MeshRenderer>().material = discoTileMaterialVariants[Mathf.RoundToInt(id % 4)];
    }
}
