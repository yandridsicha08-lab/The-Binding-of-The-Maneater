using UnityEngine;
using TMPro;
public class DeathUI : MonoBehaviour
{
    public static DeathUI instance;
    public TextMeshPro deathText;
    public PlayerHealth PlayerHealth;

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.currentHealth <= 0) ;
        {
            
        }
    }
}
