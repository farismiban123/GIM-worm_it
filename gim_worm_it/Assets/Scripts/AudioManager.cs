using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header ("--- Audio Slider ---")]
    public Slider volumeSlider;
    
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

        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("sooundVolume", 10);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //buat ngatur volume dari settings

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
        AudioListener.volume = volumeSlider.value;
    }
}
