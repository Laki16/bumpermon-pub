using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxLink : MonoBehaviour {

	public void Open()
    {
        GetComponentInParent<CrateController>().OpenFXEvent();
    }
}
