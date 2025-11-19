using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 10f;
    
    public float speed = 10f;

    public Transform sharkVisual; // assign your shark sprite object here
    
    public float rotationSpeed = 5f;
    
    public float sprintMultiplier = 0.2f;

    public float maxSpeed = 50f;
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection.y = 1;

        if (Input.GetKey(KeyCode.S))
            moveDirection.y = -1;

        if (Input.GetKey(KeyCode.A))
            moveDirection.x = -1;

        if (Input.GetKey(KeyCode.D))
            moveDirection.x = 1;
       
        if (Input.GetKey(KeyCode.LeftShift) && speed < maxSpeed)
        {
               speed *= sprintMultiplier;
        }
        else
        {
            // NEW -> Reset speed when not sprinting
            speed = baseSpeed;
        }
        
        // Normalize so diagonal isn't faster
        moveDirection = moveDirection.normalized;

        // Move the parent (world space, unaffected by rotation)
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Rotate ONLY the visual shark
        if (moveDirection != Vector3.zero)
        {
            // Calculate the target angle
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            // Smoothly rotate toward the target angle
            float angle = Mathf.LerpAngle(sharkVisual.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

            sharkVisual.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (moveDirection.x < 0)
        {
            sharkVisual.localScale = new Vector3(1, -1, 1);   // flipped vertically
        }
        else if (moveDirection.x > 0)
        {
            sharkVisual.localScale = new Vector3(1, 1, 1);    // normal
        }
    }
}

