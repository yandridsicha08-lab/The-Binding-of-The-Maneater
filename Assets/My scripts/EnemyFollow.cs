using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;       // Enemy speed
    public int damage = 10;            // Damage to player on contact
    public float attackCooldown = 1f;  // How often it can damage the player

    private Transform player;
    private float attackTimer = 0f;

    void Start()
    {
        // Find the player in the scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Move towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        // Countdown attack timer
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Only attack player
        if (collision.CompareTag("Player") && attackTimer <= 0f)
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                attackTimer = attackCooldown; // reset cooldown
            }
        }
    }
}
