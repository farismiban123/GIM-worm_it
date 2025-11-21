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

	//---------------------------------
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
}
