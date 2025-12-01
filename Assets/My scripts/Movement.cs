using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 10f;

    public PlayerHealth PlayerHealthReal;
    
    public float speed = 10f;

    public Transform sharkVisual; 
    
    public float rotationSpeed = 5f;
    
    public float sprintMultiplier = 0.2f;

    public float maxSpeed = 50f;
    void Update()
    {
        if (PlayerHealthReal.currentHealth <= 0)
        {
            return;
        }

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
            
            speed = baseSpeed;
        }
        
        
        moveDirection = moveDirection.normalized;

        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        
        if (moveDirection != Vector3.zero)
        {
            
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            
            float angle = Mathf.LerpAngle(sharkVisual.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

            sharkVisual.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (moveDirection.x < 0)
        {
            sharkVisual.localScale = new Vector3(1, -1, 1);   
        }
        else if (moveDirection.x > 0)
        {
            sharkVisual.localScale = new Vector3(1, 1, 1);    
        }
    }
}

