using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinItem : Item {
    public override void SetThisItem(PlayerInteract playerInteract) {
        playerInteract.GetPlayerVisual().SetSkinItemWorld(this);
    }

    public override void Use() {
        //Debug.Log("Skin Item Used");
    }
}
