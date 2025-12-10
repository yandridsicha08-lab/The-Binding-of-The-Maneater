using UnityEngine;

public class NoseDamage : MonoBehaviour
{
    public int damage = 999; // kills enemy instantly

    public GameObject bloodPrefab; // assign BloodParticles prefab in inspector
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only target enemies
        if (collision.CompareTag("Enemy"))
        {
            // Play crunch sound on player
            AudioSource playerAudio = transform.parent.GetComponent<AudioSource>();
            if (playerAudio != null && playerAudio.clip != null)
            {
                playerAudio.PlayOneShot(playerAudio.clip);
            }
            
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the enemy
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("Boss"))
        {
            BossDeath boss = collision.GetComponent<BossDeath>();
            if (boss != null)
            {
                boss.Die();
            }

            // Optional: play crunch sound on player too
            AudioSource playerAudio = transform.parent.GetComponent<AudioSource>();
            if (playerAudio != null && playerAudio.clip != null)
                playerAudio.PlayOneShot(playerAudio.clip);
        }
    }
    
    
}
