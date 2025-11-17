using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        GameObject clickedObject = rayHit.collider.gameObject;
        string itemName = clickedObject.name;
        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.AddItem(itemName);
        }

        Destroy(clickedObject);


        Debug.Log(rayHit.collider.gameObject.name);
    }
}
