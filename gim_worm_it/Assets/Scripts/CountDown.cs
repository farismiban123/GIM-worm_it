using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float RemainingTime;
    void Update()
    {
        if(RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;

        }
        else if(RemainingTime < 0)
        {
            RemainingTime = 0;

            countdownText.color = Color.red;
        }
        
        int minutes = Mathf.FloorToInt(RemainingTime/60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);


        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
