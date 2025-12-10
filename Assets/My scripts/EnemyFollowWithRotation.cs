using UnityEngine;

public class EnemyFollowWithRotation : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public Transform sharkVisual;  // assign the sprite here

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

        // --- Movement ---
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // --- Rotation ---
        if (sharkVisual != null)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Since the sprite points LEFT by default, add 180° to rotate the nose toward player
            sharkVisual.rotation = Quaternion.Euler(0, 0, angle + 180f);

            // Optional: vertical flip for aesthetic consistency
            if (direction.x < 0)
                sharkVisual.localScale = new Vector3(1, -1, 1);
            else
                sharkVisual.localScale = new Vector3(1, 1, 1);
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
}
