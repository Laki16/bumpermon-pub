﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //[HideInInspector]
    [Header("System")]
    public Character currentCharacter;
    public Equipment equipment;
    public SoundManager soundManager;

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
    public GameObject upgradeBtn;
    public GameObject sellBtn;
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
    public int storage;
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
    public int[] normalGold = { 200, 500, 1000, 2500, 3500, 5000, 7500, 10000, 15000 };
    [HideInInspector]
    public int[] rareGold = { 1500, 3000, 5000, 7500, 10000, 15000, 20000, 30000, 50000 };
    [HideInInspector]
    public int[] epicGold = { 10000, 15000, 20000, 30000, 45000, 60000, 80000, 120000, 170000 };
    [HideInInspector]
    public int[] legendGold = { 20000, 30000, 50000, 100000, 160000, 220000, 350000, 500000, 700000 };
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

    public void LoadItem()
    {
        string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
        string _equipped = PlayerPrefs.GetString("Equipped");
        int itemIndex, itemLevel, itemCharacter;

        for(int i=0; i<_inventory.Length-1; i++)
        {
            var newItem = Instantiate(item);
            newItem.transform.SetParent(contentPanel.transform, false);

            itemIndex = System.Convert.ToInt32(_inventory[i].Substring(0, 4));
            itemLevel = System.Convert.ToInt32(_inventory[i].Substring(4, 1));
            itemCharacter = _equipped[i]-48;

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
    }

    public void CloudSaveItem()
    {

        string[] _inventory = PlayerPrefs.GetString("Inventory").Split(',');
        for (int i = 0; i < _inventory.Length - 1; i++) 
        {
            int item = System.Convert.ToInt32(_inventory[i]);
            CloudVariables.SystemValues[21 + i] = item;
        }

        PlayGamesScript.Instance.SaveData();
    }

    public void AddItem(int size, int type) //type 0:normal, 1:null change
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
        slot.transform.Find("Text").GetComponent<Text>().text = "+" + currentEquip.Level;

        for (int i=0; i<totalItemSlot.Count; i++)
        {
            if (totalItemSlot[i].GetComponent<Equip>() == currentEquip)
            {
                slot.GetComponent<Equip>().equippedCharacter = i;
                break;
            }
        }

        if(type == 0) //normal ->insert
        {
            currentCharacter.GetComponent<EquippedItem>().equippedItem.Insert(size, currentEquip);
        }
        else if(type == 1) //null -> change
        {
            currentCharacter.GetComponent<EquippedItem>().equippedItem[size] = currentEquip;
        }

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
        slot.GetComponent<Equip>().Level = currentEquip.Level;
        slot.GetComponentInChildren<Image>().sprite = currentEquip.gameObject.GetComponentInChildren<Image>().sprite;
        slot.transform.Find("Text").GetComponent<Text>().text = "+" + currentEquip.Level;
        for (int i=0; i<totalItemSlot.Count; i++)
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

        if(currentCharacter != null)
        {
            currentCharacter.GetComponent<EquippedItem>().equippedItem.Clear();

            for (int i = 0; i < totalItemSlot.Count; i++)
            {
                if (currentCharacter.MonsterIndex == totalItemSlot[i].GetComponent<Equip>().equippedCharacter)
                {
                    switch (count)
                    {
                        case 1: slot = slot1; break;
                        case 2: slot = slot2; break;
                        case 3: slot = slot3; break;
                        default: slot = null; break;
                    }
                    if (slot != null)
                    {
                        slot.GetComponent<Equip>().equippedCharacter = i;
                        count++;
                    }
                    totalItemSlot[i].GetComponent<Equip>().UpdateFrame(currentCharacter.MonsterIndex);
                    currentEquip = totalItemSlot[i].GetComponent<Equip>();
                    AddItem(count - 2, 0);
                }
                else
                {
                    totalItemSlot[i].GetComponent<Equip>().ResetFrame();
                }
            }
        }

        

        UpdateInventoryText();
    }

    public void UpdateInventoryText()
    {
        storage = totalItemSlot.Count;
        storageText.text = storage.ToString() + "/100";

        int[] indexArray = new int[totalItemSlot.Count];
        for (int i = 0; i < totalItemSlot.Count; i++)
        {
            indexArray[i] = totalItemSlot[i].GetComponent<Equip>().EquipIndex;
        }
        List<int> temp = indexArray.Distinct().ToList();

        foundItemText.text = "발견한 아이템 " + temp.Count.ToString() + "/30";

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
        }
        else
        {
            changeAnimator.Play("Idle");
            changeBtn.SetActive(false);
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

        if (!currentEquip.isSlot)
        {
            upgradeBtn.SetActive(true);
            sellBtn.SetActive(true);
        }
        else
        {
            upgradeBtn.SetActive(false);
            sellBtn.SetActive(false);
        }

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
            if (_equippedItem.equippedItem[i] != null
                && _equippedItem.equippedItem[i] == currentEquip)
            {
                Debug.Log(currentEquip.name + " Same Item");
                isDuplicate = true;
            }
        }

        if (!isDuplicate)
        {
            bool isNull = false;
            int size = _equippedItem.equippedItem.Count;
            for (int i = 0; i < _equippedItem.equippedItem.Count; i++)
            {
                if (_equippedItem.equippedItem[i] == null)
                {
                    size = i;
                    AddItem(size, 1);
                    isNull = true;
                    break;
                }
            }
            if(isNull == false)
            {
                if (size < 3)
                {
                    AddItem(size, 0);
                }
                else //full
                {
                    StartCoroutine("ChangeItem");
                }
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
            if(currentCharacter.GetComponent<EquippedItem>().equippedItem[i] == null)
            {

            }
            else if(currentEquip.EquipIndex
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
        currentCharacter.GetComponent<EquippedItem>().equippedItem[slotcount] = null;

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
            case 10: requireGold = normalGold[currentEquip.Level-1]; break;
            case 11: requireGold = rareGold[currentEquip.Level-1]; break;
            case 12: requireGold = epicGold[currentEquip.Level-1]; break;
            case 13: requireGold = legendGold[currentEquip.Level-1]; break;
            default: requireGold = -1; break;
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
            default: sellGold = -1; break;
        }
        sellGold *= 0.15f;
        return (int)sellGold;
    }

    public bool IsUpgradable()
    {
        if(currentEquip.Level >= 9)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void BtnOnUpgrade()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        if(coin >= GetUpgradeGold())
        {
            for (int i = 0; i < totalItemSlot.Count; i++)
            {
                if (totalItemSlot[i].GetComponent<Equip>() == currentEquip)
                {
                    coin -= GetUpgradeGold();
                    LevelUp(i);
                    break;
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
            UpdateShopUI();

            soundManager.PlayLvUp(2);
        }
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
            if(currentCharacter.GetComponent<EquippedItem>().equippedItem[i] != null)
            {
                equippedLevel[i].GetComponentInParent<Equip>().Level
                    = currentCharacter.GetComponent<EquippedItem>().equippedItem[i].Level;
                equippedLevel[i].text
                    = "+" + currentCharacter.GetComponent<EquippedItem>().equippedItem[i].Level.ToString();
            }
        }

    }

    public void UpdateInfoPanel()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        //돈 부족하거나 만렙이면 upgrade버튼 비활성화
        if (GetUpgradeGold() > coin || IsUpgradable() == false)
        {
            if(IsUpgradable() == false)
            {
                upgradeText.text = "MAX";
                upgradeCoin.text = string.Empty;
            }
            else
            {
                upgradeText.text = "Upgrade";
                upgradeCoin.text = GetUpgradeGold().ToString();
            }
            upgradeBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            upgradeText.text = "Upgrade";
            upgradeCoin.text = GetUpgradeGold().ToString();
            upgradeBtn.GetComponent<Button>().interactable = true;
        }

        if (currentEquip.isEquipped)
        {
            sellBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            sellBtn.GetComponent<Button>().interactable = true;
        }

        equipLevelText.text = "+" + currentEquip.Level;

        UpdateStatUI();
    }

    public void UpdateStatUI()
    {
        if (currentEquip.isSlot)
        {
            currentEquip.HP = 0;
            currentEquip.SPD = 0;
            currentEquip.DEF = 0;
            currentEquip.STR = 0;
            currentEquip.LUK = 0;
            currentEquip.nitroEarnSize = 0;
            currentEquip.bombSize = 0;
            currentEquip.nitroSpeed = 0;
        }

        for(int i=0; i<4; i++)
        {
            statText[i].text = string.Empty;
        }

        float[] stats = new float[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        currentEquip.SetStatus();
        stats[0] += currentEquip.HP;
        stats[1] += currentEquip.SPD;
        stats[2] += currentEquip.DEF;
        stats[3] += currentEquip.STR;
        stats[4] += currentEquip.LUK;
        stats[5] += currentEquip.nitroEarnSize;
        stats[6] += currentEquip.bombSize;
        stats[7] += currentEquip.nitroSpeed;
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            if (stats[i] != 0 && count < 4)
            {
                switch (i)
                {
                    case 0:
                        statText[count].text = "체력";
                        break;
                    case 1:
                        statText[count].text = "속도";
                        break;
                    case 2:
                        statText[count].text = "방어력";
                        break;
                    case 3:
                        statText[count].text = "힘";
                        break;
                    case 4:
                        statText[count].text = "행운";
                        break;
                    case 5:
                        statText[count].text = "부스트 획득량";
                        break;
                    case 6:
                        statText[count].text = "폭발 범위";
                        break;
                    case 7:
                        statText[count].text = "부스트 스피드";
                        break;
                }
                if (stats[i] > 0)
                {
                    statText[count].text += " + " + stats[i].ToString();
                }
                else
                {
                    statText[count].text += " " + stats[i].ToString();
                }
                if (i == 6) statText[count].text += "m";
                else if (i == 7) statText[count].text += "%";
                count++;
            }
        }
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
                //Debug.Log(inventory);
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
            escPanel.SetActive(false);

            soundManager.PlayEquip();
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
        newItem.transform.SetParent(contentPanel.transform, false);
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

    public void ApplyEquipmentToCharacter()
    {
        if (currentCharacter != null)
        {
            for (int i = 0; i < currentCharacter.GetComponent<EquippedItem>().equippedItem.Count; i++)
            {
                if(currentCharacter.GetComponent<EquippedItem>().equippedItem[i] != null)
                {
                    equip = currentCharacter.GetComponent<EquippedItem>().equippedItem[i];
                    currentCharacter.HP += equip.HP;
                    currentCharacter.SPD += equip.SPD;
                    currentCharacter.DEF += equip.DEF;
                    currentCharacter.STR += equip.STR;
                    currentCharacter.LUK += equip.LUK;
                    currentCharacter.nitroEarnSize += equip.nitroEarnSize;
                    currentCharacter.bombSize += equip.bombSize;
                    currentCharacter.nitroSpeed += equip.nitroSpeed;
                }
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
        hpUpgraded.text = "+";
        spdUpgraded.text = "+";
        defUpgraded.text = "+";
        strUpgraded.text = "+";
        lukUpgraded.text = "+";
        nSizeUpgraded.text = "+";
        bSizeUpgraded.text = "+";
        nTimeUpgraded.text = "+";

        ApplyEquipment();

        hpDefault.text = currentCharacter.HP.ToString();
        spdDefault.text = currentCharacter.SPD.ToString();
        defDefault.text = currentCharacter.DEF.ToString();
        strDefault.text = currentCharacter.STR.ToString();
        lukDefault.text = currentCharacter.LUK.ToString();
        nSizeDefault.text = currentCharacter.nitroEarnSize.ToString();
        bSizeDefault.text = currentCharacter.bombSize.ToString() + "m";
        nTimeDefault.text = currentCharacter.nitroSpeed.ToString() + "%";

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

