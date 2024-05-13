using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantTrade : MonoBehaviour
{
    [SerializeField] private Transform pfProduct;
    [SerializeField] private Transform productListPanel;
    [SerializeField] private Button closeProductList;
    [SerializeField] List<ItemSO> productList = new List<ItemSO>();
    
    public void ShowProductList() {

        DeleteProductList();

        productListPanel.gameObject.SetActive(true);
        closeProductList.gameObject.SetActive(true);

        closeProductList.onClick.AddListener(() => { GetComponent<Merchant>().CloseInteract(); });

        foreach (var item in productList) {
            var product = Instantiate(pfProduct, productListPanel);
            ProductUI productUI = product.GetComponent<ProductUI>();
            productUI.productIcon.sprite = item.sprite;
            productUI.productBuyButton.onClick.AddListener(()=> { BuyProduct(item); });
            foreach (var requireResource in item.GetRequiredResources()) {
                var rR = Instantiate(productUI.requireResource, productUI.pricesUI).GetComponent<RequireResourceUI>();
                rR.resourceIcon.sprite = requireResource.resourceSO.icon;
                rR.amountText.SetText(requireResource.amount.ToString());
            }
        }
    }
    public void HideProductList() {
        productListPanel.gameObject.SetActive(false);
        closeProductList.onClick.RemoveAllListeners();
        closeProductList.gameObject.SetActive(false);
    }

    private void DeleteProductList() {
        if (productListPanel.childCount > 0) {
            int childCount = productListPanel.childCount;
            for (int i = 0; i < childCount; i++) {
                Destroy(productListPanel.GetChild(i).gameObject);
            }
        }
    }

    private void BuyProduct(ItemSO itemSO) {
        if (!PlayerInteract.instance.playerInventory.IsNonConsumableItemInInventory(itemSO.pfItem.GetComponent<Item>())) {
            bool enoughResources = true;
            foreach (var requireResource in itemSO.GetRequiredResources()) {
                foreach (var resource in ResourceManager.instance.GetResourceList()) {
                    if (requireResource.resourceSO == resource.resourceSO) {
                        if (requireResource.amount > resource.amount) {
                            enoughResources = false;
                            break;
                        }
                    }
                }
            }
            if (enoughResources) {
                foreach (var requireResource in itemSO.GetRequiredResources()) {
                    ResourceManager.instance.DecreaseResource(requireResource.resourceSO, requireResource.amount);
                }
                PlayerInteract.instance.playerInventory.AddItem(itemSO.pfItem.GetComponent<Item>());
                FindAnyObjectByType<InventoryUI>().RefreshInventoryUI();
            }
            else {
                //Debug.Log("No enough Resources");
            }
        }
        else {
            //Debug.Log("This item is nonconsumable and is in inventory already!");
        }

    }
}
