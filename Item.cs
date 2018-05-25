using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("System")]
    public GameObject player;
    public GameObject gameManager;

    [Header("Type")]
    public ItemType myType;
    public enum ItemType
    {
        MINE,
        LIFE,
        COIN,
        NITRO,
        SHIELD;
    }

    [Header("FX")]
    GameObject myEffect;
    public GameObject mineBoom;
    public GameObject mineFX;
    public GameObject getLife;
    public GameObject lifeFX;
    public GameObject getCoin;
    public GameObject coinFX;
    public GameObject nitroFX;
    public GameObject getShield;
    public GameObject shieldFX;


    private void OnEnable()
    {
        switch (myType)
        {
            case ItemType.MINE:
                myEffect = Instantiate(mineFX, transform.position, transform.rotation);
                break;
            case ItemType.LIFE:
                myEffect = Instantiate(mineFX, transform.position, transform.rotation);
                break;
            case ItemType.COIN:
                myEffect = Instantiate(mineFX, transform.position, transform.rotation);
                break;
            case ItemType.NITRO:
                myEffect = Instantiate(mineFX, transform.position, transform.rotation);
                break;
            case ItemType.SHIELD:
                myEffect = Instantiate(shieldFX, transform.position, transform.rotation);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (myType)
        {
            case ItemType.MINE:
                myEffect = Instantiate(mineBoom, transform.position, transform.rotation);
                break;
            case ItemType.LIFE:
                myEffect = Instantiate(getLife, transform.position, transform.rotation);
                GetLife();
                break;
            case ItemType.COIN:
                myEffect = Instantiate(getCoin, transform.position, transform.rotation);
                GetCoin();
                break;
            case ItemType.NITRO:
                UseNitro();
                break;
            case ItemType.SHIELD:
                myEffect = Instantiate(getShield, transform.position, transform.rotation);
                GetShield();
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

    private void GetLife()
    {
        if (player.GetComponent<PlayerController>().live < 3)
        {
            player.GetComponent<PlayerController>().live++;
        }
    }

    private void GetCoin()
    {
        gameManager.GetComponent<GameManager>().coin++;
    }

    private void UseNitro()
    {
        player.GetComponent<PlayerController>().isNitro = true;
        player.GetComponent<PlayerController>().UseNitro();
    }

    private void GetShield()
    {
        player.GetComponent<PlayerController>().isShield = true;
    }
}
