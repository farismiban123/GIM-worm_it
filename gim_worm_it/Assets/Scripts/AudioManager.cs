using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [Header ("--- Audio Source ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header ("--- Audio Clip ---")]
    public AudioClip background;
    public AudioClip tapTanah;
    public AudioClip tapCacing;
    public AudioClip tapMenu;
    public AudioClip woodenStake;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play(); 
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
