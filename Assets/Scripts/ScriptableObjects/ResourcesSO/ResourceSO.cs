using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSO", menuName = "ScriptableObjects/ResourceSO", order = 2)]
public class ResourceSO : ScriptableObject {
    public Sprite icon;
    public ParticleSystem resourceFX;
    public ItemSO requiredItemSO;
    public ResourceType resourceType; 
    public int resourceReserveAmount;
}
public enum ResourceType {
    Wood,
    Rock,
    Gold
}
