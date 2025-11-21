using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float RemainingTime;

    void Start()
    {
        // Ambil waktu per level dari GameData (30 detik)
        if (GameData.Instance != null)
        {
            RemainingTime = GameData.Instance.levelTime;

            GameData.Instance.coinsBeforeLevel = GameData.Instance.coins;
        }
    }

    void Update()
    {
        if (RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;
        }
        else
        {
            RemainingTime = 0;
            if (countdownText != null) countdownText.color = Color.red;

            HandleLevelEnd();
        }

        if (countdownText != null)
        {
            int minutes = Mathf.FloorToInt(RemainingTime / 60);
            int seconds = Mathf.FloorToInt(RemainingTime % 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void HandleLevelEnd()
    {
        if (UIManager.Instance == null || LevelManager.Instance == null || GameData.Instance == null)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        bool reached = LevelManager.Instance.HasReachedQuota();

        if (reached)
        {
            // kalau quota terpenuhi → pergi ke GameEnd
            SceneManager.LoadScene("GameEnd");
        }
        else
        {
            // gagal → restore coin dan GameOver
            GameData.Instance.coins = GameData.Instance.coinsBeforeLevel;
            SceneManager.LoadScene("GameOver");
        }
    }
}
