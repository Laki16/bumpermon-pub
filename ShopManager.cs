using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Accessary Index
    //  0001 : 
    //private static int maxAccessorys = 100;

    public List<GameObject> accessories = new List<GameObject>();

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
    public List<Accessory> equipment = new List<Accessory>();
    private int maxEquipment = 3;

    [Header("UI")]
    public List<GameObject> accessoryContainer = new List<GameObject>();
    public Transform accessoryParent;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject newAccessoryTemplete;
    public Transform newAccessoryTransform;

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
        Transform newSlot = Instantiate(newAccessoryTransform);
        newSlot.name = "Slot_" + (accessoryContainer.Count());
        newSlot.parent = accessoryParent;
        RectTransform slotRect = newSlot.GetComponent<RectTransform>();
        slotRect.anchorMin = new Vector2(0, 1);
        slotRect.anchorMax = new Vector2(0, 1);
        slotRect.pivot = new Vector2(0, 1);
        slotRect.offsetMin = Vector2.zero;
        slotRect.offsetMax = Vector2.zero;
        slotRect.position = new Vector3(accessoryContainer.Count() % 4 * 200f, accessoryContainer.Count() / 4 * 150f, 0);

        GameObject newItem = Instantiate(accessories[itemNumber]);
        newItem.transform.parent = newSlot;
        accessoryContainer.Add(newItem);
    }

    public void ApplyAccessory()
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            //스탯 적용
        }
    }
}
