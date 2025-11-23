using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject clickArea;

    public Animator transitionAnimator;
    public float transitionTime = 1f;

    public static string lastSceneBeforeShop = "";

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void RestartGame()
    {
        StartCoroutine(LoadLevel("StartMenu"));
    }

    public void ShopMenu()
    {
        //simpan scene before shop
         SceneLoader.lastSceneBeforeShop = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadLevel("ShopMenu"));
    }

    public void Back()
    {
         string target = SceneLoader.lastSceneBeforeShop;

        if (string.IsNullOrEmpty(target))
            target = "StartMenu"; // fallback kalau kosong

        StartCoroutine(LoadLevel(target));
    }


    public void Pause()
    {

        pauseMenuPanel.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseMenuPanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadLevel(currentSceneName));
    }

    public void RetryGame()
    {
        StartCoroutine(LoadLevel("GameScene"));
    }

    
    IEnumerator LoadLevel(string sceneName)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }
        yield return new WaitForSecondsRealtime(transitionTime);

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}