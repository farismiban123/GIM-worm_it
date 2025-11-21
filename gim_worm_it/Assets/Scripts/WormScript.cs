using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public Transform target;       // Stake
    public float startSpeed = 4f;  // Kecepatan awal
    public float maxSpeed = 5f;    // Kecepatan maksimal
    public float acceleration = 1f; // Percepatan

    public GameObject BagianCacing;
    public GameObject BagianTanah;
    private Animator wormAnimator;
    private Animator tanahAnimator;
    private Collider2D wormCollider;

    private float currentSpeed;

    public float waktuSpawn = 1.519f;
    float time = 0f;
    bool hasSpawned = false;
    bool canClick = false;

    void Start()
    {
        currentSpeed = startSpeed;
        wormAnimator = BagianCacing.GetComponent<Animator>();
        tanahAnimator = BagianTanah.GetComponent<Animator>();
        wormCollider = GetComponent<Collider2D>();
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
            //Destroy(gameObject);
        }
    }

    public void CacingMasuk()
    {
        Destroy(gameObject);
    }

    public void CacingKeluar()
    {
        canClick = true;
        wormCollider.enabled = true;
    }

    void digUp()
    {
        wormAnimator.SetTrigger("Keluar");
        tanahAnimator.SetTrigger("Keluar");
    }
    
    public void click()
    {
        if(canClick == true)
        {
            UIManager.Instance.AddItem("Cacing");
            Destroy(gameObject); 
        }
    }
}
