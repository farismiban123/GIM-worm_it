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

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        GameObject clickedObject = rayHit.collider.gameObject;

        if (clickedObject.CompareTag("Wooden Stake"))
            return;

        UIManager.Instance.AddItem(clickedObject.name);
        Destroy(clickedObject);

        Debug.Log(clickedObject.name);
    }
}
