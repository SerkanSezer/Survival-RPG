using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    private Dictionary<ResourceSO,TextMeshProUGUI> resourcesUI = new Dictionary<ResourceSO, TextMeshProUGUI>();

    [SerializeField] private ResourceSO woodResource;
    [SerializeField] private ResourceSO rockResource;
    [SerializeField] private ResourceSO goldResource;
    [SerializeField] private TextMeshProUGUI woodResourceText;
    [SerializeField] private TextMeshProUGUI rockResourceText;
    [SerializeField] private TextMeshProUGUI goldResourceText;

    private void Awake() {
        InitResourceDict();
    }

    void Start()
    {
        ResourceManager.instance.OnChangedResources += ResourceManager_OnChangedResources;
    }

    private void ResourceManager_OnChangedResources(ResourceSO resourceSO, int amount) {
        resourcesUI[resourceSO].SetText(amount.ToString());
    }

    public void InitResourceDict() {
        resourcesUI.Add(woodResource, woodResourceText);
        resourcesUI.Add(rockResource, rockResourceText);
        resourcesUI.Add(goldResource, goldResourceText);
    }
    public void RefreshResourceUI() {
        foreach (var resource in ResourceManager.instance.GetResourceList()) {
            resourcesUI[resource.resourceSO].SetText(resource.amount.ToString());
        }
    }
}
