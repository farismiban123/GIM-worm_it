using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public Transform target;       // Stake
    public float startSpeed = 2f;  // Kecepatan awal
    public float maxSpeed = 5f;    // Kecepatan maksimal
    public float acceleration = 1f; // Percepatan

    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
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
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
        );

        // Kalo udah sampe stake, hancur
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
