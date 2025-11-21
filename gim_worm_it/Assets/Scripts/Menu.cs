using UnityEngine;
using UnityEngine.UI;

public class MenuSFX : MonoBehaviour
{
    private AudioManager audioManager;
    private Button[] buttons;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        buttons = GetComponentsInChildren<Button>();

        foreach (Button btn in buttons)
            btn.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        if (audioManager != null && audioManager.tapMenu != null)
            audioManager.PlaySFX(audioManager.tapMenu);
    }

    private void OnDestroy()
    {
        foreach (Button btn in buttons)
            btn.onClick.RemoveAllListeners();
    }
}
