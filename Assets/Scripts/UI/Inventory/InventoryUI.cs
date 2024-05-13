using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance { get; private set; }
    private ItemGroupUI grpTool;
    private ItemGroupUI grpWeapon;
    private ItemGroupUI grpMedical;
    private ItemGroupUI grpSkin;
    [SerializeField] private ItemGroupUI itemGroupUI;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI() {

        DeleteInventoryUI();

        foreach (var (item, amount) in PlayerInteract.instance.playerInventory.GetItemList()) {
            if (item.TryGetComponent<ToolItem>(out ToolItem toolItem)) {
                if (!grpTool) {
                    grpTool = Instantiate(itemGroupUI, transform);
                    grpTool.grpTitle.SetText("Tools");
                }
                grpTool.CreateItemUI(item, amount);
            }
            else if (item.TryGetComponent<WeaponItem>(out WeaponItem weaponItem)) {
                if (!grpWeapon) {
                    grpWeapon = Instantiate(itemGroupUI, transform);
                    grpWeapon.grpTitle.SetText("Weapons");
                }
                grpWeapon.CreateItemUI(item, amount);
            }
            else if (item.TryGetComponent<MedicalItem>(out MedicalItem medicalItem)) {
                if (!grpMedical) {
                    grpMedical = Instantiate(itemGroupUI, transform);
                    grpMedical.grpTitle.SetText("Potions");
                }
                grpMedical.CreateItemUI(item, amount);
            }
            else if (item.TryGetComponent<SkinItem>(out SkinItem skinItem)) {
                if (!grpSkin) {
                    grpSkin = Instantiate(itemGroupUI, transform);
                    grpSkin.grpTitle.SetText("Skins");
                }
                grpSkin.CreateItemUI(item, amount);
            }
        }
    }

    private void DeleteInventoryUI() {
        if (transform.childCount > 0) {
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        grpTool = null;
        grpWeapon = null;
        grpMedical = null;
        grpSkin = null;
    }

}
