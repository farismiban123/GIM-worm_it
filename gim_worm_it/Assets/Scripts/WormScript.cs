using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public Transform target;       // Stake
    public float startSpeed = 2f;  // Kecepatan awal
    public float maxSpeed = 5f;    // Kecepatan maksimal
    public float acceleration = 1f; // Percepatan

    private float currentSpeed;

    float waktuSpawn;
    float time = 0f;
    bool hasSpawned = false;

    void Start()
    {
        currentSpeed = startSpeed;
        waktuSpawn = Random.Range(2f, 4f);
    }

    void Update()
    {
        if (target == null) return;

        // Percepat hingga maxSpeed
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed)
                currentSpeed = maxSpeed;
        }


        // Gerak ke target
        if(hasSpawned == false)
        {
            transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
            );
        }

        // Kalo udah deket ke stake, destroy 
        // if (Vector3.Distance(transform.position, target.position) < 0.1f)
        // {
        //     Destroy(gameObject);
        // }

        time += Time.deltaTime;
        if (time > waktuSpawn && hasSpawned == false)
        {
            digUp();
            hasSpawned = true;
        }

        if (time > waktuSpawn + 5f && hasSpawned == true)
        {
            Destroy(gameObject);
        }
    }

    void digUp()
    {
        Debug.Log("Dig Up");
    }
    
    public void click()
    {
        if(hasSpawned == true)
        {
           UIManager.Instance.AddItem(gameObject.name);
            Destroy(gameObject); 
        }
    }
}
