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
            UIManager.Instance.AddItem("Cacing");

            // tambah coin
            GameData.Instance.coins += 2;

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

        // Simpan scale awal cacing
        Vector3 startScale = cacing.transform.localScale;

        // Bikin animasi sedikit melengkung + random offset kecil
        Vector3 mid = (start + end) * 0.5f;
        mid.y += 1f;

        while (t < 1f)
        {
            if (cacing == null) yield break;

            t += Time.deltaTime / duration;

            // Smoothstep
            float s = t * t * (3 - 2 * t);

            Vector3 a = Vector3.Lerp(start, mid, s);
            Vector3 b = Vector3.Lerp(mid, end, s);
            cacing.transform.position = Vector3.Lerp(a, b, s);

            //scale menegcil makin ke kantong
            float shrink = Mathf.Lerp(1f, 0f, s);

            // terapkan ke scale cacing
            cacing.transform.localScale = startScale * shrink;

            yield return null;
        }

        Destroy(cacing);
    }



}
