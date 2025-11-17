using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingMenuPanel;
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
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

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
