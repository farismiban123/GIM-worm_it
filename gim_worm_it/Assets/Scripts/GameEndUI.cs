using UnityEngine;
using TMPro;

public class GameEndUI : MonoBehaviour
{
    public TextMeshProUGUI resultText; // SATU TEXT SAJA

    void Start()
    {
        // Ambil hasil akhir dari UIManager
        int cacing = UIManager.Instance.cacingCount;
        int semut = UIManager.Instance.semutCount;
        int kumbang = UIManager.Instance.kumbangCount;

        // Tampilkan ke UI
        resultText.text =
            "Cacing: " + cacing + "\n" +
            "Semut: " + semut + "\n" +
            "Kumbang: " + kumbang;
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
