using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("UI")]
    public Text countdownText;
    public GameObject outGamePanel;
    public GameObject inGamePanel;
    //public Text meterText;

    [Header("System")]
    public GameObject blockController;
    public GameObject player;
    public GameObject enemy;
    public Camera camera;
    float meterBetBlock;
    int timeLeft = 3;
    Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        LoadSample();
    }

    void LoadSample(){
        
    }

	public void BtnOnStart()
	{
        outGamePanel.SetActive(false);
        inGamePanel.SetActive(true);
        blockController.SetActive(true);
        myAnimator = camera.GetComponent<Animator>();
        myAnimator.Play("StartMoving");
        StartCoroutine(GameStart());
	}

    IEnumerator GameStart(){
        for (int i = 3; i > 0; i--){
            countdownText.text = (i + "");
            if (i == 1)
            {
                player.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "";
        enemy.SetActive(true);
        meterBetBlock = player.transform.position.x;
        myAnimator.enabled = false;
    }
}
