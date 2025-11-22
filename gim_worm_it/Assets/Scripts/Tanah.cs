using UnityEngine;

public class TanahScript : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (audioManager != null && audioManager.tapTanah != null)
        {
            audioManager.PlaySFX(audioManager.tapTanah);
        }
    }

    public void TanahAnimSelesai()
    {
        Destroy(gameObject);
    }

    bool canClick = false;
    public void click()
    {
        if (canClick)
        {
            audioManager.PlaySFX(audioManager.tapTanah);
        }
    }
}
