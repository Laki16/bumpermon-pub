using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animLink : MonoBehaviour {

    public void NitroEvent()
    {
        GetComponentInParent<PlayerController>().NitroAnimEvent();
    }
}
