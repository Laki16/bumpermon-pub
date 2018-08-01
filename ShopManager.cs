using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Accessary Index
    //  0001 : 
    //private static int maxAccessorys = 100;

    [Header("System")]
    public List<GameObject> accessories = new List<GameObject>();
    //[HideInInspector]
    public Character currentCharacter;
    private bool isStatus;

    [Header("Equipment")]
    public List<Accessory> equipment = new List<Accessory>();
    private int maxEquipment = 3;

    [Header("UI")]
    public GameObject accessoryPanel;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    [Space(10)]
    public GameObject swapPanel;
    public GameObject characterImage;
    public Sprite golemCircleImage;
    public Sprite ghostCircleImage;
    public Sprite santaCircleImage;
    public Sprite skeletonCircleImage;
    [Space(10)]
    public Text transText;
    public Text introText;
    public Text hpDefault;
    public Text hpUpgraded;
    public Text spdDefault;
    public Text spdUpgraded;
    public Text defDefault;
    public Text defUpgraded;
    public Text strDefault;
    public Text strUpgraded;
    public Text lukDefault;
    public Text lukUpgraded;
    public Text nSizeDefault;
    public Text nSizeUpgraded;
    public Text bSizeDefault;
    public Text bSizeUpgraded;
    public Text nLengthDefault;
    public Text nLengthUpgraded;

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

    public void BtnOnTrans()
    {
        //스탯창 -> 장비창
        if (isStatus)
        {
            swapPanel.GetComponent<Animator>().Play("Equip");
            isStatus = false;
            transText.text = "Status";
        }
        else //장비창 -> 스탯창
        {
            swapPanel.GetComponent<Animator>().Play("Status");
            isStatus = true;
            transText.text = "Equip";
        }
    }

    public void UpdateUI()
    {
        int idx = currentCharacter.MonsterIndex;
        Debug.Log(idx);
        switch (idx)
        {
            case 1: //golem
                introText.text = "";
                characterImage.GetComponent<Image>().sprite = golemCircleImage;
                //UpdateStatus();
                break;
            case 2: //ghost
                introText.text = "Ready to surprise?\nThis smooth ghost doll going to scary\nall the other dolls!";
                characterImage.GetComponent<Image>().sprite = ghostCircleImage;
                //UpdateStatus();
                break;
            case 3: //dragon
                break;
            case 4: //santa
                introText.text = "";
                characterImage.GetComponent<Image>().sprite = santaCircleImage;
                //UpdateStatus();
                break;
            case 5: //skeleton
                introText.text = "";
                characterImage.GetComponent<Image>().sprite = skeletonCircleImage;
                //UpdateStatus();
                break;
            default:
                break;
        }
    }

    public void UpdateStatus()
    {
        hpDefault.text = currentCharacter.HP.ToString();
        spdDefault.text = currentCharacter.SPD.ToString();
        defDefault.text = currentCharacter.DEF.ToString();
        strDefault.text = currentCharacter.STR.ToString();
        lukDefault.text = currentCharacter.LUK.ToString();


    }


}
