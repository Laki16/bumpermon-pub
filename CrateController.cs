using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateController : MonoBehaviour {

    [Header("System")]
    public Canvas canvas;
    public GameObject mainPanel;
    public GameObject mainCam;
    public Camera crateCam;
    public GameObject chkPanel;
    public Text chkText;
    public Equipment equipment;
    public ShopManager shopManager;
    public PurchaseManager purchaseManager;
    public GameObject errorPanel;
    [Space(10)]
    public GameObject crates;
    public GameObject currentCrate;
    public GameObject boxCrate;
    public GameObject simpleCrate;
    public GameObject metalCrate;
    public GameObject superCrate;

    [Header("Particle Effect")]
    //public GameObject boxIdleFx;
    public GameObject simpleIdleFX;
    public GameObject metalIdleFX;
    public GameObject superIdleFX;
    [Space(10)]
    //public GameObject boxDropFX;
    public GameObject metalDropFX;
    public GameObject superDropFX;
    public GameObject superDropFX2;
    //public GameObject metalOpenFX;
    public GameObject superOpenFX;
    [Space(10)]
    public GameObject normalCardFX;
    public GameObject rareCardFX;
    public GameObject epicCardFX;
    public GameObject legendCardFX;
    public GameObject eventCardFX;
    
    //system variables
    private int chk;
    private int index;
    private int crateNum;
    private int crateOpenNum;
    private int crate_empty;
    private int crate_gold;
    private int crate_gem;
    private int crate_normal;
    private int crate_rare;
    private int crate_epic;
    private int crate_legend;
    private int prob;
    private List<int> usedProb = new List<int>();
    private bool open = false;
    //private bool interactable = false;

    [Header("Image")]
    //public GameObject item;
    //public GameObject flipItem;
    public Image currentItemImg;
    //public Text currentItemTitle;
    public Text currentItemTitle;
    [Space(5)]
    public Sprite gold_sprite;
    public Sprite gem_sprite;

    public void BtnOnCrate(int _index)
    {
        crateNum = 0;
        chk = -1;
        index = _index;
        usedProb.Clear();

        int crateEquipNumber = 0;
        switch (index)
        {
            case 1:
                crateEquipNumber = 1;
                break;
            case 2: //메탈 상자
                crateEquipNumber = 3;
                break;
            case 3: //슈퍼 상자
                crateEquipNumber = 5;
                break;
            case 4: //나무 상자
                crateEquipNumber = 1;
                break;
            default:
                break;
        }
        if(crateEquipNumber + shopManager.storage <= 100)
        {
            StartCoroutine("BoxOpening");
        }
        else
        {
            errorPanel.SetActive(true);
        }
    }

    public void ChkErrorMsg()
    {
        errorPanel.SetActive(false);
    }

    public void BtnOnNo()
    {
        chk = 0;
        purchaseManager.chkIndex = 0;
    }

    public void BtnOnYes()
    {
        chk = 1;
        purchaseManager.chkIndex = 1;
    }

    IEnumerator BoxOpening()
    {
        switch (index)
        {
            case 1: //기본 박스
                chk = 1;
                break;
            case 2: //메탈 상자
                chkPanel.SetActive(true);
                chkText.text = "메탈 상자를 구매하시겠습니까?";
                break;
            case 3: //슈퍼 상자
                chkPanel.SetActive(true);
                chkText.text = "슈퍼 상자를 구매하시겠습니까?";
                break;
            case 4: //나무 상자
                chk = 1;
                break;
            default:
                break;
        }

        while (chk == -1)
        {
            yield return null;
        }

        chkPanel.SetActive(false);

        if (chk == 0)
        {
            Debug.Log("Cancel");
            yield break;
        }
        else
        {
            switch (index)
            {
                case 1:
                    currentCrate = boxCrate;
                    break;
                case 2:
                    currentCrate = metalCrate;
                    break;
                case 3:
                    currentCrate = superCrate;
                    break;
                case 4:
                    currentCrate = simpleCrate;
                    break;
                default:
                    break;
            }
            StartCoroutine("FadeIn");
        }

        currentCrate.SetActive(true);
        //mainBtn.interactable = false;
        crates.GetComponent<Animator>().Play("Drop", -1, 0f);

        float time = 0.0f;
        float interval = 1.0f;
        while(time < interval)
        {
            yield return null;
            time += Time.deltaTime;
        }
        mainPanel.GetComponent<Animator>().Play("Ready", -1, 0f);
    }

    IEnumerator FadeIn()
    {
        ResetFX();

        float timer = 0.0f;
        float interval = .5f;

        mainPanel.SetActive(true);

        while (timer < interval)
        {
            timer += Time.deltaTime;
            mainPanel.GetComponent<Image>().color
                = Color32.Lerp(mainPanel.GetComponent<Image>().color, new Color(0, 0, 0, 255), timer);
            yield return null;
        }

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        mainCam.SetActive(false);

        IdleFXEvent();

        timer = 0.0f;
        while (timer < interval + .5f)
        {
            timer += Time.deltaTime;
            mainPanel.GetComponent<Image>().color
                = Color32.Lerp(mainPanel.GetComponent<Image>().color, new Color(255, 255, 255, 255), timer);
            yield return null;
        }

    }

    IEnumerator FadeOut()
    {
        currentCrate.SetActive(false);

        float timer = 0.0f;
        float interval = .4f;
        while (timer < interval)
        {
            timer += Time.deltaTime;
            mainPanel.GetComponent<Image>().color
                = Color32.Lerp(mainPanel.GetComponent<Image>().color, new Color(255, 255, 255, 0), timer);
            yield return null;
        }

        mainPanel.SetActive(false);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        mainCam.SetActive(true);

    }

    public void ResetFX()
    {
        metalDropFX.SetActive(false);
        superDropFX.SetActive(false);
        superDropFX2.SetActive(false);
        superOpenFX.SetActive(false);

        normalCardFX.SetActive(false);
        rareCardFX.SetActive(false);
        epicCardFX.SetActive(false);
        legendCardFX.SetActive(false);
    }

    public void IdleFXEvent()
    {
        switch (index)
        {
            case 1:
                //boxIdleFx.SetActive(true);
                break;
            case 2:
                metalIdleFX.SetActive(true);
                break;
            case 3:
                superIdleFX.SetActive(true);
                break;
            case 4:
                simpleIdleFX.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void IdleFXEndEvent()
    {
        switch (index)
        {
            case 1:
                break;
            case 2:
                metalIdleFX.SetActive(false);
                break;
            case 3:
                superIdleFX.SetActive(false);
                break;
            case 4:
                simpleIdleFX.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void DropFXEvent()
    {
        StartCoroutine("CameraShake");
        switch (index)
        {
            case 1:
                //Instantiate(boxDropFX, currentCrate.transform.position, currentCrate.transform.rotation);
                break;
            case 2:
                //Instantiate(metalDropFX, currentCrate.transform.position, currentCrate.transform.rotation);
                metalDropFX.SetActive(true);
                break;
            case 3:
                //Instantiate(superDropFX, currentCrate.transform.position, currentCrate.transform.rotation);
                superDropFX.SetActive(true);
                superDropFX2.SetActive(true);
                break;
            case 4:
                //simpleDropFX.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OpenFXEvent()
    {
        superOpenFX.SetActive(true);
        superOpenFX.GetComponent<ParticleSystem>().Clear();
        superOpenFX.GetComponent<ParticleSystem>().Play();
        superOpenFX.GetComponent<ParticleSystem>().Emit(0);
    }
    
    IEnumerator CameraShake()
    {
        float x, y;
        float duration = Time.time + 0.2f;
        float magnitude = 0.0f;

        switch (index)
        {
            case 1:
                magnitude = 0.003f;
                break;
            case 2:
                magnitude = 0.01f;
                break;
            case 3:
                magnitude = 0.02f;
                break;
            case 4:
                magnitude = 0.007f;
                break;
            default:
                break;
        }

        while(Time.time < duration)
        {
            //Debug.Log("Shake!");
            x = Random.Range(-magnitude, magnitude);
            y = Random.Range(-magnitude, magnitude);

            crateCam.rect = new Rect(x, y, 1, 1);
            yield return null;
        }
        crateCam.rect = new Rect(0, 0, 1, 1);
    }

    public void BtnOnOpen()
    {
        if (!open)
        {
            open = true;
            StartCoroutine("Open");
        }
    }

    IEnumerator Open()
    {
        IdleFXEndEvent();
        //Item.SetActive(true);

        if (crateNum == 0)
        {
            currentCrate.GetComponent<Animator>().Play("Open", -1, 0f);
            switch (index)
            {
                case 1:
                    crateNum = 3;
                    yield return new WaitForSeconds(.5f);
                    break;
                case 2:
                    crateNum = 5;
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 3:
                    crateNum = 7;
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 4:
                    crateNum = 3;
                    yield return new WaitForSeconds(1.0f);
                    break;
                default:
                    break;
            }
            DisplayItem(1);
            crateOpenNum = 2;
            open = false;
        }
        else
        {
            if (crateOpenNum <= crateNum)
            {
                DisplayItem(crateOpenNum);
                crateOpenNum++;
                open = false;
            }
            else if (crateOpenNum > crateNum)
            {
                mainPanel.GetComponent<Animator>().Play("Idle");
                StartCoroutine("FadeOut");
                open = false;
            }
        }

        //상자 까고난 뒤에 세이브
        shopManager.CloudSaveItem();
        shopManager.UpdateInventoryText();
        shopManager.UpdateShopUI();
    }

    public void DisplayItem(int _level)
    {
        switch (_level)
        {
            case 1:
                if (index == 1) crate_gold = Random.Range(20, 50);
                else if (index == 2)
                {
                    crate_gold = Random.Range(500, 1000);
                }
                else if (index == 3)
                {
                    crate_gold = Random.Range(1500, 5000);
                }
                else if (index == 4) crate_gold = Random.Range(50, 100);
                currentItemImg.sprite = gold_sprite;
                currentItemTitle.text = crate_gold.ToString();

                int coin = PlayerPrefs.GetInt("Coin");
                coin += crate_gold;
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.Save();
                CloudVariables.SystemValues[0] = coin;
                PlayGamesScript.Instance.SaveData();
                break;
            case 2:
                if (index == 1) crate_gem = Random.Range(1, 2);
                else if (index == 2)
                {
                    crate_gem = Random.Range(5, 10);
                }
                else if (index == 3) crate_gem = Random.Range(10, 20);
                else if (index == 4) crate_gem = Random.Range(2, 5);
                currentItemImg.sprite = gem_sprite;
                currentItemTitle.text = crate_gem.ToString();

                int gem = PlayerPrefs.GetInt("Gem");
                gem += crate_gem;
                PlayerPrefs.SetInt("Gem", gem);
                PlayerPrefs.Save();
                CloudVariables.SystemValues[1] = gem;
                PlayGamesScript.Instance.SaveData();
                break;
            case 3: 
                if (index == 1) //Normal(98.89%)
                {
                    prob = Random.Range(0, 10000);
                    if (prob == 0) RandomLegendItem(); //0.01%
                    else if (prob < 10) RandomEpicItem(); //0.1%
                    else if (prob < 100) RandomRareItem(); //1%
                    else RandomNormalItem();
                }
                else if(index == 4)//Normal(70%)
                {
                    prob = Random.Range(0, 10000);
                    if (prob < 100) RandomLegendItem(); //1%
                    else if (prob < 2900) RandomEpicItem(); //29%
                    else RandomRareItem();
                }
                else //Normal(100%)
                {
                    RandomNormalItem();
                }
                break;
            //box crate
            case 4: //Normal(66%) & Rare Items(33%)
                prob = Random.Range(0, 3);
                if (prob == 0) RandomRareItem();
                else RandomNormalItem();
                break;
            case 5: //Rare(100%)
                RandomRareItem();
                break;
            //metal crate
            case 6: //Epic(100%)
                RandomEpicItem();
                break;
            case 7: //Epic(33%) & Legend(66%)
                prob = Random.Range(0, 3);
                if (prob == 0) RandomEpicItem();
                else RandomLegendItem();
                break;
            //super crate
            default:
                break;
        }
        mainPanel.GetComponent<Animator>().Play("Card", -1, 0f);
        //PanelBtnEnable();
    }

    public int GenerateRandomNumber(int min, int max)
    {
        int result = Random.Range(min, max);
        while (usedProb.Contains(result))
        {
            result = Random.Range(min, max);
        }
        usedProb.Add(result);
        return result;
    }

    public void RandomNormalItem()
    {
        normalCardFX.SetActive(false);
        int randItem = GenerateRandomNumber(0, 13);
        //Debug.Log(randItem);

        currentItemImg.sprite = equipment.normal_item_image[randItem];
        currentItemTitle.text = equipment.normal_item_name[randItem];
        //currentItemNum.color = new Color32(0, 255, 0, 255);
        FXClear();
        normalCardFX.SetActive(true);

        shopManager.NewItem(randItem, 0);
    }

    public void RandomRareItem()
    {
        rareCardFX.SetActive(false);
        int randItem = GenerateRandomNumber(0, 9);

        currentItemImg.sprite = equipment.rare_item_image[randItem];
        currentItemTitle.text = equipment.rare_item_name[randItem];
        //currentItemNum.color = new Color32(0, 85, 255, 255);
        FXClear();
        rareCardFX.SetActive(true);

        shopManager.NewItem(randItem, 1);
    }

    public void RandomEpicItem()
    {
        epicCardFX.SetActive(false);
        int randItem = GenerateRandomNumber(0, 5);

        currentItemImg.sprite = equipment.epic_item_image[randItem];
        currentItemTitle.text = equipment.epic_item_name[randItem];
        //currentItemNum.color = new Color32(200, 0, 255, 255);
        FXClear();
        epicCardFX.SetActive(true);

        shopManager.NewItem(randItem, 2);
    }

    public void RandomLegendItem()
    {
        legendCardFX.SetActive(false);
        int randItem = GenerateRandomNumber(0, 3);

        currentItemImg.sprite = equipment.legend_item_image[randItem];
        currentItemTitle.text = equipment.legend_item_name[randItem];
        //currentItemNum.color = new Color32(255, 225, 80, 255);
        FXClear();
        legendCardFX.SetActive(true);

        shopManager.NewItem(randItem, 3);
    }

    public void FXClear()
    {
        normalCardFX.GetComponent<ParticleSystem>().Clear();
        rareCardFX.GetComponent<ParticleSystem>().Clear();
        epicCardFX.GetComponent<ParticleSystem>().Clear();
        legendCardFX.GetComponent<ParticleSystem>().Clear();
    }

}
