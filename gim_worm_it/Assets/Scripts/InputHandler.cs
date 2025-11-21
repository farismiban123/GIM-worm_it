using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        // Kita pakai Input lama (GetMouseButtonDown) karena lebih aman dari warning UI
        // Dan karena settingmu sudah "Both", ini pasti jalan.
        if (Input.GetMouseButtonDown(0))
        {
            // Cek UI (Tidak akan warning lagi karena dipanggil di Update)
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Logika Raycast (Tembak Sinar)
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
            if (!rayHit.collider) return;

            GameObject clickedObject = rayHit.collider.gameObject;

            if (clickedObject.CompareTag("Wooden Stake"))
                return;

            if (clickedObject.CompareTag("Cacing"))
            {
                Debug.Log("Cacing");
                if (UIManager.Instance != null)
                {
                    // Panggil fungsi klik di cacing
                    var wormScript = clickedObject.GetComponent<WormMovement>();
                    if (wormScript != null) wormScript.click();
                }
                return;
            }

            if (clickedObject.CompareTag("Background")) 
            {
                GameManager.Instance.ButtonPressed();
                
            }
        }
    }
}
