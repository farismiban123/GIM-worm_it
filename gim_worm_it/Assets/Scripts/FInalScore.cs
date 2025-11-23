using UnityEngine;
using TMPro; 

public class FInalScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void UpdateScoreText()
    {
        scoreText.text = "Cacing: " + GameData.Instance.fincacingCount + "\n" +
                         "Semut: " + GameData.Instance.finisopodCount;
    }
    void Start()
    {
        UpdateScoreText();
    }
}            