using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Image healthBar;
    void Start()
    {
        healthBar = transform.Find("HealthUI").GetComponent<Image>();
        playerHealth = PlayerInteract.instance.GetPlayerHealth();
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(int health) {
         healthBar.fillAmount = (float)health/100;
    }

}
