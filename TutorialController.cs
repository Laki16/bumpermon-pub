using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    //public GameObject nonClickPanel;
    public GameObject panel1;
    public GameObject player;
    public Image pointer;

    bool isStop = false;
    bool t1 = false;
    bool t2 = false;
    bool t3 = false;
    bool t4 = false;
    bool t5 = false;
    bool t6 = false;
    bool isEnd = false;

    float holdCount = 0;
    float tutorialCount = 0;
    float boostTime = 500;

    private Vector2 initialPos;
    private bool isMouseDown = false;

    public Text tutorialText;
    public GameObject tutorialBg;
    
    void OnEnable()
    {
        //nonClickPanel.SetActive(true);
        panel1.SetActive(false);
        tutorialCount = Time.time;
        StartCoroutine(Tutorial());

        player.GetComponent<PlayerController>().tutorial = true;
    }

    public IEnumerator Tutorial()
    {
        //LEFT
        yield return new WaitForSeconds(5.0f);
        isStop = true;
        pointer.gameObject.SetActive(true);
        tutorialBg.gameObject.SetActive(true);
        tutorialText.text = "화면을 슬라이드해\n 박스를 피한다.";
        while (!t1)
        {
            yield return null;
        }
        isStop = false;
        player.GetComponent<PlayerController>().speed = 20.0f;
        pointer.gameObject.SetActive(false);
        tutorialBg.gameObject.SetActive(false);


        //RIGHT
        yield return new WaitForSeconds(5.0f);
        isStop = true;
        pointer.gameObject.SetActive(true);
        pointer.GetComponent<Animator>().Play("Right");
        while (!t2)
        {
            yield return null;
        }
        isStop = false;
        player.GetComponent<PlayerController>().speed = 20.0f;
        pointer.gameObject.SetActive(false);


        //RIGHT_ACTION
        yield return new WaitForSeconds(5.0f);
        
        tutorialBg.gameObject.SetActive(true);
        tutorialText.text = "아슬아슬하게 피하면\n 순간부스터가 발동된다.";
        //pointer.gameObject.SetActive(true);
        //pointer.GetComponent<Animator>().Play("Right");

        yield return new WaitForSeconds(3.0f);
        tutorialBg.gameObject.SetActive(false);
        

        //HOLD
        yield return new WaitForSeconds(5.0f);
        tutorialBg.gameObject.SetActive(true);
        tutorialText.text = "화면을 누르고 있으면\n 속도가 빨라진다.";
        pointer.gameObject.SetActive(true);
        pointer.GetComponent<Animator>().Play("Hold");
        while (!t3)
        {
            yield return null;
        }
        pointer.gameObject.SetActive(false);

        tutorialText.text = "속도가 어느정도 모이면\n 기어가 올라간다.";
        yield return new WaitForSeconds(4.0f);


        tutorialText.text = "손을 떼고 있으면\n 기어와 속도가 떨어진다.";
        yield return new WaitForSeconds(3.0f);
        tutorialBg.gameObject.SetActive(false);


        //BOOST
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().nitro = 100;
        //isStop = true;
        //Debug.Log("Stop");
        tutorialBg.gameObject.SetActive(true);
        tutorialText.text = "소울이 다 차면\n 부스터를 사용할 수 있다.";
        pointer.gameObject.SetActive(true);
        pointer.GetComponent<Animator>().Play("Down");

        isEnd = true;
    }

    void Update()
    {
        if(isStop)
            player.GetComponent<PlayerController>().speed = 0.0f;

        //---------------mouse control-------------- -
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            Calculate(Input.mousePosition);
        }
        //---------------------------------------------

        if (Input.GetMouseButton(0))
        {
            holdCount += Time.deltaTime;
            if (holdCount >= 5.0f)
            {
                t3 = true;
            }
        }

        if(tutorialCount + 100.0f <= Time.time)
        {
            EndTutorial();
        }
        
        if(boostTime + 4.5f <= Time.time)
        {
            EndTutorial();
        }

    }

    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);

        if (disX > 0 || disY > 0)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    if (transform.position.z < 0.9f)
                    {
                        //Debug.Log("Left");
                        if (t1) t4 = true;
                        t1 = true;
                    }
                }
                else
                {
                    if (transform.position.z > -0.9f)
                    {
                        //Debug.Log("Right");
                        if (t2) t5 = true;
                        t2 = true;
                    }
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    //Debug.Log("Down");
                    if (isEnd)
                    {
                        boostTime = Time.time;
                        pointer.gameObject.SetActive(false);
                        tutorialBg.gameObject.SetActive(false);
                    }
                }
            }
        }
    }


    void EndTutorial()
    {
        Debug.Log("tutorial end");

        SceneManager.LoadScene(0);
    }

}
