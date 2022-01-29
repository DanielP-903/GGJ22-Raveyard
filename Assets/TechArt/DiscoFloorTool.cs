using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloorTool : MonoBehaviour
{
    
    [SerializeField][Tooltip("Prefab to use as disco tile")]
    GameObject discoTilePrefab;

    [SerializeField] [Tooltip("Number of disco tiles in x and z axes")]
    Vector2 tiling = new Vector2(0, 0);

    [SerializeField]
    float tileWidth = 1f;
    float offset;

    private GameObject[] discoTilesGO;
    private DiscoFloorTile[] discoFloorTilescripts;


    private void OnEnable()
    {
        RefreshTiles();
        InstantiateDiscoTiles();
    }

    private void Update()
    {

    }

        //Destroy existing tiles and reset variables
    void RefreshTiles()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        discoTilesGO = new GameObject[0];
        discoFloorTilescripts = new DiscoFloorTile[0];
    }


    void InstantiateDiscoTiles()
    {
        int totalTiles = Mathf.RoundToInt(tiling.x * tiling.y);
        discoTilesGO = new GameObject[totalTiles];
        discoFloorTilescripts = new DiscoFloorTile[totalTiles];

        float totalWidth = tileWidth * tiling.x;
        offset = -totalWidth * 0.5f;


            //instantiate grid of tiles from top left to bottom right
        int i = 0;
        for (int x = 0; x < tiling.x; x++)
        {
            for (int z = 0; z < tiling.y; z++)
            {
                discoTilesGO[i] = Instantiate(discoTilePrefab);
                discoFloorTilescripts[i] = discoTilesGO[i].GetComponent<DiscoFloorTile>();

                discoTilesGO[i].transform.parent = transform;
                discoFloorTilescripts[i].SetWidth(tileWidth);
                discoTilesGO[i].transform.position = new Vector3(x * tileWidth + offset, 0f, z * tileWidth + offset);

                
                discoFloorTilescripts[i].SetID(i);

                i++;
            }
        }
            

            
        
    }

    void SetTilePosition()
    {
        
    }
}
