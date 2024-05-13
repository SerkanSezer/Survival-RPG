using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    private ResourcesUI resourcesUI;
    [SerializeField] private List<PlayerResource> playerResourceList;
    public event Action<ResourceSO,int> OnChangedResources;
    private void Awake() {
        instance = this;
        resourcesUI = FindObjectOfType<ResourcesUI>();
    }

    public List<PlayerResource> GetResourceList() {
        return playerResourceList;
    }

    public void IncreaseResource(ResourceSO resourceSO, int amount) {
        foreach (var playerResource in playerResourceList) {
            if (playerResource.resourceSO == resourceSO) {
                playerResource.amount += amount;
                OnChangedResources?.Invoke(resourceSO, playerResource.amount);
            }
        }
    }

    public void DecreaseResource(ResourceSO resourceSO, int amount) {
        foreach (var playerResource in playerResourceList) {
            if (playerResource.resourceSO == resourceSO) {
                playerResource.amount -= amount;
                OnChangedResources?.Invoke(resourceSO, playerResource.amount);
            }
        }
    }

    public void RefreshResource() {
        resourcesUI.RefreshResourceUI();
    }
}

[Serializable]
public class PlayerResource {
    public ResourceSO resourceSO;
    public int amount;
}
