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

    [Header("Equip Information")]
    public GameObject infoPanel;
    public GameObject escPanel;
    public EquipDisplay equipDisplay;
    public Image infoImage;
    public Button upgradeBtn;
    public Button sellBtn;
    public Text upgradeCoin;
    public Text upgradeText;
    public Text equipLevelText;
    public List<Text> statText = new List<Text>(4);

    [Header("Inventory")]
    [HideInInspector]
    public GameObject beforeInfoBtn;
    [HideInInspector]
    public GameObject beforeEquipBtn;
    public Text foundItemText;
    public Text storageText;
    //public int foundItem = 0;
    //public int storage;
    public List<Text> equippedLevel = new List<Text>(3);
    [Space(10)]
    public GameObject chkPanel;
    public Text chkText;
    private int checkNumber;

    [Header("DB")]
    public Text equipCoins;
    public Text equipGems;
    public Text shopCoins;
    public Text shopGems;

    //Upgrade coins
    [HideInInspector]
    public int[] normalGold = { 30, 200, 400, 600, 800, 1000, 2000, 3000, 4000, 8000, 10000 };
    [HideInInspector]
    public int[] rareGold = { 100, 800, 1500, 3000, 5000, 8000, 12000, 15000, 20000 };
    [HideInInspector]
    public int[] epicGold = { 500, 3000, 5000, 10000, 15000, 20000, 30000 };
    [HideInInspector]
    public int[] legendGold = { 4000, 15000, 30000, 50000, 100000 };
    //판매가격은 업그레이드 가격의 15%


    // Use this for initialization
    void Start()
    {
        LoadItem();
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

        UpdateShopUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadItem()
    {
        string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
        string _equipped = PlayerPrefs.GetString("Equipped");
        int itemIndex, itemLevel, itemCharacter;

        for(int i=0; i<_inventory.Length-1; i++)
        {
            var newItem = Instantiate(item);
            newItem.transform.SetParent(contentPanel.transform, false);

            //number = System.Convert.ToInt32(_inventory[i].Substring(0,3));
            itemIndex = System.Convert.ToInt32(_inventory[i].Substring(0, 4));
            itemLevel = System.Convert.ToInt32(_inventory[i].Substring(4, 1));
            itemCharacter = _equipped[i]-48;

            //Debug.Log(i + ", " + itemIndex + ", " + itemLevel + ", " + itemCharacter);
            newItem.GetComponent<Equip>().EquipIndex = itemIndex;
            newItem.GetComponent<Equip>().Level = itemLevel;
            newItem.GetComponent<Equip>().equippedCharacter = itemCharacter;

            totalItemSlot.Add(newItem);

            switch (itemIndex / 100)
            {
                case 10:
                    newItem.GetComponentInChildren<Image>().sprite
                        = equipment.normal_item_image[itemIndex%100];
                    break;
                case 11:
                    newItem.GetComponentInChildren<Image>().sprite
                        = equipment.rare_item_image[itemIndex % 100];
                    break;
                case 12:
                    newItem.GetComponentInChildren<Image>().sprite
                        = equipment.epic_item_image[itemIndex % 100];
                    break;
                case 13:
                    newItem.GetComponentInChildren<Image>().sprite
                        = equipment.legend_item_image[itemIndex % 100];
                    break;
                default: Debug.Log("index error!"); break;
            }
        }

        UpdateEquipUI();
    }

    public void CloudLoadItem()
    {
        string _inventory = string.Empty;
        int item;
        for(int i=0; i<100; i++)
        {
            item = CloudVariables.SystemValues[21 + i];
            if (item == 0) break;

            _inventory += item.ToString() + ",";
        }
        PlayerPrefs.SetString("Inventory", _inventory);

        //LoadItem();
    }

    public void CloudSaveItem()
    {

        string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
        //Debug.Log("Saving..." + _inventory.Length);
        for (int i = 0; i < _inventory.Length - 1; i++) 
        {
            int item = System.Convert.ToInt32(_inventory[i]);
            CloudVariables.SystemValues[21 + i] = item;
        }

        PlayGamesScript.Instance.SaveData();
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
        slot.GetComponent<Equip>().Level = currentEquip.Level;
        slot.GetComponentInChildren<Image>().sprite = currentEquip.gameObject.GetComponentInChildren<Image>().sprite;
        //slot.GetComponentInChildren<Text>().text = "+" + currentEquip.Level;
        slot.transform.Find("Text").GetComponent<Text>().text = "+" + currentEquip.Level;

        //slot.GetComponent<Equip>().equippedCharacter = currentCharacter.MonsterIndex;
        for (int i=0; i<totalItemSlot.Count; i++)
        {
            if (totalItemSlot[i].GetComponent<Equip>() == currentEquip)
            {
                slot.GetComponent<Equip>().equippedCharacter = i;
                break;
            }
        }

        currentCharacter.GetComponent<EquippedItem>().equippedItem.Insert(size, currentEquip);
        //currentCharacter.GetComponent<EquippedItem>().size++;

        currentEquip.UpdateFrame(currentCharacter.MonsterIndex);
        currentEquip.isEquipped = true;

        string _equipped = string.Empty;
        for(int i=0; i<totalItemSlot.Count; i++)
        {
            _equipped += totalItemSlot[i].GetComponent<Equip>().equippedCharacter.ToString();
        }
        PlayerPrefs.SetString("Equipped", _equipped);
    }

    public void ReplaceItem(int size, int _resetItem)
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
        slot.GetComponent<Equip>().EquipIndex = currentEquip.Level;
        slot.GetComponentInChildren<Image>().sprite = currentEquip.gameObject.GetComponentInChildren<Image>().sprite;
        //slot.GetComponent<Equip>().equippedCharacter = currentCharacter.MonsterIndex;
        for(int i=0; i<totalItemSlot.Count; i++)
        {
            if(totalItemSlot[i].GetComponent<Equip>() == currentEquip)
            {
                slot.GetComponent<Equip>().equippedCharacter = i;
                break;
            }
        }

        currentCharacter.GetComponent<EquippedItem>().equippedItem.RemoveAt(size);
        currentCharacter.GetComponent<EquippedItem>().equippedItem.Insert(size, currentEquip);

        currentEquip.UpdateFrame(currentCharacter.MonsterIndex);

        string _equipped = string.Empty;
        for (int i = 0; i < totalItemSlot.Count; i++)
        {
            if(i == _resetItem)
            {
                totalItemSlot[_resetItem].GetComponent<Equip>().equippedCharacter = 0;
            }
            _equipped += totalItemSlot[i].GetComponent<Equip>().equippedCharacter.ToString();
        }
        PlayerPrefs.SetString("Equipped", _equipped);
    }

    public void UpdateEquipUI()
    {
        //DB
        equipCoins.text = PlayerPrefs.GetInt("Coin").ToString();
        equipGems.text = PlayerPrefs.GetInt("Gem").ToString();

        int count = 1;
        GameObject slot;

        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);

        currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();

        for(int i=0; i<totalItemSlot.Count; i++)
        {
            if(currentCharacter.MonsterIndex == totalItemSlot[i].GetComponent<Equip>().equippedCharacter)
            {
                switch (count)
                {
                    case 1: slot = slot1; break;
                    case 2: slot = slot2; break;
                    case 3: slot = slot3; break;
                    default: slot = null; break;
                }
                if(slot != null)
                {
                    slot.GetComponent<Equip>().equippedCharacter = i;
                    count++;
                }

                //Debug.Log("equip!");
                
                totalItemSlot[i].GetComponent<Equip>().UpdateFrame(currentCharacter.MonsterIndex);
                currentEquip = totalItemSlot[i].GetComponent<Equip>();
                AddItem(count-2);
            }
            else
            {
                totalItemSlot[i].GetComponent<Equip>().ResetFrame();
            }
        }

        UpdateInventoryText();
    }

    public void UpdateInventoryText()
    {
        storageText.text = totalItemSlot.Count.ToString() + "/100";

        int[] indexArray = new int[totalItemSlot.Count];
        for (int i = 0; i < totalItemSlot.Count; i++)
        {
            indexArray[i] = totalItemSlot[i].GetComponent<Equip>().EquipIndex;
        }
        List<int> temp = indexArray.Distinct().ToList();

        foundItemText.text = "발견한 아이템 " + temp.Count.ToString() + "/40";

        for (int i = 0; i < totalItemSlot.Count; i++)
        {
            totalItemSlot[i].transform.Find("Text").GetComponent<Text>().text
                = "+" + totalItemSlot[i].GetComponent<Equip>().Level.ToString();
        }
    }

    public void UpdateShopUI()
    {
        //시작했을 때와 아이템 구매했을 때 적용
        equipCoins.text = PlayerPrefs.GetInt("Coin").ToString();
        equipGems.text = PlayerPrefs.GetInt("Gem").ToString();

        shopCoins.text = PlayerPrefs.GetInt("Coin").ToString();
        shopGems.text = PlayerPrefs.GetInt("Gem").ToString();
    }

    IEnumerator ChangeItem()
    {
        int resetItem = 0;

        changeSlotNum = -1;
        changeAnimator.Play("Change");
        changeBtn.SetActive(true);
        //changePanel.SetActive(true);

        while (changeSlotNum == -1)
        {
            yield return null;
        }

        if(changeSlotNum != 0)
        {
            changeAnimator.Play("Idle");
            switch (changeSlotNum)
            {
                case 1:
                    resetItem = slot1.GetComponent<Equip>().equippedCharacter;
                    ReplaceItem(0, resetItem);
                    break;
                case 2:
                    resetItem = slot2.GetComponent<Equip>().equippedCharacter;
                    ReplaceItem(1, resetItem);
                    break;
                case 3:
                    resetItem = slot3.GetComponent<Equip>().equippedCharacter;
                    ReplaceItem(2, resetItem);
                    break;
            }

            currentCharacter.GetComponent<EquippedItem>().equippedItem[changeSlotNum-1] = currentEquip;
            currentEquip.isEquipped = true;

            changeBtn.SetActive(false);

            //빠진 아이템 리셋
            Debug.Log("Reset " + resetItem);
            totalItemSlot[resetItem].GetComponent<Equip>().ResetFrame();
            totalItemSlot[resetItem].GetComponent<Equip>().isEquipped = false;

            //UpdateEquipUI();
        }
        else
        {
            changeAnimator.Play("Idle");
            changeBtn.SetActive(false);
            //changePanel.SetActive(false);
            Debug.Log("cancel");
        }

    }

    public void BtnOnChange(int number)
    {
        changeSlotNum = number;
    }

    public void BtnOnInfo()
    {
        Destroy(beforeEquipBtn);
        Destroy(beforeInfoBtn);

        equipDisplay.UpdateUI(currentEquip.EquipIndex);
        UpdateInfoPanel();
        infoPanel.SetActive(true);
        infoImage.sprite = currentEquip.gameObject.GetComponentInChildren<Image>().sprite;
        escPanel.SetActive(true);
    }

    public void BtnOnEsc()
    {
        infoPanel.SetActive(false);
        escPanel.SetActive(false);
    }

    public void BtnOnEquip()
    {
        Destroy(beforeEquipBtn);
        Destroy(beforeInfoBtn);

        EquippedItem _equippedItem = currentCharacter.GetComponent<EquippedItem>();
        bool isDuplicate = false;
        for(int i=0; i< _equippedItem.equippedItem.Count; i++)
        {
            if (_equippedItem.equippedItem[i] == currentEquip)
            {
                Debug.Log(currentEquip.name + " Same Item");
                isDuplicate = true;
            }
        }

        if(!isDuplicate)
        {
            int size = _equippedItem.equippedItem.Count;
            Debug.Log(size);
            if (size < 3)
            {
                AddItem(size);
            }
            else //full
            {
                StartCoroutine("ChangeItem");
            }
        }
    }

    public void BtnOnUnequip()
    {
        Destroy(beforeEquipBtn);
        Destroy(beforeInfoBtn);

        currentEquip.ResetFrame();
        currentEquip.equippedCharacter = 0;
        currentEquip.isEquipped = false;

        int slotcount = 0;
        int size = currentCharacter.GetComponent<EquippedItem>().equippedItem.Count;
        for(int i=0; i<size; i++)
        {
            if(currentEquip.EquipIndex
                == currentCharacter.GetComponent<EquippedItem>().equippedItem[i].EquipIndex)
            {
                slotcount = i;
            }
        }

        switch (slotcount)
        {
            case 0: slot1.SetActive(false); break;
            case 1: slot2.SetActive(false); break;
            case 2: slot3.SetActive(false); break;
            default: break;
        }
        currentCharacter.GetComponent<EquippedItem>().equippedItem.RemoveAt(slotcount);

        string _equipped = string.Empty;
        for (int i = 0; i < totalItemSlot.Count; i++)
        {
            _equipped += totalItemSlot[i].GetComponent<Equip>().equippedCharacter.ToString();
        }
        PlayerPrefs.SetString("Equipped", _equipped);
    }

    public int GetUpgradeGold()
    {
        int requireGold = 0;
        switch (currentEquip.EquipIndex / 100)
        {
            case 10: requireGold = normalGold[currentEquip.Level]; break;
            case 11: requireGold = rareGold[currentEquip.Level]; break;
            case 12: requireGold = epicGold[currentEquip.Level]; break;
            case 13: requireGold = legendGold[currentEquip.Level]; break;
        }
        return requireGold;
    }

    public int GetSellGold()
    {
        float sellGold = 0f;
        switch (currentEquip.EquipIndex / 100)
        {
            case 10: sellGold = normalGold[currentEquip.Level-1]; break;
            case 11: sellGold = rareGold[currentEquip.Level-1]; break;
            case 12: sellGold = epicGold[currentEquip.Level-1]; break;
            case 13: sellGold = legendGold[currentEquip.Level-1]; break;
        }
        sellGold *= 0.15f;
        return (int)sellGold;
    }

    public bool IsUpgradable()
    {
        int level = currentEquip.Level;
        switch (currentEquip.EquipIndex)
        {
            case 10:
                if (level == 11) return false;
                else return true;
            case 11:
                if (level == 9) return false;
                else return true;
            case 12:
                if (level == 7) return false;
                else return true;
            case 13:
                if (level == 5) return false;
                else return true;
            default: return true;
        }
    }

    public void BtnOnUpgrade()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        coin -= GetUpgradeGold();

        for(int i=0; i<totalItemSlot.Count; i++)
        {
            if (totalItemSlot[i].GetComponent<Equip>() == currentEquip)
            {
                //Debug.Log("Lv Up " + i);
                LevelUp(i); break;
            }
        }
        equipCoins.text = coin.ToString();
        

        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
        CloudVariables.SystemValues[0] = coin;
        PlayGamesScript.Instance.SaveData();

        //Cloud에 아이템 업그레이드 저장
        CloudSaveItem();
        UpdateInfoPanel();
    }

    public void LevelUp(int index) //index는 totalItemSlot의 위치.
    {
        currentEquip.Level++;

        string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
        string temp = _inventory[index];
        int curLevel = temp[4]-48;

        string _temp = temp.Substring(0, 4) + (curLevel + 1).ToString();
        _inventory[index] = _temp;

        string inventory = string.Empty;
        for (int i = 0; i < _inventory.Length-1; i++)
        {
            inventory += (_inventory[i] + ",");
        }

        PlayerPrefs.SetString("Inventory", inventory);
        PlayerPrefs.Save();

        UpdateInventoryText();
        for (int i = 0; i < currentCharacter.GetComponent<EquippedItem>().equippedItem.Count; i++)
        {
            equippedLevel[i].text
                = "+" + currentCharacter.GetComponent<EquippedItem>().equippedItem[i].Level.ToString();
        }
    }

    public void UpdateInfoPanel()
    {
        //돈 부족하거나 만렙이면 upgrade버튼 비활성화
        if (GetUpgradeGold() > PlayerPrefs.GetInt("Coin"))
        {
            upgradeBtn.interactable = false;
        }
        else
        {
            upgradeBtn.interactable = true;
        }

        if (IsUpgradable() == true)
        {
            upgradeText.text = "Upgrade";
            upgradeCoin.text = GetUpgradeGold().ToString();
        }
        else
        {
            upgradeText.text = "MAX";
            upgradeCoin.text = string.Empty;
        }

        if(currentEquip.isEquipped == true)
        {
            sellBtn.interactable = false;
        }
        else
        {
            sellBtn.interactable = true;
        }

        equipLevelText.text = "+" + currentEquip.Level;

        float[] stats = new float[8] {0,0,0,0,0,0,0,0 };

        Equip origin_equip = new Equip();
        origin_equip.EquipIndex = currentEquip.EquipIndex;
        origin_equip.Level = 1;
        origin_equip.SetStatus();

        Equip temp_equip = new Equip();
        temp_equip.EquipIndex = currentEquip.EquipIndex;
        temp_equip.Level = currentEquip.Level;
        temp_equip.SetStatus();

        stats[0] += temp_equip.HP - origin_equip.HP;
        stats[1] += temp_equip.SPD - origin_equip.SPD;
        stats[2] += temp_equip.DEF - origin_equip.DEF;
        stats[3] += temp_equip.STR - origin_equip.STR;
        stats[4] += temp_equip.LUK - origin_equip.LUK;
        stats[5] += temp_equip.nitroEarnSize - origin_equip.nitroEarnSize;
        stats[6] += temp_equip.bombSize - origin_equip.bombSize;
        stats[7] += temp_equip.nitroSpeed - origin_equip.nitroSpeed;

        int count = 0;
        for(int i=0; i<8; i++)
        {
            //Debug.Log("Stat" + i + " " + stats[i]);
            if (stats[i] != 0)
            {
                if(stats[i] > 0)
                {
                    statText[count].text += "<color=#FF0000> +" + stats[i].ToString()+"</color>";
                }
                count++;
            }
        }
        Destroy(origin_equip);
        Destroy(temp_equip);
    }

    public IEnumerator SellCoroutine()
    {
        while(checkNumber == -1)
        {
            yield return null;
        }
        if(checkNumber == 0) //NO
        {
            Debug.Log("cancel");
        }
        else if(checkNumber == 1) //YES
        {
            int coin = PlayerPrefs.GetInt("Coin");
            coin += GetSellGold();
            equipCoins.text = coin.ToString();

            string equipped = PlayerPrefs.GetString("Equipped");
            string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
            string temp = string.Empty;
            for (int i = 0; i < totalItemSlot.Count; i++)
            {
                if (totalItemSlot[i].GetComponent<Equip>() == currentEquip)
                {
                    _inventory[i] = temp;
                    Destroy(totalItemSlot[i]);
                    totalItemSlot.RemoveAt(i);

                    string start = equipped.Substring(0, i);
                    string end = equipped.Substring(i + 1, equipped.Length - i - 1);
                    equipped = start + end;
                    break;
                }
            }
            PlayerPrefs.SetString("Equipped", equipped);

            string inventory = string.Empty;
            for (int i = 0; i < _inventory.Length - 1; i++)
            {
                if (_inventory[i] != string.Empty)
                {
                    inventory += (_inventory[i] + ",");
                }
                Debug.Log(inventory);
            }
            PlayerPrefs.SetString("Inventory", inventory);
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.Save();
            CloudVariables.SystemValues[21 + totalItemSlot.Count] = 0;

            CloudSaveItem();
            CloudVariables.SystemValues[0] = coin;
            PlayGamesScript.Instance.SaveData();

            infoPanel.SetActive(false);
            UpdateInventoryText();
        }
    }

    public void BtnOnSell()
    {
        checkNumber = -1;
        StartCoroutine("SellCoroutine");
        chkText.text = "<color=#FFFF00>" + GetSellGold() + "</color>코인에 판매하시겠습니까?";
        chkPanel.SetActive(true);
    }

    public void BtnOnYes()
    {
        checkNumber = 1;
        chkPanel.SetActive(false);
    }

    public void BtnOnNo()
    {
        checkNumber = 0;
        chkPanel.SetActive(false);
    }

    public void NewItem(int itemNumber, int itemValue)
    {
        var newItem = Instantiate(item);
        //newItem.transform.parent = contentPanel.transform;
        newItem.transform.SetParent(contentPanel.transform, false);
        //newItem.AddComponent<Equip>();
        int itemNum = 0;

        switch (itemValue)
        {
            case 0:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.normal_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1000;
                itemNum = itemNumber + 1000;
                break;
            case 1:
                newItem.GetComponentInChildren<Image>().sprite
                    = equipment.rare_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1100;
                itemNum = itemNumber + 1100;
                break;
            case 2:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.epic_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1200;
                itemNum = itemNumber + 1200;
                break;
            case 3:
                newItem.GetComponentInChildren<Image>().sprite 
                    = equipment.legend_item_image[itemNumber];
                newItem.GetComponent<Equip>().EquipIndex = itemNumber + 1300;
                itemNum = itemNumber + 1300;
                break;
            default:
                break;
        }

        newItem.GetComponent<Equip>().Level = 1;
        int inventoryCount = totalItemSlot.Count;
        if(inventoryCount < 100)
        {
            totalItemSlot.Add(newItem);
            //save
            //string temp = inventoryCount.ToString("000") + itemNum.ToString("0000") + "1,";
            string temp = itemNum.ToString("0000") + "1,"; //item index, level
            string _inventory = PlayerPrefs.GetString("Inventory");
            _inventory += temp;
            PlayerPrefs.SetString("Inventory", _inventory);

            temp = "0";
            string _equipped = PlayerPrefs.GetString("Equipped");
            _equipped += temp;
            PlayerPrefs.SetString("Equipped", _equipped);
        }
        else
        {
            Debug.Log("Inventory full!");
        }

        //currentItemNumber = totalItemSlot.Count;
    }

    public void ApplyEquipment()
    {
        equip_hp = 0;
        equip_spd = 0;
        equip_def = 0;
        equip_str = 0;
        equip_luk = 0;
        equip_nitroEarnSize = 0;
        equip_bombSize = 0;
        equip_nitroTime = 0;

        if (currentCharacter != null)
        {
            for (int i = 0; i < currentCharacter.GetComponent<EquippedItem>().equippedItem.Count; i++)
            {
                currentCharacter.GetComponent<EquippedItem>().equippedItem[i].GetComponent<Equip>().SetStatus();
                equip = currentCharacter.GetComponent<EquippedItem>().equippedItem[i];

                equip_hp += equip.HP;
                equip_spd += equip.SPD;
                equip_def += equip.DEF;
                equip_str += equip.STR;
                equip_luk += equip.LUK;

                equip_nitroEarnSize += equip.nitroEarnSize;
                equip_bombSize += equip.bombSize;
                equip_nitroTime += equip.nitroSpeed;
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

        UpdateEquipUI();
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
