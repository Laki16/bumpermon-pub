using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTest : MonoBehaviour {

    public GameObject a;
	// Use this for initialization
	void Start () {
        StartCoroutine(A());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator A()
    {
        while (true)
        {
            a.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            a.SetActive(false);
        }
    }
}
