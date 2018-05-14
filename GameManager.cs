using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Text")]
    public Text countdownText;
    public Text meterText;

    [Header("System")]
    public GameObject player;
    float meterBetBlock;
    int timeLeft = 3;

	// Use this for initialization
	void Start () {
        meterBetBlock = player.transform.position.x;
	}
	
	void Update () {
        meterText.text = ((int)player.transform.position.x - meterBetBlock + "m");
	}
    
}
