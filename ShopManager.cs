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
    public GameObject contentPanel;
    public Animator changeAnimator;
    public GameObject item;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject changeBtn;
    private int changeSlotNum;

    [Header("UI")]
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
    public Equip currentEquip;

    // Use this for initialization
    void Start()
    {
        equipment = GetComponent<Equipment>();
        changeSlotNum = -1;

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

    public void AddItem(int size)
    {
        GameObject slot;
        switch (size)
        {
            case 0: slot = slot1; break;
            case 1: slot = slot2; break;
            case 2: slot = slot3; break;
            default: slot = null; break;
        }
        slot.SetActive(true);
        slot.GetComponent<Equip>().EquipIndex = currentEquip.EquipIndex;
        slot.GetComponentInChildren<Image>().sprite = currentEquip.gameObject.GetComponentInChildren<Image>().sprite;
        //장착한 아이템 테두리 변경

    }

    IEnumerator ChangeItem()
    {
        changeAnimator.Play("Change");
        changeBtn.SetActive(true);
        while(changeSlotNum == -1)
        {
            yield return null;
        }

        changeAnimator.Play("Idle");
        switch (changeSlotNum)
        {
            case 0: AddItem(0);
                break;
            case 1: AddItem(1);
                break;
            case 2: AddItem(2);
                break;
        }
        changeSlotNum = -1;
        changeBtn.SetActive(false);
    }

    public void BtnOnChange(int number)
    {
        changeSlotNum = number;
    }

    public void BtnOnEquip()
    {
        int size = currentCharacter.GetComponent<EquippedItem>().size;
        Debug.Log(size);
        if (size < 3)
        {
            currentCharacter.GetComponent<EquippedItem>().equippedItem.Insert(size, currentEquip);
            currentCharacter.GetComponent<EquippedItem>().size++;
            AddItem(size);
        }
        else //full
        {
            StartCoroutine("ChangeItem");
        }
    }

    public void BtnOnUpgrade()
    {

    }

    public void BtnOnSell()
    {

    }

    public void NewItem(int itemNumber, int itemValue)
    {
        var newItem = Instantiate(item);
        //newItem.transform.parent = contentPanel.transform;
        newItem.transform.SetParent(contentPanel.transform, false);
        //newItem.AddComponent<Equip>();

        switch (itemValue)
        {
            case 0:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.normal_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1000;
                break;
            case 1:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.rare_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1100;
                break;
            case 2:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.epic_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1200;
                break;
            case 3:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.legend_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1300;
                break;
            default:
                break;
        }

        totalItemSlot.Add(newItem);
        //currentItemNumber = totalItemSlot.Count;
    }

    public void ApplyEquipment()
    {
        if(currentCharacter != null)
        {
            for (int i = 0; i < currentCharacter.GetComponent<EquippedItem>().equippedItem.Count; i++)
            {
                equip = currentCharacter.GetComponent<EquippedItem>().equippedItem[i];

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
                introText.text = "놀랄 준비 되었나요?\n이 말랑한 유령 인형은 다른 인형들을\n놀래키는걸 좋아한답니다!";
                characterImage.GetComponent<Image>().sprite = ghostCircleImage;
                break;
            case 3: //dragon
                break;
            case 4: //santa
                introText.text = "오늘도 산타는 달리고 달리죠..\n당신에게 선물을 전달해주기 위해서요!";
                characterImage.GetComponent<Image>().sprite = santaCircleImage;
                break;
            case 5: //skeleton
                introText.text = "이 귀여운 스켈레톤 병사는 슬프게도\n여기가 던전인줄 안답니다.\n저 진지한 표정 좀 보세요!";
                characterImage.GetComponent<Image>().sprite = skeletonCircleImage;
                break;
            default:
                break;
        }
    }

    public void UpdateStatus()
    {
        ApplyEquipment();
        hpDefault.text = currentCharacter.HP.ToString();
        spdDefault.text = currentCharacter.SPD.ToString();
        defDefault.text = currentCharacter.DEF.ToString();
        strDefault.text = currentCharacter.STR.ToString();
        lukDefault.text = currentCharacter.LUK.ToString();
        nSizeDefault.text = currentCharacter.nitroEarnSize.ToString();
        bSizeDefault.text = currentCharacter.bombSize.ToString();
        nTimeDefault.text = currentCharacter.nitroTime.ToString();

        hpUpgraded.text = "+" + equip_hp.ToString();
        spdUpgraded.text = "+" + equip_spd.ToString();
        defUpgraded.text = "+" + equip_def.ToString();
        strUpgraded.text = "+" + equip_str.ToString();
        lukUpgraded.text = "+" + equip_luk.ToString();
        nSizeUpgraded.text = "+" + equip_nitroEarnSize.ToString();
        bSizeUpgraded.text = "+" + equip_bombSize.ToString();
        nTimeUpgraded.text = "+" + equip_nitroTime.ToString();
    }


}
