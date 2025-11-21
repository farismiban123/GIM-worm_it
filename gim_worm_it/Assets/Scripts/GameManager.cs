using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject WoodenStake;
    public GameObject WoodenStakeCenter;
    public float percentageToSpawnWorm = 0.1f;

    public GameObject wormPrefab;
    public GameObject tanahPrefab;
    public GameObject antPrefab;
    public GameObject kumbangPrefab;

    private Animator WoodenStakeAnimator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        WoodenStakeAnimator = WoodenStake.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        percentageToSpawnWorm -= 0.0001f;
        if(percentageToSpawnWorm < 0.1f)
        {
            WoodenStakeAnimator.SetFloat("Speed", 0);
            percentageToSpawnWorm = 0.1f;
        }
        else
        {
            WoodenStakeAnimator.SetFloat("Speed", percentageToSpawnWorm * 5);
        }

        if (Input.GetMouseButtonDown(0))
        { 
            if (Time.timeScale == 0) return;

            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

            //Debug.Log("Press");
            //ButtonPressed(); 
        }
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

    //spawn tanah
    public void SpawnTanah()
    {
        
        // 1. Get the spawn position from the screen edge (same as cacing/worm)
        Vector3 spawnPos = GetSpawnFromScreenEdge();
        
        // 2. Instantiate Tanah
        GameObject newTanah = Instantiate(tanahPrefab, spawnPos, Quaternion.identity);

        // 3. Set rotation to face the WoodenStakeCenter
        
        // Calculate the direction vector
        Vector3 directionToTarget = WoodenStakeCenter.transform.position - newTanah.transform.position;
        
        // Calculate the angle using Mathf.Atan2
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        
        // Apply the rotation with the -90 degree adjustment
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
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(offset, Random.Range(0.3f, 1f - offset), zDistance));
                break;
            case 1: // kanan
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1f- offset, Random.Range(0.3f, 1f) - offset, zDistance));
                break;
            case 2: // atas
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(offset, 1f - offset), 1f - offset, zDistance));
                break;
        }

        spawnPos.z = 0; 
        return spawnPos;
    }
}
