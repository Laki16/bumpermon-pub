using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour {

    public GameObject playerController;
    public bool isNitro = false;
	// Use this for initialization
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("PlayerController");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UseNitro()
    {
        playerController.GetComponent<PlayerController>().UseNitro();
    }
     
}
