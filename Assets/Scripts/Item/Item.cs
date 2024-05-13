using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;

    public ItemSO GetItemSO() {
        return itemSO;
    }
    public abstract void SetThisItem(PlayerInteract playerInteract);

    public abstract void Use();

}
