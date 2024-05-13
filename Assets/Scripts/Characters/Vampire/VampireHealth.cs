using UnityEngine;

public class VampireHealth : MonoBehaviour
{
    private VampireAnimation vampireAnimation;
    private VampireAttack vampireAttack;
    private bool vampireAlive = true;
    private int vampireHealth = 100;
    private Vampire vampire;

    private void Awake() {
        vampire = GetComponent<Vampire>();
        vampireAttack = GetComponent<VampireAttack>();
        vampireAnimation = GetComponent<VampireAnimation>();
    }

    public void DecreaseHealth(int amount) {
        vampireHealth -= amount;
        if (vampireHealth <= 0 && vampireAlive) {
            vampireAlive = false;
            vampireAnimation.AnimateDeath();
            vampireAttack.DeactivateAttack();
            vampire.Dead();
            Destroy(gameObject,5);
        }
        else { vampireAnimation.AnimateHit(); }
    }
    public bool IsVampireAlive() {
        return vampireAlive;
    }
}
