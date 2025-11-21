using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText; 
    public int cacingCount = 0;
    public int semutCount = 0;
    public int kumbangCount = 0;

    //------------ Level & Quota UI ------------
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI quotaText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateAllUI();
    }

    // DIPANGGIL setiap kali cacing/semut/kumbang tertangkap
    public void AddItem(string itemName)
    {
        if (itemName == "Cacing")
        {
            cacingCount++;

            // Laporkan ke LevelManager agar quota naik
            if (LevelManager.Instance != null)
                LevelManager.Instance.AddWorm();
        }
        else if (itemName == "Semut")
        {
            semutCount++;
        }
        else if (itemName == "Kumbang")
        {
            kumbangCount++;
        }

        UpdateAllUI();
    }

    // UPDATE semua UI (Score + Level + Quota)
    void UpdateAllUI()
    {
        if (scoreText != null)
        {
            scoreText.text = 
                "Cacing: " + cacingCount + "\n" +
                "Semut: " + semutCount + "\n" +
                "Kumbang: " + kumbangCount;
        }

        if (levelText != null)
            levelText.text = "Level: " + GameData.Instance.currentLevel;

        if (quotaText != null)
            quotaText.text = "Quota: " + cacingCount + " / " + GameData.Instance.quotaPerLevel;
    }
}
