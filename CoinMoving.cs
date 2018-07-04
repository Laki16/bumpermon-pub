using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoving : MonoBehaviour {

    public GameObject effect;

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            effect.SetActive(true);
            effect.GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
        }
	}
}
