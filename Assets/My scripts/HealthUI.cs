using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;

    public TextMeshProUGUI healthText;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int current, int max)
    {
        healthText.text = current + " / " + max;
    }
}
