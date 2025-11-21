using UnityEngine;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public GameObject WoodenStake;
    public GameObject WoodenStakeCenter;
    public float percentageToSpawnWorm = 0.1f;

    public GameObject wormPrefab;
    public GameObject antPrefab;
    public GameObject kumbangPrefab;
    public GameObject tanahPrefab;

    private Animator WoodenStakeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        WoodenStakeAnimator = WoodenStake.GetComponent<Animator>();

        percentageToSpawnWorm += GameData.Instance.wormUpgradeLevel * 0.05f;

    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        percentageToSpawnWorm -= 0.00001f;
        if(percentageToSpawnWorm < 0.1f)
        {
            WoodenStakeAnimator.SetFloat("Speed", 0);
            percentageToSpawnWorm = Mathf.Max(0f, percentageToSpawnWorm);
        }
        else
        {
            WoodenStakeAnimator.SetFloat("Speed", percentageToSpawnWorm * 5);
        }

        // if (Input.GetMouseButtonDown(0))
        // { 
        //     if (Time.timeScale == 0) return;

        //     if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        //     ButtonPressed(); 
        // }
    }

    public void ButtonPressed()
    {
        if (Random.value <= percentageToSpawnWorm)
            SpawnCreature(wormPrefab);

        // if (Random.value <= 0.1f) // peluang spawn semut
        //     SpawnCreature(antPrefab);

        // if (Random.value <= 0.05f) // peluang spawn kumbang
        //     SpawnCreature(kumbangPrefab);

        if(percentageToSpawnWorm < 0.3f)
        {
            percentageToSpawnWorm *= 1.1f;
        }
    }

    // Spawn semua jenis binatang 
    void SpawnCreature(GameObject prefab)
    {
        Vector3 spawnPos = GetSpawnFromScreenEdge();
        GameObject creature = Instantiate(prefab, spawnPos, Quaternion.identity);
        GameObject newTanah = Instantiate(tanahPrefab, spawnPos, Quaternion.identity);

        WormMovement wm = creature.GetComponent<WormMovement>();
        if (wm != null)
        {
            wm.target = WoodenStakeCenter.transform;
            wm.startSpeed = 2.5f;
            wm.maxSpeed = 5f;
            wm.acceleration = 0.5f;
        }

        //buat tanah
        Vector3 directionToTarget = WoodenStakeCenter.transform.position - newTanah.transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        newTanah.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    // Spawn dari ujung layar: kiri, kanan, atau atas
    Vector3 GetSpawnFromScreenEdge()
    {
        Vector3 spawnPos = Vector3.zero;
        float zDistance = 0f; 
        float offset = Random.Range(0.05f, 0.1f);
        //Debug.Log(offset);

        int edge = Random.Range(0, 3); 

        switch (edge)
        {
            case 0: // kiri
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0, Random.Range(0.3f, 1f), zDistance));
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(offset, Random.Range(0.3f, 1f - offset), zDistance));
                break;
            case 1: // kanan
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1, Random.Range(0.3f, 1f), zDistance));
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1f- offset, Random.Range(0.3f, 1f) - offset, zDistance));
                break;
            case 2: // atas
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1, zDistance));
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(offset, 1f - offset), 1f - offset, zDistance));
                break;
        }
        spawnPos.z = 0; 
        return spawnPos;
    }
}
