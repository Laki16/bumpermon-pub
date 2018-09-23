using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoving : MonoBehaviour {

    public GameObject effect;
    public GameManager gameManager;
    public SoundManager soundManager;
    public bool isRed;

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            effect.SetActive(true);
            effect.GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
            if (isRed) //뭔가 잘못되었지만 여튼 이것이 노란 일반코인
            {
                gameManager.coin ++;
            }
            else //그리고 이것이 빨간 코인임
            {
                gameManager.coin += 2;
            }
            soundManager.PlayNormalCoin();
        }
	}
}
