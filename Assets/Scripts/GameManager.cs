using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Grid
{
    public const int WIDTH = 5;
    public const int HEIGHT = 5;
    public GameObject[,] m_tiles = new GameObject[WIDTH, HEIGHT];

}

public class GameManager : MonoBehaviour
{
    private int m_noOfTilesOn = 0;
    private bool m_winState;
    internal int m_moves = 0;
    internal bool m_isPaused = false;
    [SerializeField] private TMP_Text m_movesText;
    [SerializeField] private Slider m_raveometer;
    [SerializeField] private GameObject m_TileObject;
    [SerializeField] private List<GameObject> m_foliageObjects;
    private Grid m_grid = new Grid();
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < Grid.WIDTH; x++)
        {
            for (int y = 0; y < Grid.HEIGHT; y++)
            {
                GameObject tile = Instantiate(m_TileObject);
                m_grid.m_tiles[x, y] = tile;
                m_grid.m_tiles[x, y].transform.position = new Vector3(x * 1.6f, 0, y * 1.6f);
                m_grid.m_tiles[x, y].name = "Tile: " + x + " , " + y;
                m_grid.m_tiles[x, y].GetComponent<Tile>().m_position = new Vector2Int(x,y);
                GameObject chosenFoliage = m_foliageObjects[Random.Range(0, m_foliageObjects.Count)];
                m_grid.m_tiles[x, y].GetComponent<Tile>().m_foliage = Instantiate(chosenFoliage, m_grid.m_tiles[x, y].transform);
                if (x + 1 < Grid.WIDTH)
                {
                    m_grid.m_tiles[x, y].GetComponent<Tile>().m_neighbours.Add(new Vector2Int(1,0));
                }
                if (x - 1 >= 0)
                {
                    m_grid.m_tiles[x, y].GetComponent<Tile>().m_neighbours.Add(new Vector2Int(-1, 0));
                }
                if (y + 1 < Grid.HEIGHT)
                {
                    m_grid.m_tiles[x, y].GetComponent<Tile>().m_neighbours.Add(new Vector2Int(0, 1));
                }
                if (y - 1 >= 0)
                {
                    m_grid.m_tiles[x, y].GetComponent<Tile>().m_neighbours.Add(new Vector2Int(0, -1));
                }
            }
        }
    }

    void Update()
    { 
        m_raveometer.value = Mathf.Lerp(m_raveometer.value, m_noOfTilesOn / 25.0f, Time.deltaTime*2);
        m_movesText.text = "Moves: " + m_moves;
    }

    public void HandleInteraction(Vector2Int pos)
    {

            m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().SwapTileState();
            m_noOfTilesOn += m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().state == 0 ? -1 : 1;
            foreach (var neighbour in m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().m_neighbours)
            {
                m_grid.m_tiles[pos.x + neighbour.x, pos.y + neighbour.y].GetComponent<Tile>().SwapTileState();
                m_noOfTilesOn +=
                    m_grid.m_tiles[pos.x + neighbour.x, pos.y + neighbour.y].GetComponent<Tile>().state == 0 ? -1 : 1;
            }
        
    }


}
