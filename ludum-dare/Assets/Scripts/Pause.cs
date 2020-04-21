using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject PauseMenuUI;
    public GameObject LifeBar;
    public GameObject AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        
        PauseMenuUI.SetActive(false);
        LifeBar.SetActive(true);
        GameIsPaused = false;
        Play("Ambiente");
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        LifeBar.SetActive(false);
        GameIsPaused = true;
        AudioManager.GetComponent<AudioManager>().Pause("Ambiente");
        Time.timeScale = 0f;

    }

    public void Play(string sound)
    {
        AudioManager.GetComponent<AudioManager>().Play(sound);
    }
}
