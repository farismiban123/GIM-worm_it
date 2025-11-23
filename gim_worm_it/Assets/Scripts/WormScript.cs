using UnityEngine;
using System.Collections;

public class WormMovement : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public Transform target;       // Stake
    public float startSpeed = 2f;  // Kecepatan awal
    public float maxSpeed = 5f;    // Kecepatan maksimal
    public float acceleration = 1f; // Percepatan

    private float currentSpeed;

    public GameObject BagianCacing;
    public GameObject BagianTanah;
    private Animator wormAnimator;
    private Animator tanahAnimator;
    private Collider2D wormCollider;
    

    public float waktuSpawn = 1.519f;
    float time = 0f;
    [HideInInspector] public bool hasSpawned = false;
    bool canClick = false;


    //------------autocollect allowed
    public bool autoCollectAllowed = false;  
    public float autoCollectDelay; 

    bool disableDestroy = false; 

    //---------------------------------
    void Start()
    {
        currentSpeed = startSpeed;
        wormAnimator = BagianCacing.GetComponent<Animator>();
        tanahAnimator = BagianTanah.GetComponent<Animator>();
        wormCollider = GetComponent<Collider2D>();

        autoCollectDelay = waktuSpawn;

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
            audioManager.PlaySFX(audioManager.tapCacing);
            UIManager.Instance.AddItem("Cacing");
            Destroy(gameObject); 
            GameData.Instance.coins += 1;
        }
    }
    //disable destroy buat kalo kita punya kantong ga tabrakan


    public void DisableAutoDestroy()
    {
        disableDestroy = true;
    }

}
