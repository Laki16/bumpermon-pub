using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource BGM;
    public AudioSource boxSFX;
    public AudioSource efxSource;
    public AudioSource itemSFX;
    public AudioSource buttonClick;

    //public static SoundManager Instance = null;

    //타이틀 드럼 끝났는지 체크
    public bool isLobbyEnd = false;
    [Header("BGM")]
    //타이틀 드럼(loop)
    public AudioClip lobbyBGM;
    public AudioClip InGameBGM;
    public AudioClip inGameBGM_1;
    public AudioClip inGameBGM_2;
    public AudioClip inGameBGM_3;
    public AudioClip inGameBGM_4;
    public AudioClip gameOverEffect;
    public AudioClip gameOverBGM;
    public AudioClip continueBGM;

    [Header("SFX")]
    public AudioClip boostSFX;
    public AudioClip nitroSFX;
    public AudioClip boxBrokenSFX;

    [Header("ITEM")]
    public AudioClip itemLifeSFX;
    public AudioClip itemNitroSFX;
    public AudioClip itemShieldSFX;
    public AudioClip itemCoinSFX;


    // Use this for initialization
    void Start () {
        //StartCoroutine(BeginBGM());
	}

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(gameObject);
    //}

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isLobbyEnd)
            {
                PlayClick();
            }
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

        //BGM.Stop();
        //BGM.GetComponent<AudioSource>().loop = false;
        //BGM.clip = inGameBGM_3;
        //BGM.Play();
        //yield return new WaitForSeconds(BGM.clip.length);
        //BGM.Play();
        //yield return new WaitForSeconds(BGM.clip.length);
        //BGM.clip = inGameBGM_4;
        //BGM.GetComponent<AudioSource>().loop = true;
        //BGM.Play();
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
        //yield return new WaitForSeconds(BGM.clip.length);
        BGM.GetComponent<AudioSource>().loop = false;
        BGM.clip = InGameBGM;
        BGM.Play();
        yield return new WaitForSeconds(BGM.clip.length);
        BGM.clip = inGameBGM_4;
        BGM.Play();
        BGM.GetComponent<AudioSource>().loop = true;
        //    BGM.clip = lobbyBGM;
        //    BGM.Play();
        //    while(!isLobbyEnd)
        //    {
        //        yield return null;
        //    }
        //    //yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.GetComponent<AudioSource>().loop = false;
        //    BGM.clip = inGameBGM_1;
        //    BGM.Play();
        //    yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.clip = inGameBGM_2;
        //    BGM.Play();
        //    yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.Play();
        //    yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.clip = inGameBGM_3;
        //    BGM.Play();
        //    yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.Play();
        //    yield return new WaitForSeconds(BGM.clip.length);
        //    BGM.clip = inGameBGM_4;
        //    BGM.GetComponent<AudioSource>().loop = true;
        //    BGM.Play();
    }
}
