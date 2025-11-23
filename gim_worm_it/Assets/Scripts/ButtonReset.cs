using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonReset : MonoBehaviour
{
    private Animator anim;
    private RectTransform rect;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        if (rect != null)
        {
            rect.localScale = Vector3.one;
        }

        if (anim != null)
        {
            anim.keepAnimatorStateOnDisable = false;
            
            anim.Rebind();
            anim.Update(0f);
        }
        StartCoroutine(ClearSelection());
    }

    System.Collections.IEnumerator ClearSelection()
    {
        yield return null;
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}