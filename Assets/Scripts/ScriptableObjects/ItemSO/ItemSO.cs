using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject {

    public Transform pfItem;
    public Sprite sprite;
    public string nameString;
    public ItemType itemtype;
    public List<AudioClip> itemSound;
    public int itemEffectAmount;
    [SerializeField] private List<RequiredResources> requiredResources;

    public List<RequiredResources> GetRequiredResources() {
        return requiredResources;
    }
}

[Serializable]
public class RequiredResources {
    public ResourceSO resourceSO;
    public int amount;
}
public enum ItemType {
    NonConsumable,
    Consumable
}



