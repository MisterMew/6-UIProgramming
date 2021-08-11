using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) { 
                ResumeGame(); 
            } 
            else { PauseGame(); }
        }
    }

    public void ResumeGame() {
        Debug.Log("GAME RESUMED");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseGame() {
        Debug.Log("GAME PAUSED");
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    public void ReturnMainMenu() {
        Debug.Log("RETURNED TO MAIN MENU");

        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Debug.Log("QUIT GAME");

        pauseMenuUI.SetActive(false);
        Application.Quit();
    }
}
