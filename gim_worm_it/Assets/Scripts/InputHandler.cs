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
            // logika raycast
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
            if (!rayHit.collider) return;

            GameObject clickedObject = rayHit.collider.gameObject;

            if (clickedObject.CompareTag("Wooden Stake")) return;

            if (clickedObject.CompareTag("Cacing"))
            {
                coinsTambah.ShowText("+2", Color.green); 

                if (UIManager.Instance != null)
                {
                    //panggil fungsi klik di isopod
                    var wormScript = clickedObject.GetComponent<WormMovement>();
                    if (wormScript != null) wormScript.click();
                }
                return;
            }

            if (clickedObject.CompareTag("Isopod"))
            {
                coinsTambah.ShowText("-4", Color.red);
                
                if (UIManager.Instance != null)
                {
                    var isopodScript = clickedObject.GetComponent<Isopod>();
                    if (isopodScript != null) isopodScript.click();
                }
                return;
            }

            if (clickedObject.CompareTag("Background")) 
            {
                GameManagerScript.Instance.ButtonPressed();
            }
        }
    }
}