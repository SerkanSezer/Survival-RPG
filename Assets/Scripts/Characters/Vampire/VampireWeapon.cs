using UnityEngine;

public class VampireWeapon : MonoBehaviour
{
    private bool isAttackActive = false;
    public int attackDamageAmount = 5;

    //Attack animation events
    public void ActivateAttack() {
        isAttackActive = true;
    }
    public void DeactivateAttack() {
        isAttackActive = false;
    }

    public bool IsAttackActive() {
        return isAttackActive;
    }
}
