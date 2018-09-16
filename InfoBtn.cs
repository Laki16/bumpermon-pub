using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBtn : MonoBehaviour {

    private ShopManager shopManager;

    public void BtnOnInfo()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        shopManager.BtnOnInfo();
    }
}
