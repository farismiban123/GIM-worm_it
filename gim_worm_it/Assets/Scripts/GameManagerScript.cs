using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject WoodenStake;
    public float percentageToSpawnWorm = 0.1f;

    public GameObject wormPrefab;
    public GameObject antPrefab;
    public GameObject kumbangPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        if(Random.value <= percentageToSpawnWorm)
        {
            SpawnWorm();
            percentageToSpawnWorm = 0.1f;
        }
        else
        {
            percentageToSpawnWorm *= 1.1f;
        }

    }

    void SpawnWorm()
    {
        Debug.Log("Worm spawned");
        Instantiate(wormPrefab, new Vector3(Random.Range(8,-8),Random.Range(0, 4),0), Quaternion.identity);
    }
}
