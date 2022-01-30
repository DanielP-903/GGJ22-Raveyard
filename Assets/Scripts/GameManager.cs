using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Grid
{
    public const int WIDTH = 5;
    public const int HEIGHT = 5;
    public GameObject[,] m_tiles = new GameObject[WIDTH, HEIGHT];

}

public class GameManager : MonoBehaviour
{
    public static int LEVEL = 1;
    public static int PAR_MOVES = 5;
    public static int LVL1_NOTES = 0;
    public static int LVL2_NOTES = 0;
    public static int LVL3_NOTES = 0;
    public static int LVL1_NOTES_HIGHEST = 0;
    public static int LVL2_NOTES_HIGHEST = 0;
    public static int LVL3_NOTES_HIGHEST = 0;
    public static bool FIRST_PLAY = true;
    public static float VOLUME_MULTI = 1.0f;
    private int m_noOfTilesOn = 0;
    private bool m_winState;
    internal int m_moves = 0;
    internal bool m_isPaused = false;

    [SerializeField] private TMP_Text m_movesText;
    [SerializeField] private TMP_Text m_levelText;
    [SerializeField] private TMP_Text m_parText;
    [SerializeField] private List<ParticleSystem> m_party;
    [SerializeField] private List<GameObject> m_lasers;
    [SerializeField] private GameObject m_note1;
    [SerializeField] private GameObject m_note2;
    [SerializeField] private GameObject m_note3;
    [SerializeField] private ParticleSystem m_chadEffect;
    [SerializeField] private Slider m_raveometer;
    [SerializeField] private GameObject m_winPanel;
    [SerializeField] private GameObject m_howToPlayPanel;
    [SerializeField] private GameObject m_TileObject;
    [SerializeField] private TMP_Text m_totalMoves;
    [SerializeField] private List<GameObject> m_foliageObjects;
    private Grid m_grid = new Grid();

    [SerializeField] private AudioSource m_music;
    [SerializeField] private AudioSource m_sfx;

    private readonly int[,] m_layoutLevel1 =
    {
        {0, 0, 1, 1, 1},
        {1, 1, 1, 1, 1},
        {0, 0, 1, 1, 0},
        {0, 1, 1, 0, 0},
        {1, 1, 1, 1, 0}
    };

    private readonly int[,] m_layoutLevel2 =
    {
        {1, 0, 1, 1, 1},
        {0, 1, 0, 1, 0},
        {0, 1, 0, 1, 1},
        {1, 1, 0, 0, 0},
        {1, 1, 0, 1, 1}
    };

    private readonly int[,] m_layoutLevel3 =
    {
        {1, 0, 0, 0, 1},
        {0, 1, 1, 1, 0},
        {0, 1, 0, 1, 0},
        {1, 0, 1, 0, 1},
        {1, 1, 0, 1, 1}
    };

    // Start is called before the first frame update
    void Start()
    {
        m_note1.SetActive(false);
        m_note2.SetActive(false);
        m_note3.SetActive(false);
        m_music.volume = VOLUME_MULTI;
        m_sfx.volume = 0.75f * VOLUME_MULTI;
        m_isPaused = false;
        m_winState = false;

        if (FIRST_PLAY)
        {
            m_isPaused = true;
            m_howToPlayPanel.SetActive(true);
        }

        for (int x = 0; x < Grid.WIDTH; x++)
        {
            for (int y = 0; y < Grid.HEIGHT; y++)
            {
                GameObject tile = Instantiate(m_TileObject);
                m_grid.m_tiles[x, y] = tile;
                m_grid.m_tiles[x, y].transform.position = new Vector3(x * 1.6f, 0, y * 1.6f);
                m_grid.m_tiles[x, y].name = "Tile: " + x + " , " + y;
                m_grid.m_tiles[x, y].GetComponent<Tile>().m_position = new Vector2Int(x, y);
                GameObject chosenFoliage = m_foliageObjects[Random.Range(0, m_foliageObjects.Count)];
                m_grid.m_tiles[x, y].GetComponent<Tile>().m_foliage =
                    Instantiate(chosenFoliage, m_grid.m_tiles[x, y].transform);
                switch (LEVEL)
                {
                    case 1:
                    {
                        m_grid.m_tiles[x, y].GetComponent<Tile>().state = m_layoutLevel1[x, y];
                        m_grid.m_tiles[x, y].GetComponent<Tile>().previousState = m_layoutLevel1[x, y];
                        if (m_layoutLevel1[x, y] == 1)
                        {
                            m_noOfTilesOn++;
                        }

                        break;
                    }
                    case 2:
                    {
                        m_grid.m_tiles[x, y].GetComponent<Tile>().state = m_layoutLevel2[x, y];
                        m_grid.m_tiles[x, y].GetComponent<Tile>().previousState = m_layoutLevel2[x, y];
                        if (m_layoutLevel2[x, y] == 1)
                        {
                            m_noOfTilesOn++;
                        }

                        break;
                    }
                    case 3:
                    {
                        m_grid.m_tiles[x, y].GetComponent<Tile>().state = m_layoutLevel3[x, y];
                        m_grid.m_tiles[x, y].GetComponent<Tile>().previousState = m_layoutLevel3[x, y];
                        if (m_layoutLevel3[x, y] == 1)
                        {
                            m_noOfTilesOn++;
                        }

                        break;
                    }
                }

                if (x + 1 < Grid.WIDTH)
                {
                    m_grid.m_tiles[x, y].GetComponent<Tile>().m_neighbours.Add(new Vector2Int(1, 0));
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

        foreach (var t in m_party)
        {
            t.Play();
        }

        foreach (var t in m_lasers)
        {
            t.SetActive(false);
        }
    }

    void Update()
    {
        m_raveometer.value = Mathf.Lerp(m_raveometer.value, m_noOfTilesOn / 25.0f, Time.deltaTime * 3);
        m_movesText.text = "Moves: " + m_moves;
        m_levelText.text = "LEVEL " + LEVEL;
        m_parText.text = "Par: " + PAR_MOVES;

        if (m_raveometer.value > 0.99f)
        {
            m_winState = true;
        }

        for (int i = 0; i < m_party.Count; i++)
        {
            var emissionModule = m_party[i].emission;
            ParticleSystem.MinMaxCurve tempCurve = emissionModule.rateOverTime;
            tempCurve.constant = m_raveometer.value * 3;
            emissionModule.rateOverTime = tempCurve;
        }

        if (m_winState)
        {
            m_totalMoves.text = "Total Moves: " + m_moves;
            foreach (var t in m_lasers)
            {
                t.SetActive(true);
            }


            m_winPanel.SetActive(true);
            if (m_moves <= PAR_MOVES)
            {
                // 3 notes
                m_note1.SetActive(true);
                m_note2.SetActive(true);
                m_note3.SetActive(true);
                switch (LEVEL)
                {
                    case 1:
                    {
                        LVL1_NOTES = 3;
                        break;
                    }
                    case 2:
                    {
                        LVL2_NOTES = 3;
                        break;
                    }
                    case 3:
                    {
                        LVL3_NOTES = 3;
                        break;
                    }
                }
            }
            else if (m_moves > PAR_MOVES && m_moves <= PAR_MOVES + 3)
            {
                m_note1.SetActive(true);
                m_note2.SetActive(true);
                if (LVL2_NOTES <= 2)
                {
                    switch (LEVEL)
                    {
                        case 1:
                        {
                            LVL1_NOTES = 2;
                            break;
                        }
                        case 2:
                        {
                            LVL2_NOTES = 2;
                            break;
                        }
                        case 3:
                        {
                            LVL3_NOTES = 2;
                            break;
                        }
                    }
                }
            }
            else
            {
                m_note1.SetActive(true);
                if (LVL2_NOTES <= 1)
                {
                    switch (LEVEL)
                    {
                        case 1:
                        {
                            LVL1_NOTES = 1;
                            break;
                        }
                        case 2:
                        {
                            LVL2_NOTES = 1;
                            break;
                        }
                        case 3:
                        {
                            LVL3_NOTES = 1;
                            break;
                        }
                    }
                }
            }

            if (LVL1_NOTES > LVL1_NOTES_HIGHEST)
            {
                LVL1_NOTES_HIGHEST = LVL1_NOTES;
            }

            if (LVL2_NOTES > LVL2_NOTES_HIGHEST)
            {
                LVL2_NOTES_HIGHEST = LVL2_NOTES;
            }

            if (LVL3_NOTES > LVL3_NOTES_HIGHEST)
            {
                LVL3_NOTES_HIGHEST = LVL3_NOTES;
            }

            m_isPaused = true;
        }
    }

    public void HandleInteraction(Vector2Int pos)
    {
        m_chadEffect.Play();
        m_sfx.Play();
        m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().SwapTileState();
        m_noOfTilesOn += m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().state == 0 ? -1 : 1;
        foreach (var neighbour in m_grid.m_tiles[pos.x, pos.y].GetComponent<Tile>().m_neighbours)
        {
            m_grid.m_tiles[pos.x + neighbour.x, pos.y + neighbour.y].GetComponent<Tile>().SwapTileState();
            m_noOfTilesOn +=
                m_grid.m_tiles[pos.x + neighbour.x, pos.y + neighbour.y].GetComponent<Tile>().state == 0 ? -1 : 1;
        }

    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        m_isPaused = !m_isPaused;
    }

    public void SetFirstPlayed()
    {
        FIRST_PLAY = false;
    }

}
