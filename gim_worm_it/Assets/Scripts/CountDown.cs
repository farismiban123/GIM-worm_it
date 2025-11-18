using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            GameData.Instance.fincacingCount = UIManager.Instance.cacingCount;
            GameData.Instance.finsemutCount = UIManager.Instance.semutCount;
            GameData.Instance.finkumbangCount = UIManager.Instance.kumbangCount;
            RemainingTime = 0;
            countdownText.color = Color.red;
            SceneManager.LoadScene("GameEnd");
        }
        
        int minutes = Mathf.FloorToInt(RemainingTime/60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);


        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
