using UnityEngine;
using System.Collections;
using TMPro;

public class AutoCollectManager : MonoBehaviour
{
    public static AutoCollectManager Instance;

    public GameObject potionButton; // Tombol pake potion
    public GameObject kantongIcon;  // Icon kantong setelah dipakai
    public Transform kantongTarget; // Target animasi cacing masuk

    public TextMeshProUGUI potionStackText;
    public TextMeshProUGUI potionTimerText;
    private float activeTime = 0f;
    private bool autoRunning = false;



    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Instance = this;
    }

    void Start()
    {
        // reset pemakaian potion di level setiap masuk level baru
        GameData.Instance.potionUsedThisLevel = 0;

        // Tombol hanya muncul jika masih ada potion di inventory
        potionButton.SetActive(GameData.Instance.potionCount > 0);

        // Hitung slot tersedia di level
        int slotLevel = GameData.Instance.maxPotionUsePerLevel;
        int inventoryPotion = GameData.Instance.potionCount;

        // UI selalu berdasarkan slot level
        int displayCount = Mathf.Min(inventoryPotion, slotLevel);
        potionStackText.text = "x" + displayCount;

        kantongIcon.SetActive(false);
        potionTimerText.text = "";
    }




    void Update()
    {
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
            potionTimerText.text = Mathf.Ceil(activeTime).ToString();

            if (!autoRunning)
            {
                autoRunning = true;
                StartCoroutine(AutoCollectRoutine());
            }
        }
        else
        {
            potionTimerText.text = "";
            kantongIcon.SetActive(false);
            autoRunning = false;
        }
    }



   public void UsePotion()
{
    if (GameData.Instance.potionUsedThisLevel >= GameData.Instance.maxPotionUsePerLevel) return;
    if (GameData.Instance.potionCount <= 0) return;

    // kurangi inventory
    GameData.Instance.potionCount--;

    // kurangi kuota pakai per level
    GameData.Instance.potionUsedThisLevel++;

    activeTime = GameData.Instance.potionDuration;
    kantongIcon.SetActive(true);

    // hitung potion TERSISA yang BISA digunakan di level ini
    int remainingSlot = GameData.Instance.maxPotionUsePerLevel - GameData.Instance.potionUsedThisLevel;

    // hitung potion TERSISA di inventory
    int inventoryPotion = GameData.Instance.potionCount;

    // UI menampilkan MIN(remainingSlot, inventoryPotion)
    int display = Mathf.Min(remainingSlot, inventoryPotion);

    potionStackText.text = "x" + display;

    if (display <= 0)
        potionButton.SetActive(false);
}






    IEnumerator AutoCollectRoutine()
    {
        while (activeTime > 0) // jalan terus sampai scene ganti
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
            if (w != null && w.autoCollectAllowed && w.hasSpawned && w.canClickReady())
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
        audioManager.PlaySFX(audioManager.tapCacing);

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
