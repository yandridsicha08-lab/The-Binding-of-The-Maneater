using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float maxSpeed = 50f;
    public float sprintMultiplier = 0.2f;
    public float rotationSpeed = 5f;

    public PlayerHealth PlayerHealthReal;
    public Transform sharkVisual;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (PlayerHealthReal.currentHealth <= 0)
            return;

        // --- INPUT ---
        moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection.y = 1;
        if (Input.GetKey(KeyCode.S)) moveDirection.y = -1;
        if (Input.GetKey(KeyCode.A)) moveDirection.x = -1;
        if (Input.GetKey(KeyCode.D)) moveDirection.x = 1;

        moveDirection = moveDirection.normalized;

        // --- SPRINT ---
        if (Input.GetKey(KeyCode.LeftShift) && currentSpeed < maxSpeed)
            currentSpeed = baseSpeed * (1 + sprintMultiplier);
        else
            currentSpeed = baseSpeed;

        // --- ROTATION & FLIP ---
        if (moveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(sharkVisual.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            sharkVisual.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (moveDirection.x < 0)
            sharkVisual.localScale = new Vector3(1, -1, 1);
        else if (moveDirection.x > 0)
            sharkVisual.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        if (PlayerHealthReal.currentHealth <= 0)
            return;

        // --- MOVE WITH PHYSICS ---
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
    }
}

