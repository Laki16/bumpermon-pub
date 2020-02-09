﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAlpha : MonoBehaviour {
    
    public GameObject toonGhost;
    public Material normalShader;
    public Material fadeShader;
    bool isFade;
    public float rollLenght = 5.0f;
    public GameObject player;

	private void Start()
	{
        isFade = false;
	}

    public void RollStart(){
        player.GetComponent<PlayerController>().ghostMode = true;
        StopAllCoroutines();
        StartCoroutine("Rolling");
    }

    IEnumerator Rolling(){
        yield return new WaitForSeconds(rollLenght);
        player.GetComponent<PlayerController>().ghostMode = false;
        ChangeAlpha();
    }

	public void ChangeAlpha(){
        SkinnedMeshRenderer smr = toonGhost.GetComponent<SkinnedMeshRenderer>();
        if(isFade){
            smr.material = normalShader;
            isFade = false;
        }else{
            smr.material = fadeShader;
            isFade = true;
        }
    }
}
