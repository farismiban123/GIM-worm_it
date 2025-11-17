using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject WoodenStake;
    public float percentageToSpawnWorm = 0.1f;

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
        }

    }

    void SpawnWorm()
    {
        Debug.Log("Worm spawned");
    }
}
