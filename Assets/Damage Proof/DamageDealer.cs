using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 25;  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            Debug.Log("Player took damage!");
        }
    }
}
