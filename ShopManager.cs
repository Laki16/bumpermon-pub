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
    public Equipment equipmentItem;

    [Header("Equipment")]
    public List<Accessory> equipments = new List<Accessory>();
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

    public void AddItem()
    {

    }

    public void NewItem(int itemNumber, int itemValue)
    {
        var newItem = Instantiate(item);
        //newItem.transform.parent = contentPanel.transform;
        newItem.transform.SetParent(contentPanel.transform, false);
        newItem.GetComponent<Equipment>().equipNumber = itemNumber;

        switch (itemValue)
        {
            case 0:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipmentItem.GetComponent<Equipment>().normal_item_image[itemNumber];
                break;
            case 1:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipmentItem.GetComponent<Equipment>().rare_item_image[itemNumber];
                break;
            case 2:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipmentItem.GetComponent<Equipment>().epic_item_image[itemNumber];
                break;
            case 3:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipmentItem.GetComponent<Equipment>().legend_item_image[itemNumber];
                break;
            default:
                break;
        }

    }

    public void ApplyAccessory()
    {
        for (int i = 0; i < equipments.Count; i++)
        {
            //스탯 적용
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
        hpDefault.text = currentCharacter.HP.ToString();
        spdDefault.text = currentCharacter.SPD.ToString();
        defDefault.text = currentCharacter.DEF.ToString();
        strDefault.text = currentCharacter.STR.ToString();
        lukDefault.text = currentCharacter.LUK.ToString();


    }


}
