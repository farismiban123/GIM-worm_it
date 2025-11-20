using UnityEngine;
using TMPro;

public class ShopUpgrade : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;

    void Start()
    {
        UpdateUI();
    }

    public void Upgrade()
    {
        int cost = GameData.Instance.wormUpgradeCost;

        // Kalau koin ga cukup ga bisa beli
        if (GameData.Instance.coins < cost)
            return;

        // Kurangi koin
        GameData.Instance.coins -= cost;

        // Naik level
        GameData.Instance.wormUpgradeLevel++;

        // Harga naik 1.5x
        GameData.Instance.wormUpgradeCost =
            Mathf.RoundToInt(GameData.Instance.wormUpgradeCost * 1.5f);

        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = "Level: " + GameData.Instance.wormUpgradeLevel;
        costText.text = "Cost: " + GameData.Instance.wormUpgradeCost;
    }
}

