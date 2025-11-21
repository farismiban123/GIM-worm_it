using UnityEngine;

public class GameData : MonoBehaviour
{
	//---------------------------------Makhluk
	public static GameData Instance;
    public int fincacingCount = 0;
    public int finsemutCount = 0;
    public int finkumbangCount = 0;

	//--------------------------------- KOIN
	public int coins = 0;                  // Koin pemain
	public int wormUpgradeLevel = 1;       // Level upgrade mulai dari 1
	public int wormUpgradeCost = 20;       // Harga pertama 20 coin


	//---------------------------------POTION
	public int potionCount = 0;
	public int maxAutoCollect = 15;

	//---------------------------------Level dan kuota

	public int currentLevel = 1;    // mulai dari level 1
	public int quotaPerLevel = 30;  // quota awal
	public float levelTime = 30f; 
	public int coinsBeforeLevel = 0;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			UpdateQuota();
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void UpdateQuota()
    {
        // Level 1 = 30, naik 5 tiap level
        quotaPerLevel = 30 + ((currentLevel - 1) * 5);
    }
}
