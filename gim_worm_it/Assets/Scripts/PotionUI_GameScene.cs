using UnityEngine;
using TMPro;

public class PotionUI_GameScene : MonoBehaviour
{
    public TextMeshProUGUI potionText;

    void Update()
    {
        potionText.text = "Potion: " + GameData.Instance.potionCount;
    }
}
