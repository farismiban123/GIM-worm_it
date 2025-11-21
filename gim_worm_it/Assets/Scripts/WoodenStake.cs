using UnityEngine;

public class WoodenStake : MonoBehaviour
{
    private AudioManager audioManager;
    private float speedMultiplier = 1f;
    public float speedIncrement = 0.3f;
    public float maxSpeed = 3f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (audioManager != null && audioManager.woodenStake != null)
        {
            audioManager.SFXSource.clip = audioManager.woodenStake;
            audioManager.SFXSource.loop = true;
            audioManager.SFXSource.pitch = speedMultiplier;
            audioManager.SFXSource.Play();
        }
    }

    public void OnClick()
    {
        if (audioManager != null && audioManager.SFXSource.isPlaying)
        {
            speedMultiplier += speedIncrement;
            if (speedMultiplier > maxSpeed)
                speedMultiplier = maxSpeed;

            audioManager.SFXSource.pitch = speedMultiplier;
        }
    }
}
