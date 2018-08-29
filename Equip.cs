using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour {

    [Header("System")]
    public int equippedCharacter;
    //public int index;
    private GameObject shopManager;

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

    private void Start()
    {
        shopManager = GameObject.Find("Equip(Shop)Manager");    
    }

    public void BtnOnItem()
    {
        //아이템 누르면 UI업데이트 되고 장착, 업그레이드, 판매버튼 활성화된다. 캐릭터에 맞춰서..
        shopManager.GetComponent<EquipDisplay>().UpdateUI(EquipIndex);
        //Debug.Log(EquipIndex);
        shopManager.GetComponent<ShopManager>().currentEquip = transform.GetComponent<Equip>();
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

}
