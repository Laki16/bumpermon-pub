using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour {

    public ShopManager shopManager;
    public GameObject chkPanel;
    public Text chkText;
    public int chkIndex;
    private int index;

    public void BtnOnBuyCoin(int _index)
    {
        chkIndex = -1;
        StartCoroutine("Check");
        index = _index;
        switch (_index)
        {
            case 1: chkText.text = "5000코인을 구매하시겠습니까?"; break;
            case 2: chkText.text = "20000코인을 구매하시겠습니까?"; break;
            case 3: chkText.text = "100000코인을 구매하시겠습니까?"; break;
            case 4: chkText.text = "500000코인을 구매하시겠습니까?"; break;
            default: Debug.Log("Invalid request"); break;
        }
    }

    IEnumerator Check()
    {
        int gem = PlayerPrefs.GetInt("Gem");
        int coin = PlayerPrefs.GetInt("Coin");
        chkPanel.SetActive(true);

        while (chkIndex == -1)
        {
            yield return null;
        }
        if(chkIndex == 0)
        {
            Debug.Log("Cancel");
            chkPanel.SetActive(false);
        }
        else if(chkIndex == 1)
        {
            switch (index)
            {
                case 1: //29gems to 5000coins
                    if (gem >= 29)
                    {
                        gem -= 29;
                        coin += 5000;
                    }
                    else Debug.Log("need more gems!");
                    break;
                case 2: //99gems to 20000coins
                    if (gem >= 99)
                    {
                        gem -= 99;
                        coin += 20000;
                    }
                    else Debug.Log("need more gems!");
                    break;
                case 3: //249gems to 100000coins
                    if (gem >= 249)
                    {
                        gem -= 249;
                        coin += 100000;
                    }
                    else Debug.Log("need more gems!");
                    break;
                case 4: //1499gems to 500000coins
                    if (gem >= 1499)
                    {
                        gem -= 1499;
                        coin += 450000;
                    }
                    else Debug.Log("need more gems!");
                    break;
                default:
                    break;
            }
            chkPanel.SetActive(false);

            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("Gem", gem);
            PlayerPrefs.Save();

            CloudVariables.SystemValues[0] = coin;
            CloudVariables.SystemValues[1] = gem;
            PlayGamesScript.Instance.SaveData();

            shopManager.UpdateShopUI();
        }
    }

    //public void BtnOnBuyGem(int _index)
    //{
    //    chkIndex = 0;
    //    int gem = PlayerPrefs.GetInt("Gem");
    //    index = _index;
    //    switch (_index)
    //    {
    //        case 1: //0.99$ to 40gems

    //            break;
    //        case 2: //2.99$ to 300gems

    //            break;
    //        case 3: //4.99$ to 1100gems

    //            break;
    //        case 4: //9.99$ to 10000gems

    //            break;
    //        default: Debug.Log("Invalid request"); break;
    //        //case 5: chkText.text = "40젬을 구매하시겠습니까?"; break;
    //        //case 6: chkText.text = "300젬을 구매하시겠습니까?"; break;
    //        //case 7: chkText.text = "1100젬을 구매하시겠습니까?"; break;
    //        //case 8: chkText.text = "10000젬을 구매하시겠습니까?"; break;
    //    }
    //    PlayerPrefs.SetInt("Gem", gem);
    //    CloudVariables.SystemValues[1] = gem;
    //    PlayGamesScript.Instance.SaveData();

    //    shopManager.UpdateShopUI();
    //}
}
