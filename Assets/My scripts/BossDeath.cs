using UnityEngine;
using UnityEngine.UI; // if you want to use UI text

public class BossDeath : MonoBehaviour
{
    public GameObject bloodPrefab;      // same as normal enemies
    public AudioClip crunchSound;       // optional: same as fish/enemy
    public GameObject winTextUI;        // UI Text or Panel with "Yay! You won!"
    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Call this when the boss dies
    public void Die()
    {
        // Spawn blood particles
        if (bloodPrefab != null)
            Instantiate(bloodPrefab, transform.position, Quaternion.identity);

        // Play crunch sound
        if (audioSource != null && crunchSound != null)
            audioSource.PlayOneShot(crunchSound);

        // Show win message
        if (winTextUI != null)
            winTextUI.SetActive(true);

        // Stop the game
        Time.timeScale = 0f;

        // Destroy boss
        Destroy(gameObject);
    }
}
