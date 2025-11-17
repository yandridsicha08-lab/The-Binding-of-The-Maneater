using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed

    void Update()
    {
        // Get the player's current position
        Vector3 moveDirection = Vector3.zero;

        // Check for W key (move up)
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y = speed;
        }
        // Check for S key (move down)
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y = -speed;
        }
        // Check for A key (move left)
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -speed;
        }
        // Check for D key (move right)
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = speed;
        }

        // Apply movement based on input
        transform.Translate(moveDirection * Time.deltaTime);

        // If there's movement (i.e., the player pressed a key), rotate the shark
        if (moveDirection != Vector3.zero)
        {
            // Calculate the angle of movement using Atan2
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            // Apply the rotation to make the tip of the shark point in the movement direction
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}
