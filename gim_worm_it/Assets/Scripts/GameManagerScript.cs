using UnityEngine;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public GameObject WoodenStake;
    public GameObject WoodenStakeCenter;
    public float baseMaxChanceToSpawnCreature = 0.2f;
    public float percentageToSpawnCreature = 0.1f;
    public float maxPercentageToSpawnCreature = 0.3f;
    public float changeToSpawnWorm = 0.9f;


    public GameObject wormPrefab;
    public GameObject isopodPrefab;
    public GameObject tanahPrefab;

    private Animator WoodenStakeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        WoodenStakeAnimator = WoodenStake.GetComponent<Animator>();

        //percentageToSpawnCreature += GameData.Instance.wormUpgradeLevel * 0.05f;

        maxPercentageToSpawnCreature = baseMaxChanceToSpawnCreature + GameData.Instance.wormUpgradeLevel * 0.05f;

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
        percentageToSpawnCreature -= 0.00001f;
        if(percentageToSpawnCreature < 0.1f)
        {
            WoodenStakeAnimator.SetFloat("Speed", 0);
            percentageToSpawnCreature = 0.1f;
            //percentageToSpawnCreature = Mathf.Max(0f, percentageToSpawnCreature);
        }
        else
        {
            WoodenStakeAnimator.SetFloat("Speed", percentageToSpawnCreature * 5);
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
        if (Random.value < percentageToSpawnCreature)
        {
            if (Random.value < changeToSpawnWorm)
            {
                SpawnCreature(wormPrefab);
            }
            else SpawnCreature(isopodPrefab);
        }

        if(percentageToSpawnCreature < maxPercentageToSpawnCreature)
        {
            percentageToSpawnCreature *= 1.05f;
        }
    }

    // Spawn semua jenis binatang 
    void SpawnCreature(GameObject prefab)
    {
        Vector3 spawnPos = GetSpawnFromScreenEdge();
        GameObject creature = Instantiate(prefab, spawnPos, Quaternion.identity);
        GameObject newTanah = Instantiate(tanahPrefab, spawnPos, Quaternion.identity);

        if(prefab == isopodPrefab)
        {
            Isopod isopodMovementScript = creature.GetComponent<Isopod>();
            isopodMovementScript.target = WoodenStakeCenter.transform;
            isopodMovementScript.startSpeed = 2.5f;
            isopodMovementScript.maxSpeed = 5f;
            isopodMovementScript.acceleration = 0.5f;
        }
        else if(prefab == wormPrefab)
        {
            WormMovement wormMovementScript = creature.GetComponent<WormMovement>();
            wormMovementScript.target = WoodenStakeCenter.transform;
            wormMovementScript.startSpeed = 2.5f;
            wormMovementScript.maxSpeed = 5f;
            wormMovementScript.acceleration = 0.5f;
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
