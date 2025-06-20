﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape)) 
     {
            if (GameIsPaused)
            {
                
                Resume();
            } else
            {
                
                Pause();
            }
         } 
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
        Resume();
    }
    void Pause () {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu(){
        
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame(){
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}
