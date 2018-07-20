using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : ShopManager {

    [Header("Accessory Info")]
    public int itemNumber;
    protected bool isUnlock = false;
    public int enhance;
    protected int maxEnhance = 9;

    [Header("State Info")]
    public bool isEquip;

}
