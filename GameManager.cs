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
    int timeLeft = 3;

	// Use this for initialization
	void Start () {
	}
	
	void Update () {
        meterText.text = ((int)player.transform.position.x + 9 + "m");
	}
    
}
