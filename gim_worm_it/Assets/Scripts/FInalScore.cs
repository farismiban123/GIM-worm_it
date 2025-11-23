using UnityEngine;
using TMPro; 

public class FInalScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void UpdateScoreText()
    {
        scoreText.text = ": " + GameData.Instance.fincacingCount + "\n" +
                         ": " + GameData.Instance.finisopodCount;
    }
    void Start()
    {
        UpdateScoreText();
    }
}            