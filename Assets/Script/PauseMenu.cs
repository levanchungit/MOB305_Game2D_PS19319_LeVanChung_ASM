using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    GameObject Mario;
    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.Find("Mario");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = false;
        Mario.GetComponent<MarioScript>().AmThanh.Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = true;
        Mario.GetComponent<MarioScript>().AmThanh.UnPause();
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
    }
}
