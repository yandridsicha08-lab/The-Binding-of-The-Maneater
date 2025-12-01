using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 1;  // how much damage it deals

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            Debug.Log("Player took damage!");
        }
    }
}
