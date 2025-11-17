using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //biar bisa dipanggil di script lain
    public TextMeshProUGUI scoreText; 
    public int cacingCount = 0;
    public int semutCount = 0;
    public int kumbangCount = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText(); 
    }

    public void AddItem(string itemName)
    {
        if (itemName == "Cacing")
        {
            cacingCount++;
        }
        else if (itemName == "Semut")
        {
            semutCount++;
        }
        else if (itemName == "Kumbang")
        {
            kumbangCount++;
        }

        UpdateScoreText(); 
    }

    void UpdateScoreText()
    {
        scoreText.text = "Cacing: " + cacingCount + "\n" +
                         "Semut: " + semutCount + "\n" +
                         "Kumbang: " + kumbangCount;
    }
}
