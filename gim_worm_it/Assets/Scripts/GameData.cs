using UnityEngine;

public class GameData : MonoBehaviour
{
	//---------------------------------Makhluk
	public static GameData Instance;
    public int fincacingCount = 0;
    public int finisopodCount = 0;

	//--------------------------------- KOIN
	public int coins = 0;                  // Koin pemain
	public int wormUpgradeLevel = 1;       // Level upgrade mulai dari 1
	public int wormUpgradeCost = 30;       // Harga pertama 20 coin


	//---------------------------------POTION
	public int potionCount = 0;
	public int maxAutoCollect = 15;

	public int maxPotionUsePerLevel = 3;
	public int potionUsedThisLevel = 0;
	public float potionDuration = 5f;

	public int baseQuota = 10;
	public int quotaIncreasePerLevel = 2;
	


	//---------------------------------Level dan kuota

	public int currentLevel = 1;    // mulai dari level 1
	public int quotaPerLevel = 15;  // quota awal
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
        quotaPerLevel = baseQuota + ((currentLevel - 1) * quotaIncreasePerLevel);

    }
}
