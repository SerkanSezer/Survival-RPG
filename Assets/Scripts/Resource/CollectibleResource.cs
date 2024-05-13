using UnityEngine;

public class CollectibleResource : MonoBehaviour, IInteractable
{
    [SerializeField] ResourceSO resourceSO;
    [SerializeField] private int amount;

    private void Awake() {
        CollectibleResourceSaveManager.AddCollectibleResource(gameObject);
    }

    private void Start() {
        CheckObjectIsInLoadList(CollectibleResourceSaveManager.Load());
    }

    private void CheckObjectIsInLoadList(CrSave crLoad) {
        if (gameObject != null) {
            if (!crLoad.hashCr.Contains(Animator.StringToHash(gameObject.name))) {
                Destroy(gameObject);
            }
        }
    }

    public void Interact() {
        ResourceManager.instance.IncreaseResource(resourceSO, amount);
        AudioManager.instance.PlayPickupSound();
        CollectibleResourceSaveManager.crList.hashCr.Remove(Animator.StringToHash(gameObject.name));
        Destroy(gameObject, 0.05f);
    }

}
