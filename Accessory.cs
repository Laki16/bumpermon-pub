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

    [Header("Accessory Effect")]
    public int HP;
    public int SPD;
    public int DEF;
    public int STR;
    public int LUK;

    public float nitroRange;
    public float nitroTime;

}
