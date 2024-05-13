using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private int hashRe;
    private int currentAmount;
    [SerializeField] private int EACH_AMOUNT;
    [SerializeField] private ResourceSO resourceSO;

    private void Awake() {
        currentAmount = resourceSO.resourceReserveAmount;
        hashRe = Animator.StringToHash(gameObject.name);
        ResourceSaveManager.AddResource(gameObject);
    }
    private void Start() {
        CheckObjectIsInLoadList(ResourceSaveManager.Load());
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Item>(out Item item)) {
            if (item.GetItemSO() == resourceSO.requiredItemSO) {
                Vector3 closestPoint = other.ClosestPoint(transform.position);
                PlayResourceFX(closestPoint);
                AddResource(resourceSO, EACH_AMOUNT);
                AudioManager.instance.PlaySound(item.GetItemSO().itemSound);
                currentAmount -= EACH_AMOUNT;
                ResourceSaveManager.DecreaseResource(hashRe, currentAmount);
                if (currentAmount <= 0) {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void PlayResourceFX(Vector3 hitPoint) {
        Vector3 dir = (PlayerInteract.instance.transform.position - transform.position).normalized;
        Instantiate(resourceSO.resourceFX, hitPoint, Quaternion.LookRotation(dir));
    }
    private void AddResource(ResourceSO resourceSO,int amount) {
        ResourceManager.instance.IncreaseResource(resourceSO, amount);
    }

    public int GetInitResourceAmount() {
        return resourceSO.resourceReserveAmount;
    }
    private void CheckObjectIsInLoadList(List<RSave> rSaves) {
        if (gameObject != null) {
            bool isInRLoad = false;
            foreach (RSave rSave in rSaves) {
                if (rSave.hashResource == hashRe) {
                    isInRLoad = true;
                    currentAmount = rSave.amountResource;
                    break;
                }
            }
            if (!isInRLoad) {
                Destroy(gameObject);
            }
        }
    }
}
