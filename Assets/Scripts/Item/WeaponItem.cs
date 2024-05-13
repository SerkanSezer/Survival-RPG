using UnityEngine;

public class WeaponItem : Item {

    private int damageAmount;
    public override void SetThisItem(PlayerInteract playerInteract) {
        Transform itemHolder = playerInteract.GetItemHolder();
        var selectedPf = Instantiate(GetItemSO().pfItem);
        playerInteract.SetSelectedItem(selectedPf.GetComponent<Item>());
        selectedPf.GetComponent<WeaponItem>().SetDamageAmount(GetItemSO().itemEffectAmount);
        selectedPf.transform.localPosition = Vector3.zero;
        selectedPf.SetParent(itemHolder, false);
    }

    public override void Use() {
        PlayerInteract.instance.GetPlayerAnimation().AnimateAttack();
    }

    public int GetDamageAmount() {
        return damageAmount;
    }

    public void SetDamageAmount(int amount) {
        damageAmount = amount;  
    }
}
