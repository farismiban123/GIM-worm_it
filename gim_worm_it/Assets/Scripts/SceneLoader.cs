using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject clickArea;
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void ShopMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ShopMenu");
    }

    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameEnd");
    }

    //pause menu

    public void Pause()
    {
        pauseMenu.SetActive(true);

        if (clickArea != null) clickArea.SetActive(false);

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        if (clickArea != null) clickArea.SetActive(true);

        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
