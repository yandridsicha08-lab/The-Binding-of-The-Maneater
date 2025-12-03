using UnityEngine;
using TMPro;
public class DeathUI : MonoBehaviour
{
    public static DeathUI instance;
    public TextMeshProUGUI deathText;

    // Update is called once per frame
    void Start()
    {
        instance = this;

    }

    public void UpdateDeath()
    {
        deathText.text = "You died! Skill Issue!";
    }
}
