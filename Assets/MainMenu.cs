using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private List<GameObject> m_level1Notes;
    [SerializeField] private List<GameObject> m_level2Notes;
    [SerializeField] private List<GameObject> m_level3Notes;
    [SerializeField] private Slider m_volumeSlider;
    [SerializeField] private AudioSource m_music;

    void Start()
    {
        m_volumeSlider.value = GameManager.VOLUME_MULTI;
    }


    void Update()
    {
        GameManager.VOLUME_MULTI = m_volumeSlider.value;
        m_music.volume = GameManager.VOLUME_MULTI;
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
