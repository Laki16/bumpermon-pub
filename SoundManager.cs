using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource BGM;
    public AudioSource boxSFX;
    public AudioSource efxSource;
    public AudioSource itemSFX;
    public AudioSource buttonClick;
    public AudioSource crateSFX;
    public AudioSource uiSFX;

    //타이틀 드럼 끝났는지 체크
    public bool isLobbyEnd = false;
    public bool isCrate = false;
    [Header("BGM")]
    //타이틀 드럼(loop)
    public AudioClip lobbyBGM;
    public AudioClip InGameBGM;
    public AudioClip inGameBGM_4;
    public AudioClip gameOverEffect;
    public AudioClip gameOverBGM;
    public AudioClip continueBGM;

    [Header("SFX")]
    public AudioClip boostSFX;
    public AudioClip nitroSFX;
    public AudioClip boxBrokenSFX;
    public AudioClip hitSFX;
    //public AudioClip[] characterBoostSFX;

    [Header("ITEM")]
    public AudioClip itemLifeSFX;
    public AudioClip itemNitroSFX;
    public AudioClip itemShieldSFX;
    public AudioClip itemCoinSFX;
    public AudioClip coinSFX;

    [Header("Crate")]
    public AudioClip[] cardOpenSFX; //6
    public AudioClip[] crateDropSFX; //4

    [Header("UI")]
    public AudioClip[] lvUpSFX; //2
    public AudioClip countDownSFX;
    public AudioClip sellSFX;
    
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isLobbyEnd && !isCrate)
            {
                PlayClick();
            }
        }
        if (isCrate)
        {
            BGM.mute = true;
        }
        else
        {
            BGM.mute = false;
        }
    }

    public void PlayClick()
    {
        buttonClick.Play();
    }

    public void Mute(bool mute)
    {
        if(mute)
        {
            BGM.mute = true;
            efxSource.mute = true;
            boxSFX.mute = true;
            itemSFX.mute = true;
            buttonClick.mute = true;
        }
        else
        {
            BGM.mute = false;
            efxSource.mute = false;
            boxSFX.mute = false;
            itemSFX.mute = false;
            buttonClick.mute = false;
        }

    }

    public void PlayItemLife()
    {
        itemSFX.clip = itemLifeSFX;
        itemSFX.Play();
    }

    public void PlayItemNitro()
    {
        itemSFX.clip = itemNitroSFX;
        itemSFX.Play();
    }

    public void PlayItemShield()
    {
        itemSFX.clip = itemShieldSFX;
        itemSFX.Play();
    }

    public void PlayItemCoin()
    {
        itemSFX.clip = itemCoinSFX;
        itemSFX.Play();
    }

    public void PlayNormalCoin()
    {
        itemSFX.clip = coinSFX;
        itemSFX.Play();
    }

    public void PlayBox()
    {
        boxSFX.Play();
    }
    public void PlayBoost()
    {
        efxSource.clip = boostSFX;
        efxSource.Play();
    }
    public void PlayNitro()
    {
        efxSource.clip = nitroSFX;
        efxSource.Play();
    }
    public void StopBGM()
    {
        StopAllCoroutines();
    }
    public IEnumerator GameOver()
    {
        //BGM.Stop();
        BGM.clip = gameOverEffect;
        BGM.Play();
        yield return new WaitForSeconds(BGM.clip.length);
        BGM.clip = gameOverBGM;
        BGM.Play();
    }

    public IEnumerator Continue()
    {
        BGM.Stop();
        BGM.GetComponent<AudioSource>().loop = false;
        BGM.clip = continueBGM;
        BGM.Play();
        yield return new WaitForSeconds(BGM.clip.length);
        BGM.clip = inGameBGM_4;
        BGM.Play();
        BGM.GetComponent<AudioSource>().loop = true;
    }

    public IEnumerator BeginBGM()
    {
        isLobbyEnd = false;
        BGM.GetComponent<AudioSource>().loop = true;
        BGM.clip = lobbyBGM;
        BGM.Play();
        while (!isLobbyEnd)
        {
            yield return null;
        }
        BGM.GetComponent<AudioSource>().loop = false;
        BGM.clip = InGameBGM;
        BGM.Play();
        yield return new WaitForSeconds(BGM.clip.length);
        BGM.clip = inGameBGM_4;
        BGM.Play();
        BGM.GetComponent<AudioSource>().loop = true;
    }

    public void PlayLvUp(int type) 
    {
        //1:character, 2:equip
        uiSFX.clip = lvUpSFX[type - 1];
        uiSFX.Play();
    }

    public void PlayCrateDrop(int index)
    {
        //1:normal, 2:metal, 3:super, 4:simple
        crateSFX.clip = crateDropSFX[index - 1];
        crateSFX.Play();
    }

    public void PlayCardOpen(int index)
    {
        //1:coin, 2:gem, 3:normal, 4:rare, 5:epic, 6:legend
        crateSFX.clip = cardOpenSFX[index - 1];
        crateSFX.Play();
    }

    public void PlayHit()
    {
        //1:golem, 2:ghost, 4:santa, 5:skeleton
        efxSource.clip = hitSFX;
        efxSource.Play();
    }

    public void PlayCountDown()
    {
        //3 2 1 GO!
        uiSFX.clip = countDownSFX;
        uiSFX.Play();
    }

    public void PlayEquip()
    {
        //1:equip, 2:unequip, 3:sell
        uiSFX.clip = sellSFX;
        uiSFX.Play();
    }
}
