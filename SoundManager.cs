using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject GameManager;

    public AudioSource buttonClick;
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource BGM;
    public static SoundManager Instance = null;

    //타이틀 드럼 끝났는지 체크
    public bool isLobbyEnd = false;
    [Header("AudioClips")]
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



    // Use this for initialization
    void Start () {
        StartCoroutine(BeginBGM());
	}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StopBGM()
    {
        StopAllCoroutines();
    }
    public IEnumerator GameOver()
    {
        BGM.Stop();
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
