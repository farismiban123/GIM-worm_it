using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject WoodenStake;
    public float percentageToSpawnWorm = 0.1f;

    public GameObject wormPrefab;
    public GameObject antPrefab;
    public GameObject kumbangPrefab;

    private Animator WoodenStakeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    public void ButtonPressed()
    {
        if(Random.value <= percentageToSpawnWorm)
        {
            SpawnWorm();
            //percentageToSpawnWorm = 0.1f;
        }
        else
        {
            if(percentageToSpawnWorm < 0.3f)
            {
                percentageToSpawnWorm *= 1.1f;
            }
        }

    }

    void SpawnWorm()
    {
        Debug.Log("Worm spawned");
        Instantiate(wormPrefab, new Vector3(Random.Range(8,-8),Random.Range(0, 4),0), Quaternion.identity);
    }
}
