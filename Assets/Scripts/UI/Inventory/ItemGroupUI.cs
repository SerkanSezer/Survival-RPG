using TMPro;
using UnityEngine;

public class ItemGroupUI : MonoBehaviour
{
    public TextMeshProUGUI grpTitle;
    public Transform itemUI;
    public Transform parent;

    public void CreateItemUI(Item item, int amount) {
        var itemU = Instantiate(itemUI, parent).GetComponent<ItemUI>();
        itemU.SetItem(item, amount);
    }

}
