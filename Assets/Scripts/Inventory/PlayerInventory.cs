using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory {

    public Dictionary<int, Item> allItemDict = new Dictionary<int, Item>();
    public Dictionary<Item, int> itemList = new Dictionary<Item, int>();
    
    public PlayerInventory(List<Item> itemLst) {
        foreach (var item in itemLst) {
            itemList[item] = 1;
        }
    }

    private int HashItem(Item item) => Animator.StringToHash(item.name);
    public void CreateItemDictionary(List<ItemHash> allItems) {
        foreach (ItemHash itemHash in allItems) {
            int hashKey = HashItem(itemHash.item);
            itemHash.hash = hashKey;
            allItemDict[hashKey] = itemHash.item;
        }
    }

    public void SetItemList (List<PlayerInventorySave> saveInventoryList) {
        itemList = new Dictionary<Item, int>();
        foreach (var saveItem in saveInventoryList) {
            itemList[allItemDict[saveItem.hashItem]] = saveItem.amount;
        }
        InventoryUI.instance.RefreshInventoryUI();
    }
    public Dictionary<Item,int> GetItemList() {
        return itemList;
    }

    public void AddItem(Item item) {
        if (itemList.ContainsKey(item)) {
            if (item.GetItemSO().itemtype == ItemType.Consumable) {
                itemList[item]++;
            }
        }
        else {
            itemList.Add(item, 1);
        }
        InventoryUI.instance.RefreshInventoryUI();
    }

    public void RemoveItem(Item item) {
        itemList.Remove(item);
    }
    public void DecreaseComsumableItemAmount(Item item) {
        if (item.GetItemSO().itemtype == ItemType.Consumable) {
            itemList[item]--;
        }
        if (itemList[item] == 0) {
            RemoveItem(item);
        }
        InventoryUI.instance.RefreshInventoryUI();
    }

    public bool IsNonConsumableItemInInventory(Item item) {
        if (itemList.ContainsKey(item) && item.GetItemSO().itemtype == ItemType.NonConsumable) {
            return true;
        }
        return false;
    }

}
[Serializable]
public class ItemHash {
    public int hash;
    public Item item;
}
