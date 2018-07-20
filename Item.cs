using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("System")]
    //public GameObject player;
    public GameObject gameManager;

    [Header("Type")]
    public ItemType myType;
    public enum ItemType
    {
        MINE,
        LIFE,
        COIN,
        NITRO,
        SHIELD
    }
    [Header("SFX")]
    public GameObject soundManager;

    [Header("FX")]
    GameObject myEffect;
    public GameObject mineBoom;
    public GameObject mineFX;
    public GameObject getLife;
    public GameObject lifeFX;
    public GameObject getCoin;
    public GameObject coinFX;
    public GameObject getNitro;
    public GameObject nitroFX;
    public GameObject getShield;
    public GameObject shieldFX;

    void Start()
    {
        //soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        Destroy(gameObject, 30.0f);
    }
    //void Update()
    //{
    //    if(player.transform.position.x > transform.position.x + 20.0f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnEnable()
    {
        switch (myType)
        {
            case ItemType.MINE:
                //myEffect = Instantiate(mineFX, transform.position, transform.rotation);
                break;
            case ItemType.LIFE:
                myEffect = Instantiate(lifeFX, transform.position, transform.rotation);
                break;
            case ItemType.COIN:
                myEffect = Instantiate(coinFX, transform.position, transform.rotation);
                break;
            case ItemType.NITRO:
                myEffect = Instantiate(nitroFX, transform.position, transform.rotation);
                break;
            case ItemType.SHIELD:
                myEffect = Instantiate(shieldFX, transform.position, transform.rotation);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(myEffect);
        //Debug.Log("ouch");
        switch (myType)
        {
            case ItemType.MINE:
                myEffect = Instantiate(mineBoom, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
            case ItemType.LIFE:
                soundManager.GetComponent<SoundManager>().PlayItemLife();
                myEffect = Instantiate(getLife, transform.position, transform.rotation);
                //if (other.GetComponent<PlayerController>().live < 3)
                //{
                //    other.GetComponent<PlayerController>().live++;
                //    gameManager.GetComponent<GameManager>().LiveUp();
                //}
                //float lifeMux = gameManager.GetComponent<GameManager>().player.GetComponent<Accessory>().lifeMux;
                StartCoroutine(other.GetComponent<PlayerController>().LifeUp(30.0f));
                //GetLife();
                break;
            case ItemType.COIN:
                soundManager.GetComponent<SoundManager>().PlayItemCoin();
                myEffect = Instantiate(getCoin, transform.position, transform.rotation);
                gameManager.GetComponent<GameManager>().gem++;
                Destroy(gameObject);
                //GetCoin();
                break;
            case ItemType.NITRO:
                soundManager.GetComponent<SoundManager>().PlayItemNitro();
                StartCoroutine(other.GetComponent<PlayerController>().GetNitro());
                Destroy(gameObject);
                //UseNitro();
                break;
            case ItemType.SHIELD:
                soundManager.GetComponent<SoundManager>().PlayItemShield();
                myEffect = Instantiate(getShield, transform.position, transform.rotation);
                other.GetComponent<PlayerController>().GetShield();
                Destroy(gameObject);
                //GetShield();
                break;
        }
    }

    //private IEnumerator Mine()
    //{
    //    //LoseLife();
    //    yield return null;
    //}

    //private IEnumerator Life()
    //{
    //    yield return null;
    //}

    //private IEnumerator Coin()
    //{
    //    yield return null;
    //}

    //private IEnumerator Nitro()
    //{
    //    yield return null;
    //}

    ////private void LoseLife()
    ////{

    ////}

    //private void GetLife()
    //{
    //}

    //private void GetCoin()
    //{ 
    //}

    //private void UseNitro()
    //{ 
    //}

    //private void GetShield()
    //{
    //}
}
