using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalItem : Item, IInteractable {
    public void Interact() {
        PlayerInteract.instance.playerInventory.AddItem(GetItemSO().pfItem.GetComponent<Item>());
        Destroy(gameObject,0.1f);
    }

    public override void SetThisItem(PlayerInteract playerInteract) {
        AudioManager.instance.PlaySound(GetItemSO().itemSound);
        PlayerInteract.instance.IncreaseHealth(GetItemSO().itemEffectAmount);
        PlayerInteract.instance.GetPlayerAnimation().AnimateHeal();
        PlayerInteract.instance.playerInventory.DecreaseComsumableItemAmount(this);
    }

    public override void Use() {
        //Debug.Log("Medical Item Used");
    }
}
