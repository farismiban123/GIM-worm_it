using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // jumlah worms yang dikumpulkan dalam level berjalan
    public int wormsCollectedThisLevel = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameData.Instance != null)
            GameData.Instance.UpdateQuota();

        // reset collector saat manager pertama kali dibuat
        wormsCollectedThisLevel = 0;
    }

    // DIPANGGIL dari UIManager.AddItem setiap ambil cacing
    public void AddWorm()
    {
        wormsCollectedThisLevel++;
        Debug.Log("Collected: " + wormsCollectedThisLevel + "/" + GameData.Instance.quotaPerLevel);
    }

    // DIPANGGIL oleh CountDown.cs saat waktu habis
    public bool HasReachedQuota()
    {
        return wormsCollectedThisLevel >= GameData.Instance.quotaPerLevel;
    }

    // Dipanggil dari GameEnd UI ketika next
    public void GoToNextLevel()
    {
        // naik level & update quota
        GameData.Instance.currentLevel++;
        GameData.Instance.UpdateQuota();

        // reset counter
        wormsCollectedThisLevel = 0;

        // load kembali GameScene
        SceneManager.LoadScene("GameScene");
    }

    public void RetryLevel()
    {
        // restore worms count dan reload
        wormsCollectedThisLevel = 0;
        SceneManager.LoadScene("GameScene");
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            // reset collected worm untuk level baru
            wormsCollectedThisLevel = 0;

            // simpan coins sebelum level dimulai (backup)
            if (GameData.Instance != null)
                GameData.Instance.coinsBeforeLevel = GameData.Instance.coins;

            // reset UI count kalo UIManager sudah ada
            if (UIManager.Instance != null)
            {
                UIManager.Instance.cacingCount = 0;
                UIManager.Instance.semutCount = 0;
                UIManager.Instance.kumbangCount = 0;
            }

            if (GameData.Instance != null)
                GameData.Instance.UpdateQuota();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
