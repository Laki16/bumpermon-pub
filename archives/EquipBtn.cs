using UnityEngine;

public class EquipBtn : MonoBehaviour {

    private ShopManager shopManager;

    public void BtnOnEquip()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        shopManager.BtnOnEquip();
    }

    public void BtnOnUnequip()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        shopManager.BtnOnUnequip();
    }
}
