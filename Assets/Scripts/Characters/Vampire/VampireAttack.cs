using System.Collections.Generic;
using UnityEngine;

public class VampireAttack : MonoBehaviour
{
    private VampireHealth vampireHealth;
    [SerializeField] VampireWeapon vampireWeapon;
    [SerializeField] List<Transform> hitVFXs;

    private void Awake() {
        vampireHealth = GetComponent<VampireHealth>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<WeaponItem>(out WeaponItem weaponItem) && vampireHealth.IsVampireAlive()) {
            Instantiate(hitVFXs[Random.Range(0, hitVFXs.Count)], other.ClosestPoint(transform.position),Quaternion.identity);
            vampireHealth.DecreaseHealth(weaponItem.GetDamageAmount());
            AudioManager.instance.PlaySound(weaponItem.GetItemSO().itemSound);
        }
    }

    public void ActivateAttack() {
        vampireWeapon.ActivateAttack();
    }

    public void DeactivateAttack() {
        vampireWeapon.DeactivateAttack();
    }

}
