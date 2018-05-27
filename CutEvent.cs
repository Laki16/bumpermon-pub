using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEvent : MonoBehaviour {

    public GameObject golemDummy;
    public GameObject disapearBlock;
    bool isRotate = false;

	private void Update()
	{
        if(isRotate){
            golemDummy.transform.Rotate(new Vector3(0, -1, 0) * 1.5f);
            golemDummy.transform.position += new Vector3(.1f, 0, 0);
        }
	}

	public void Pop(){
        golemDummy.transform.position += new Vector3(0, 0.5f, 0);
        golemDummy.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void Guard(){
        golemDummy.GetComponent<Animator>().SetTrigger("Guard");
    }

    public void Jump()
    {
        isRotate = true;
        golemDummy.GetComponent<Animator>().SetTrigger("Jump");
    }

    public void Run()
    {
        golemDummy.GetComponent<Animator>().SetTrigger("Run");
    }

    public void DeleteBlock(){
        disapearBlock.SetActive(false);
    }
}
