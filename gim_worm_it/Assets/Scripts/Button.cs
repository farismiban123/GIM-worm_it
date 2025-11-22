using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    private AudioManager audioManager;
    private Button button;

    public AudioClip clip; 

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        if (audioManager != null && clip != null)
        {
            audioManager.PlaySFX(clip);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}
