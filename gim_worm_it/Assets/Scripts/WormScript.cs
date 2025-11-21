using UnityEngine;
using System.Collections;


public class WormMovement : MonoBehaviour
{
    public Transform target;       // Stake
    public float startSpeed = 2f;  // Kecepatan awal
    public float maxSpeed = 5f;    // Kecepatan maksimal
    public float acceleration = 1f; // Percepatan

    private float currentSpeed;

    float waktuSpawn;
    float time = 0f;
    [HideInInspector] public bool hasSpawned = false;


    //------------autocollect allowed
    public bool autoCollectAllowed = false;  
    public float autoCollectDelay = 0.2f;     

    bool disableDestroy = false; 

    //---------------------------------
    void Start()
    {
        currentSpeed = startSpeed;
        waktuSpawn = Random.Range(2f, 4f);

        StartCoroutine(AutoCollectDelayRoutine());
    }

    IEnumerator AutoCollectDelayRoutine()
    {
        yield return new WaitForSeconds(autoCollectDelay);
        autoCollectAllowed = true; // auto colect baru boleh
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
            if (!disableDestroy)   // hanya destroy jika bukan hasil auto-collect
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
    //disable destroy buat kalo kita punya kantong ga tabrakan


    public void DisableAutoDestroy()
    {
        disableDestroy = true;
    }

}
