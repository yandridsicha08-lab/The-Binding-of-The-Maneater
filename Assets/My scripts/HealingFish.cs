using UnityEngine;

public class HealingFish : MonoBehaviour
{
    public GameObject bloodPrefab; // assign BloodParticles prefab in inspector
    public int healAmount = 20;
    public float moveSpeed = 2f;
    public float changeDirectionTime = 1.5f;

    private Vector2 direction;
    private float timer;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
            PickNewDirection();
    }

    void PickNewDirection()
    {
        direction = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.currentHealth += healAmount;
                ph.currentHealth = Mathf.Clamp(ph.currentHealth, 0, ph.maxHealth);
                HealthUI.instance.UpdateHealth(ph.currentHealth, ph.maxHealth);
            }

            // --- Play crunchy sound on the player ---
            AudioSource playerAudio = collision.GetComponent<AudioSource>();
            if (playerAudio != null)
            {
                AudioClip crunchSound = playerAudio.clip; // assign your crunch clip in player inspector
                playerAudio.PlayOneShot(crunchSound);
            }
            
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // fish disappears
        }
    }
}