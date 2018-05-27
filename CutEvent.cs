using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEvent : MonoBehaviour {

    public GameObject golemDummy;
    public GameObject disapearBlock;
    public GameObject leftArm;
    public GameObject rightArm;
    bool isRotate = false;
    public GameObject golemPlayer;
    public GameObject IngamePanel;
    public GameObject tutorial;

    Vector3 targetPos = new Vector3(-8, 0, 0);
    Quaternion targetRot = new Quaternion(0, 0, 0, 0);

    private void Update()
    {
        if (isRotate)
        {
            golemDummy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0) * Time.deltaTime);
            golemDummy.transform.position = Vector3.Lerp(golemDummy.transform.position, targetPos, Time.deltaTime);
        }
    }

    public void Pop(){
        golemDummy.transform.position += new Vector3(0, 0.5f, 0);
        golemDummy.GetComponent<Animator>().SetTrigger("Pop");

        leftArm.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void Guard(){
        golemDummy.GetComponent<Animator>().SetTrigger("Guard");
    }

    public void Punch()
    {
        golemDummy.GetComponent<Animator>().SetTrigger("Punch");
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

    public void Angry()
    {
        rightArm.GetComponent<Animator>().SetTrigger("Angry");
    }

    public void TutorialStart()
    {
        golemDummy.SetActive(false);
        golemPlayer.SetActive(true);
        IngamePanel.SetActive(true);
        tutorial.SetActive(true);
        //StartCoroutine(tutorial.GetComponent<TutorialController>().Tutorial());
        //Debug.Log("tutorial coroutine start.");
    }
}
