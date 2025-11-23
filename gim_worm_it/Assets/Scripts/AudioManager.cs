using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (!musicSource.isPlaying)
        {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
        }

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
        if(volumeSlider != null)
        {
            AudioListener.volume = volumeSlider.value;
            SaveVolume();
        }
    }

    private void SaveVolume()
    {
        if (volumeSlider != null)
            PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }

    private void LoadVolume()
    {
        float vol = PlayerPrefs.GetFloat("soundVolume");
        AudioListener.volume = vol;

        if (volumeSlider != null)
            volumeSlider.value = vol;
    }    
}
