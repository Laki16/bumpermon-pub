using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Accessary Index
    //  0001 : 
    //private static int maxAccessorys = 100;

    [Header("System")]
    public List<GameObject> accessories = new List<GameObject>();
    //[HideInInspector]
    public Character currentCharacter;

    [Header("Equipment")]
    public List<Accessory> equipment = new List<Accessory>();
    private int maxEquipment = 3;

    [Header("UI")]
    public GameObject accessoryPanel;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject equipPanel;
    public GameObject statusPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(int itemNumber)
    {
        
    }

    public void ApplyAccessory()
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            //스탯 적용
        }
    }
}
