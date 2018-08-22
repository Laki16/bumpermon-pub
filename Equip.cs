using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour {

    [Header("System")]
    public int equippedCharacter;
    public EquipDisplay equipDisplay;

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

    public void BtnOnEquip()
    {
        //아이템 누르면 UI업데이트 되고 장착, 업그레이드, 판매버튼 활성화된다.
        equipDisplay.UpdateUI(EquipIndex);
    }

    public void BtnOnUpgrade()
    {

    }

    public void BtnOnSell()
    {

    }

}
