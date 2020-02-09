using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("System")]
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

    private void OnEnable()
    {
        switch (myType)
        {
            case ItemType.MINE:
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
        switch (myType)
        {
            case ItemType.MINE:
                myEffect = Instantiate(mineBoom, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
            case ItemType.LIFE:
                soundManager.GetComponent<SoundManager>().PlayItemLife();
                myEffect = Instantiate(getLife, transform.position, transform.rotation);
                StartCoroutine(other.GetComponent<PlayerController>().LifeUp(30.0f));
                break;
            case ItemType.COIN:
                soundManager.GetComponent<SoundManager>().PlayItemCoin();
                myEffect = Instantiate(getCoin, transform.position, transform.rotation);
                gameManager.GetComponent<GameManager>().gem++;
                Destroy(gameObject);
                break;
            case ItemType.NITRO:
                soundManager.GetComponent<SoundManager>().PlayItemNitro();
                StartCoroutine(other.GetComponent<PlayerController>().GetNitro());
                Destroy(gameObject);
                break;
            case ItemType.SHIELD:
                soundManager.GetComponent<SoundManager>().PlayItemShield();
                myEffect = Instantiate(getShield, transform.position, transform.rotation);
                other.GetComponent<PlayerController>().GetShield();
                Destroy(gameObject);
                break;
        }
    }
    
}
