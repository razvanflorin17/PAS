using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Video used: https://youtu.be/JivuXdrIHK0
 * eu eram la https://youtu.be/JivuXdrIHK0?t=593
 */
public class PauseMenu : MonoBehaviour
{
    // boolean variable that stores the state of the game (if paused or not)
    public static bool GameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject playerHUD;
    [SerializeField]
    private GameObject player;
    [SerializeField] 
    private string mainMenuScript;
    [SerializeField]
    private AudioSource buttonsAudioSource;

    void Update()
    {
        // every time the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeSelf) 
        {
            if (GameIsPaused)
            {
                // Debug.Log("Game was resumed");
                ResumeGame();
            } 
            else
            {
                // Debug.Log("Game was paused");
                PauseGame();
            }
        }

        // Whenever the Player dies
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health <= 0 && !gameOverUI.activeSelf)
        {
            // print("Game Over");
            GameOver();
        }
    }

    // method also called when "RESUME" button is clicked
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        playerHUD.SetActive(true);

        // reenable the player view when the game resumes
        player.GetComponent<PlayerLook>().enabled = true;

        // hide the cursor when the game resumes
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        playerHUD.SetActive(false);

        // disable the player view (camera souldn't move when paused)
        player.GetComponent<PlayerLook>().enabled = false;

        // show the cursor when the game is paused
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        playerHUD.SetActive(false);

        // disable the player view (camera souldn't move when paused)
        player.GetComponent<PlayerLook>().enabled = false;

        // show the cursor when the game is paused
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // method called when "SETTINGS" button is pressed
    public void OpenSettings()
    {

    }

    public void RestartScene()
    {
        // reenable the player view when the game resumes
        player.GetComponent<PlayerLook>().enabled = true;

        // hide the cursor when the game resumes
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1.0f;
        GameIsPaused = false;

        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void playButtonSound()
    {
        buttonsAudioSource.volume = 0.5f;
        buttonsAudioSource.Play();
    }

    public void Exit()
    {
        GameIsPaused= false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(mainMenuScript);
    }
}
