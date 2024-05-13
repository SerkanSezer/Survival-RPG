using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemAmount;

    public Item GetItem() {
        return item;
    }

    public void OnPointerClick(PointerEventData eventData) {
        item.SetThisItem(PlayerInteract.instance);
        AudioManager.instance.PlayEquipSound();
        PlayerInteract.instance._input.DeActivateUISelect();
    }

    public void SetItem(Item item, int amount) {
        this.item = item;
        SetItemProps(item, amount);
    }

    public void SetItemProps(Item item, int amount) {
        itemIcon.sprite = item.GetItemSO().sprite;
        if (amount > 1) {
            itemAmount.SetText(amount.ToString());
        }
    }
}
