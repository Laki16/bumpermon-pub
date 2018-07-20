using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAnimation : MonoBehaviour {

    public GameObject sledge;

    void GetSledge()
    {
        sledge.SetActive(true);
        //particle effect
    }

    void RemoveSledge()
    {
        sledge.SetActive(false);
        //particle effect
    }
    
}
