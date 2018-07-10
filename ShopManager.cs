using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    //Accessary Index
    //  0001 : 
    //private static int maxAccessorys = 100;
    public List<Accessory> accessories = new List<Accessory>();
    
    [Header("Accessory Effect")]
    public int HP;
    public int SPD;
    public int DEF;
    public int STR;
    public int LUK;
    [Space(10)]
    public float nitroRange;
    public float nitroTime;

    [Header("Equipment")]
    private int maxEquipment = 3;
    public List<Accessory> equipment = new List<Accessory>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyAccessory()
    {
        for(int i = 0; i< equipment.Count; i++)
        {
            //스탯 적용
        }
    }
}
