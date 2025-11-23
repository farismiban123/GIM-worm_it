using UnityEngine;
using TMPro;

public class GameEndUI : MonoBehaviour
{
    public TextMeshProUGUI resultText; // SATU TEXT SAJA

    void Start()
    {
        // Ambil hasil akhir dari UIManager
        int cacing = UIManager.Instance.cacingCount;
        int isopod = UIManager.Instance.isopodCount;

        // Tampilkan ke UI
        resultText.text =
            ": " + cacing + "\n" +
            ": " + isopod;
    }

    public void NextLevel()
    {
        LevelManager.Instance.GoToNextLevel();
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
