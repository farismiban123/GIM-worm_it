using UnityEngine;
using System.Collections;

public class AutoCollectManager : MonoBehaviour
{
    public static AutoCollectManager Instance;

    public GameObject potionButton; // Tombol pake potion
    public GameObject kantongIcon;  // Icon kantong setelah dipakai
    public Transform kantongTarget; // Target animasi cacing masuk

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Tombol potion hanya muncul jika punya potion
        potionButton.SetActive(GameData.Instance.potionCount > 0);

        // Kantong belum muncul sebelum dipakai
        kantongIcon.SetActive(false);
    }

    public void UsePotion()
    {
        // kalau ga ada potion gabisa
        if (GameData.Instance.potionCount <= 0) return;

        // kurangin potion
        GameData.Instance.potionCount--;

        // tombol ilang, muncul kantong
        potionButton.SetActive(false);
        kantongIcon.SetActive(true);

        Debug.Log("POTION DIPAKAI — AutoCollect START");

        // Mulai proses auto collect
        StartCoroutine(AutoCollectRoutine());
    }

    IEnumerator AutoCollectRoutine()
    {
        while (true) // jalan terus sampai scene ganti
        {
            GameObject cacing = FindCollectableWorm();
            if (cacing == null)
            {
                yield return null;
                continue;
            }

            var worm = cacing.GetComponent<WormMovement>();
            if (worm != null)
                worm.DisableAutoDestroy();

            yield return StartCoroutine(AnimateToBag(cacing));

            // tambah ke UI
            UIManager.Instance.AddItem("Cacing(Clone)");

            // tambah coin
            GameData.Instance.coins++;

            yield return new WaitForSeconds(0.1f); // jarak antar collect
        }
    }


    GameObject FindCollectableWorm()
    {
        WormMovement[] worms = Object.FindObjectsByType<WormMovement>(FindObjectsSortMode.None);


        foreach (WormMovement w in worms)
        {
            if (w != null && w.autoCollectAllowed && w.hasSpawned)
                return w.gameObject;
        }

        return null;
    }

    //cacing terbang
    IEnumerator AnimateToBag(GameObject cacing)
    {
        if (cacing == null) yield break;

        // Matikan movement agar tidak melawan animasi
        var move = cacing.GetComponent<WormMovement>();
        if (move != null) move.enabled = false;

        Vector3 start = cacing.transform.position;
        Vector3 end = kantongTarget.position;

        float duration = 0.6f;
        float t = 0;

        // Bikin animasi sedikit melengkung 
        Vector3 mid = (start + end) * 0.5f;
        mid.y += 1f; // naik sedikit biar mantep

        while (t < 1f)
        {
            if (cacing == null) yield break;

            t += Time.deltaTime / duration;

            // Smoothstep
            float s = t * t * (3 - 2 * t);

            // Interpolasi ke titik tengah dulu biar melengkung
            Vector3 a = Vector3.Lerp(start, mid, s);
            Vector3 b = Vector3.Lerp(mid, end, s);

            cacing.transform.position = Vector3.Lerp(a, b, s);

            yield return null;
        }

        Destroy(cacing);
    }


}
