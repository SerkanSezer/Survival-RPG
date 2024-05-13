using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isPlayerAlive = true;
    private int health = 100;
    private PlayerAnimation playerAnimation;
    private GameOver gameOver;
    [SerializeField] Transform healVFX;
    public event Action<int> OnHealthChanged;
    private void Awake() {
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    private void Start() {
        gameOver = FindObjectOfType<GameOver>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<VampireWeapon>(out VampireWeapon vampireWeapon) && isPlayerAlive) {
            if (vampireWeapon.IsAttackActive()) {
                health -= vampireWeapon.attackDamageAmount;
                OnHealthChanged?.Invoke(health);
                if (health <= 0) {
                    isPlayerAlive = false;
                    playerAnimation.AnimateDeath();
                    AudioManager.instance.PlayGameOverSound();
                    gameOver.OpenGameOverPanel();
                }
                else { playerAnimation.AnimateHit(); }
            }
        }
    }

    public void IncreaseHealth(int health) { 
        this.health += health;
        this.health = Mathf.Clamp(this.health,0, 100);
        OnHealthChanged?.Invoke(this.health);
        PlayVFX();
    }

    public void PlayVFX() {
        Instantiate(healVFX, transform.position, transform.rotation);
    }

    
}
