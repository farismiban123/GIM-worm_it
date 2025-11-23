using UnityEngine;

public class WoodenStake : MonoBehaviour
{
    private AudioSource myAudioSource;
    private Animator myAnimator;

    private float minSpeedThreshold = 0.1f; 

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Pause();
            }
            return; 
        }

        float currentSpeed = myAnimator.GetFloat("Speed");

        if (currentSpeed > minSpeedThreshold)
        {
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.UnPause(); 
                if (!myAudioSource.isPlaying) myAudioSource.Play(); 
            }

            myAudioSource.pitch = 0.8f + (currentSpeed * 0.1f);
        }
        else
        {
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Stop();
            }
        }
    }
    
}