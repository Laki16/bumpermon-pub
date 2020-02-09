using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAnimation : MonoBehaviour {

    public GameObject sledge;

    void GetSledge()
    {
        sledge.SetActive(true);
    }

    void RemoveSledge()
    {
        sledge.SetActive(false);
    }
    
}
