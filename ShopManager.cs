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
    public GameObject contentPanel;
    public GameObject item;

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

    public void LoadItem()
    {

    }

    public void AddItem(int itemNumber)
    {
        var newItem = Instantiate(item);
        //newItem.transform.parent = contentPanel.transform;
        newItem.transform.SetParent(contentPanel.transform, false);
        newItem.GetComponent<Equipment>().equipNumber = itemNumber;
        newItem.GetComponentInChildren<Image>().sprite = golemCircleImage;
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
        int command = currentCharacter.MonsterIndex;
        switch (command)
        {
            case 1: //golem
                introText.text = "Smash everything ahead of you!\nGolem doll looks like rock,\nbut made up with sponges!";
                characterImage.GetComponent<Image>().sprite = golemCircleImage;
                break;
            case 2: //ghost
                introText.text = "Ready to surprise?\nThis smooth ghost doll going to scary\nall the other dolls!";
                characterImage.GetComponent<Image>().sprite = ghostCircleImage;
                break;
            case 3: //dragon
                break;
            case 4: //santa
                introText.text = "Santa keeps running and running...\nfor delivering your present!";
                characterImage.GetComponent<Image>().sprite = santaCircleImage;
                break;
            case 5: //skeleton
                introText.text = "Oh cute skeleton, sadly he think\nhe's in the dungeon now.\nLook at that serious face!";
                characterImage.GetComponent<Image>().sprite = skeletonCircleImage;
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
