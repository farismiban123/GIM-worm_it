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
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Logika Raycast
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
            if (!rayHit.collider) return;

            GameObject clickedObject = rayHit.collider.gameObject;

            if (clickedObject.CompareTag("Wooden Stake"))
                return;

            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddItem(clickedObject.name);
            }

            GameData.Instance.coins += 1;
            Destroy(clickedObject);
            Debug.Log(clickedObject.name);
        }
    }
}
