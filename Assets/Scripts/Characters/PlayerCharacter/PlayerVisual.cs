using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private List<SkinItemWorld> Skins = new List<SkinItemWorld>();
    [SerializeField] private List<Item> initItems = new List<Item>();

    private void Awake() {
        foreach (var item in initItems) {
            PlayerVisualSaveManager.AddItem(item.GetItemSO());
        }
    }
    private void Start() {
        SetVisualFromLoad(PlayerVisualSaveManager.Load());
        
    }
    public void SetSkinItemWorld(Item item) {
        SkinPart skinpart = SkinPart.None;
        foreach (var skin in Skins) {
            if (item.GetItemSO() == skin.itemSO) {
                skinpart = skin.skinPart;
                break;
            }
        }
        foreach (var skin in Skins) {
            if (skin.skinPart == skinpart && skin.itemSO != item.GetItemSO()) {
                PlayerVisualSaveManager.RemoveItem(skin.itemSO);
                skin.skinnedMeshRenderer.enabled = false;
            }else if(skin.itemSO == item.GetItemSO()) {
                PlayerVisualSaveManager.AddItem(skin.itemSO);
                skin.skinnedMeshRenderer.enabled = true;
            }
        }
    }

    public void SetVisualFromLoad(List<ItemSO> itemSOList) {
        foreach (var skin in Skins) {
            skin.skinnedMeshRenderer.enabled = false;
        }
        foreach (var itemSO in itemSOList) {
            foreach (var skin in Skins) {
                if (skin.itemSO == itemSO) {
                    skin.skinnedMeshRenderer.enabled = true;
                }
            }
        }
    }
}

[Serializable]
public class SkinItemWorld {
    public SkinPart skinPart;
    public ItemSO itemSO;
    public SkinnedMeshRenderer skinnedMeshRenderer;
}
public enum SkinPart {
    Body,
    Legs,
    Boots,
    Gauntlets,
    Cape,
    Helmet,
    None
}
