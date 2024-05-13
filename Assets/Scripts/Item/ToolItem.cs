using UnityEngine;

public class ToolItem : Item {
    public override void SetThisItem(PlayerInteract playerInteract) {
        Transform itemHolder = playerInteract.GetItemHolder();
        var selectedPf = Instantiate(GetItemSO().pfItem);
        playerInteract.SetSelectedItem(selectedPf.GetComponent<Item>());
        selectedPf.transform.localPosition = Vector3.zero;
        selectedPf.SetParent(itemHolder, false);
    }

    public override void Use() {
        PlayerInteract.instance.GetPlayerAnimation().AnimateTool();
    }
}
