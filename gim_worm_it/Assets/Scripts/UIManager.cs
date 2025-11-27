using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText; 
    public int cacingCount = 0;
    public int isopodCount = 0;

    //------------ Level & Quota UI ------------
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI quotaText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
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
        else if (itemName == "Isopod")
        {
            isopodCount++;
        }

        UpdateAllUI();
    }

    // UPDATE semua UI (Score + Level + Quota)
    void UpdateAllUI()
    {
        if (scoreText != null)
        {
            scoreText.text = 
                ": " + cacingCount + "\n" +
                ": " + isopodCount;
        }

        if (levelText != null)
        {
            if (GameData.Instance != null)
                levelText.text = "Level: " + GameData.Instance.currentLevel;
            else
                levelText.text = "Level: -";
        }

        if (quotaText != null)
        {
            if (GameData.Instance != null)
                quotaText.text = "Quota: " + cacingCount + " / " + GameData.Instance.quotaPerLevel;
            else
                quotaText.text = "Quota: " + cacingCount + " / -";
        }
    }
}
