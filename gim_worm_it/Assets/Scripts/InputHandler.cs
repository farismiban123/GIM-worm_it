using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public CoinsTambah coinsTambah; 

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Logika Raycast
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
            if (!rayHit.collider) return;

            GameObject clickedObject = rayHit.collider.gameObject;

            if (clickedObject.CompareTag("Wooden Stake"))
                return;

            if (clickedObject.CompareTag("Cacing"))
            {
                Debug.Log("Cacing");
                GameData.Instance.coins += 2;         
                coinsTambah.ShowText("+2", Color.green); 

                if (UIManager.Instance != null)
                {
                    // Panggil fungsi klik di cacing
                    var wormScript = clickedObject.GetComponent<WormMovement>();
                    if (wormScript != null) wormScript.click();
                }
                return;
            }

            if (clickedObject.CompareTag("Isopod"))
            {
                Debug.Log("Isopod");
                GameData.Instance.coins -= 4;       
                coinsTambah.ShowText("-4", Color.red);
                
                if (UIManager.Instance != null)
                {
                    // Panggil fungsi klik di isopod
                    var isopodScript = clickedObject.GetComponent<Isopod>();
                    if (isopodScript != null) isopodScript.click();
                }
                return;
            }

            if (clickedObject.CompareTag("Background")) 
            {
                GameManagerScript.Instance.ButtonPressed();

            }

            // GameData.Instance.coins += 1;
            // Destroy(clickedObject);
            // Debug.Log(clickedObject.name);
        }
    }
}
