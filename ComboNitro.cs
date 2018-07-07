using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboNitro : MonoBehaviour {
    public PlayerController player;
    private static int comboTextsArraySize = 15;
    public GameObject comboText;
    public GameObject[] comboTexts = new GameObject[comboTextsArraySize];
    public GameManager gameManager;

    public AnimationClip comboAnimatationClip;
    //public AnimationClip comboNitroEndClip;
    //public float comboNitroEndLength;
    public float comboAnimationLength;
    public int iterator = 0;

    public int nowIterator;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < comboTextsArraySize; i++)
        {
            comboTexts[i] = Instantiate(comboText, transform.parent);
            comboTexts[i].SetActive(false);
        }
        comboAnimationLength = comboAnimatationClip.length;
        //comboNitroEndLength = comboNitroEndClip.length;
    }

    public IEnumerator NitroCombo(int nitroCombo)
    {
        string combo = nitroCombo + " BREAK";
        //string combo = nitroCombo + "";
        int beforeIterator = iterator;
        nowIterator = beforeIterator + 1;
        nowIterator %= comboTextsArraySize;
        iterator++;
        iterator %= comboTextsArraySize;


        comboTexts[nowIterator].SetActive(true);
        comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().SetText(combo);
        yield return new WaitForSeconds(comboAnimationLength - 0.7f);
        comboTexts[beforeIterator].SetActive(false);
        yield return new WaitForSeconds(comboAnimationLength - 0.1f - (comboAnimationLength - 0.6f));
        comboTexts[nowIterator].SetActive(false);
        gameManager.brokenBoxes++;
    }
    //public IEnumerator ComboNitroEnd()
    //{
    //    comboTexts[nowIterator].SetActive(true);
    //    comboTexts[nowIterator].GetComponent<Animator>().Play("ComboNitroEnd");
    //    yield return new WaitForSeconds(comboNitroEndLength);
    //    comboTexts[nowIterator].SetActive(false);
    //}
}
