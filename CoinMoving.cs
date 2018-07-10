using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoving : MonoBehaviour {

    public GameObject effect;
    public GameManager gameManager;
    public bool isRed;

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            effect.SetActive(true);
            effect.GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
            if (isRed) gameManager.coin += 2;
            else gameManager.coin++;
        }
	}
}
