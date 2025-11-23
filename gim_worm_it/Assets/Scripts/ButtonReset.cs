using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIReset : MonoBehaviour
{
    private Animator anim;
    private RectTransform rect;

    [Header("Nama State Normal (Cek di Animator)")]
    public string normalStateName = "Normal"; 

    void Awake()
    {
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        // 1. Reset Ukuran Fisik (Backup kalau animasi gagal)
        if (rect != null) rect.localScale = Vector3.one;

        // 2. Reset Seleksi (Hapus kotak highlight)
        StartCoroutine(ClearSelection());

        // 3. PAKSA ANIMATOR BERSIH
        if (anim != null)
        {
            // Hapus semua trigger yang mungkin nyangkut
            anim.ResetTrigger("Normal");
            anim.ResetTrigger("Highlighted");
            anim.ResetTrigger("Pressed");
            anim.ResetTrigger("Selected");
            anim.ResetTrigger("Disabled");

            // Paksa mainkan state Normal
            anim.Play(normalStateName, -1, 0f);
            anim.Update(0f);
        }
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