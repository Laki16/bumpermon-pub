using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip : MonoBehaviour {

    [Header("System")]
    public int equippedCharacter;
    //public int index;
    private ShopManager shopManager;

    [Header("Stats")]
    public int EquipIndex;
    public int Level;
    public float HP;
    public float SPD;
    public float DEF;
    public float STR;
    public float LUK;
    public float nitroSize;
    public float bombSize;
    public float nitroTime;
    public bool isEquipped = false;

    [Header("Button")]
    public GameObject infoBtn;
    public GameObject equipBtn;


    private void Start()
    {
        shopManager = GameObject.Find("Equip(Shop)Manager").GetComponent<ShopManager>();    
    }

    public void UpdateFrame(int index)
    {
        transform.Find("Frame").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        equippedCharacter = index;
    }
    
    public void ResetFrame()
    {
        transform.Find("Frame").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void BtnOnEquippedItem()
    {
        shopManager.currentEquip = this;
        if(shopManager.beforeInfoBtn != null) Destroy(shopManager.beforeInfoBtn);
        if (shopManager.beforeEquipBtn != null) Destroy(shopManager.beforeEquipBtn);

        var newInfoBtn = Instantiate(infoBtn);
        newInfoBtn.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
        shopManager.beforeInfoBtn = newInfoBtn;
    }

    public void BtnOnItem()
    {
        shopManager.currentEquip = this;
        if (shopManager.beforeInfoBtn != null) Destroy(shopManager.beforeInfoBtn);
        if (shopManager.beforeEquipBtn != null) Destroy(shopManager.beforeEquipBtn);

        var newInfoBtn = Instantiate(infoBtn);
        newInfoBtn.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
        shopManager.beforeInfoBtn = newInfoBtn;

        if (!isEquipped)
        {
            var newEquipItem = Instantiate(equipBtn);
            newEquipItem.transform.SetParent(EventSystem.current.currentSelectedGameObject.transform, false);
            shopManager.beforeEquipBtn = newEquipItem;
        }
    }

}
