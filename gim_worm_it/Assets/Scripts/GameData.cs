using UnityEngine;

public class GameData : MonoBehaviour
{
	public static GameData Instance;
    public int fincacingCount = 0;
    public int finsemutCount = 0;
    public int finkumbangCount = 0;

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
