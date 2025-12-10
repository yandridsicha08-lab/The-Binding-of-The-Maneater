using UnityEngine;

public class EnemyFollowWithDetection : MonoBehaviour
{
    public float moveSpeed = 3f;          // Movement speed
    public int damage = 10;               // Contact damage
    public float attackCooldown = 1f;     // How often it can attack
    public float detectionRadius = 5f;    // How far the shark can “see” the player
    public Transform sharkVisual;         // Sprite object (child)
    
    private Transform player;
    private float attackTimer = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Vector to player
        Vector2 direction = (player.position - transform.position);

        // Check detection radius
        if (direction.magnitude <= detectionRadius)
        {
            // Move toward player
            Vector2 moveDir = direction.normalized;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            // Rotate shark nose toward player (default sprite points LEFT)
            if (sharkVisual != null)
            {
                float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg + 180f;
                sharkVisual.rotation = Quaternion.Euler(0, 0, angle);

                // Optional flip
                sharkVisual.localScale = moveDir.x < 0 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
            }
        }

        // Countdown attack timer
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && attackTimer <= 0f)
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                attackTimer = attackCooldown;
            }
        }
    }

    // Optional: visualize detection radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
