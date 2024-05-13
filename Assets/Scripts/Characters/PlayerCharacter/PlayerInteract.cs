using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerVisual playerVisual;
    private PlayerHealth playerHealth;
    private Item selectedItem;
    private const float INTERACT_DISTANCE = 3f;

    public static PlayerInteract instance;
    public StarterAssetsInputs _input;
    [SerializeField] private Transform itemHolder;
    [SerializeField]  LayerMask layerMask;

    //Initializing Inventory
    [SerializeField] private List<ItemHash> allItems = new List<ItemHash>();
    public List<Item> tempItemList;
    public PlayerInventory playerInventory; 
    


    private void Awake() {
        instance = this;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerVisual = GetComponentInChildren<PlayerVisual>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInventory = new PlayerInventory(tempItemList);
        playerInventory.CreateItemDictionary(allItems);
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        Vector3 point2 = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, point2, 0.2f, transform.forward, INTERACT_DISTANCE, layerMask);

        foreach (RaycastHit hit in hits) {
            if (hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                if (_input.interact) {
                    interactable.Interact();
                }
            }
        }

        if (_input.interact_alt) {
            selectedItem.Use();
        }
    }

    public void SetSelectedItem(Item item) {
        DeletePreviousSelectedPf();
        selectedItem = item;
    }

    private void DeletePreviousSelectedPf() {
        if(itemHolder.childCount == 1) {
            Destroy(itemHolder.GetChild(0).gameObject);
        }
    }
    public PlayerVisual GetPlayerVisual() {
        return playerVisual;
    }
    public PlayerAnimation GetPlayerAnimation() {
        return playerAnimation;
    }
    public Transform GetItemHolder() {
        return itemHolder;
    }

    public void IncreaseHealth(int healthAmount) {
        playerHealth.IncreaseHealth(healthAmount);
    }
    public PlayerHealth GetPlayerHealth() {
        return playerHealth;
    }

    //Selected Item (Tool-Weapon) animation events
    public void ActivateItem() {
        selectedItem.GetComponent<BoxCollider>().isTrigger = true;
    }
    public void DeactivateItem() {
        selectedItem.GetComponent<BoxCollider>().isTrigger = false;
    }
}
