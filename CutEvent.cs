using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEvent : MonoBehaviour {

    public GameObject golemDummy;

    public void Pop(){
        golemDummy.transform.position += new Vector3(0, 0.5f, 0);
        golemDummy.GetComponent<Animator>().SetTrigger("Pop");
    }
}
