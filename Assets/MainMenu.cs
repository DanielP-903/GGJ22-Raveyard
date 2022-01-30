using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_level1Notes;
    [SerializeField] private List<GameObject> m_level2Notes;
    [SerializeField] private List<GameObject> m_level3Notes;
    void Update()
    {
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            for (int i = 0; i < GameManager.LVL1_NOTES; i++)
            {
                m_level1Notes[i].SetActive(true);
            }
            for (int i = 0; i < GameManager.LVL2_NOTES; i++)
            {
                m_level2Notes[i].SetActive(true);

            }
            for (int i = 0; i < GameManager.LVL3_NOTES; i++)
            {
                m_level3Notes[i].SetActive(true);

            }
        }
    }

    public void Play(int levelNo)
    {
        GameManager.LEVEL = levelNo;
        switch (levelNo)
        {
            case 1:
            {
                GameManager.PAR_MOVES = 5;
                break;
            } 
            case 2:
            {
                GameManager.PAR_MOVES = 6;
                break;
            }
            case 3:
            {
                GameManager.PAR_MOVES = 7;
                break;
            }
        }
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
