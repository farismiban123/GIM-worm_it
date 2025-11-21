using UnityEngine;
using TMPro;

public class ShopPotion : MonoBehaviour
{
    public TextMeshProUGUI costText;
    public TextMeshProUGUI potionOwnedText;

    int cost = 50;

    void Start()
    {
        costText.text = cost + " Coins";
        potionOwnedText.text = "Potion Owned: " + GameData.Instance.potionCount; // ambil dari gamedata countnya
    }

    public void BuyPotion()
    {
        if (GameData.Instance.coins < cost) return; //kalo ga cukup gabisa

        GameData.Instance.coins -= cost;
        GameData.Instance.potionCount++;

        potionOwnedText.text = "Potion Owned: " + GameData.Instance.potionCount;
    }
}
