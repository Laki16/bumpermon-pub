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
    
    //[HideInInspector]
    public Character currentCharacter;
    public Equipment equipment;

    [Header("Equipment")]
    public List<GameObject> totalItemSlot = new List<GameObject>();
    private int currentItemNumber;
    public List<Equip> equippedItem = new List<Equip>();
    private int maxEquipment = 3;
    public GameObject contentPanel;
    public GameObject item;

    [Header("UI")]
    public GameObject accessoryPanel;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    [Space(10)]
    public GameObject characterImage;
    public Sprite golemCircleImage;
    public Sprite ghostCircleImage;
    public Sprite santaCircleImage;
    public Sprite skeletonCircleImage;
    [Space(10)]
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
    public Text nTimeDefault;
    public Text nTimeUpgraded;

    //Stats
    private float equip_hp;
    private float equip_spd;
    private float equip_def;
    private float equip_str;
    private float equip_luk;
    private float equip_nitroEarnSize;
    private float equip_bombSize;
    private float equip_nitroTime;
    private Equip equip;

    // Use this for initialization
    void Start()
    {
        equip_hp = 0.0f;
        equip_spd = 0.0f;
        equip_def = 0.0f;
        equip_str = 0.0f;
        equip_luk = 0.0f;
        equip_nitroEarnSize = 0.0f;
        equip_bombSize = 0.0f;
        equip_nitroTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadItem()
    {

    }

    public void AddItem()
    {

    }

    public void NewItem(int itemNumber, int itemValue)
    {
        var newItem = Instantiate(item);
        //newItem.transform.parent = contentPanel.transform;
        newItem.transform.SetParent(contentPanel.transform, false);
        newItem.AddComponent<Equip>();
        newItem.GetComponent<Equip>().EquipIndex = itemNumber;

        switch (itemValue)
        {
            case 0:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.GetComponent<Equipment>().normal_item_image[itemNumber];
                break;
            case 1:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.GetComponent<Equipment>().rare_item_image[itemNumber];
                break;
            case 2:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.GetComponent<Equipment>().epic_item_image[itemNumber];
                break;
            case 3:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.GetComponent<Equipment>().legend_item_image[itemNumber];
                break;
            default:
                break;
        }

        totalItemSlot.Add(newItem);
        //currentItemNumber = totalItemSlot.Count;
    }

    public void ApplyAccessory()
    {
        for (int i = 0; i < 3; i++)
        {
            equip = equippedItem[i].GetComponent<Equip>();

            equip_hp += equip.HP;
            equip_spd += equip.SPD;
            equip_def += equip.DEF;
            equip_str += equip.STR;
            equip_luk += equip.LUK;

            equip_nitroEarnSize += equip.nitroSize;
            equip_bombSize += equip.bombSize;
            equip_nitroTime += equip.nitroTime;
        }
    }
    
    public void UpdateUI()
    {
        int command = currentCharacter.MonsterIndex;
        switch (command)
        {
            case 1: //golem
                introText.text = "앞에 있는 모든 것을 부숴버리세요!\n골렘 인형은 무척 단단하게 생겼지만,\n사실 스펀지로 이루어져 있습니다.";
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
        ApplyAccessory();
        hpDefault.text = currentCharacter.HP.ToString();
        spdDefault.text = currentCharacter.SPD.ToString();
        defDefault.text = currentCharacter.DEF.ToString();
        strDefault.text = currentCharacter.STR.ToString();
        lukDefault.text = currentCharacter.LUK.ToString();
        nSizeDefault.text = currentCharacter.nitroEarnSize.ToString();
        bSizeDefault.text = currentCharacter.bombSize.ToString();
        nTimeDefault.text = currentCharacter.nitroTime.ToString();

        hpUpgraded.text = equip_hp.ToString();
        spdUpgraded.text = equip_spd.ToString();
        defUpgraded.text = equip_def.ToString();
        strUpgraded.text = equip_str.ToString();
        lukUpgraded.text = equip_luk.ToString();
        nSizeUpgraded.text = equip_nitroEarnSize.ToString();
        bSizeUpgraded.text = equip_bombSize.ToString();
        nTimeUpgraded.text = equip_nitroTime.ToString();
    }


}
