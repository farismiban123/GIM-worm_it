using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingMenuPanel;
    public GameObject Tutorial;
public void OnStartClick()
{
    if (GameData.Instance != null && GameData.Instance.tutorialCompleted == false)
    {
        GameData.Instance.tutorialCompleted = true;
        Tutorial.SetActive(true);   
        SceneManager.LoadScene("GameScene");
    }
    else
    {
        SceneManager.LoadScene("GameScene");
    }
}

    public void OnSettingsClick()
    {
        mainMenuPanel.SetActive(false);
        settingMenuPanel.SetActive(true);
    }

    public void OnSettingsBackClick()
    {
        mainMenuPanel.SetActive(true);
        settingMenuPanel.SetActive(false);
    }

}
