using UnityEngine;
using TMPro;

public class CoinsTambah : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public Canvas canvas;

    public void ShowText(string message, Color color)
    {
        GameObject obj = Instantiate(floatingTextPrefab, canvas.transform);
        obj.transform.position = Input.mousePosition;

        TextMeshProUGUI txt = obj.GetComponent<TextMeshProUGUI>();
        txt.text = message;
        txt.color = color;

        Destroy(obj, 1f);
    }
}
