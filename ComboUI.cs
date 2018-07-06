using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUI : MonoBehaviour {
    private static int comboTextsArraySize = 3;
    public GameObject comboText;
    public GameObject[] comboTexts = new GameObject[comboTextsArraySize];
    public GameObject scoreCombo;
    //public Animation a;

    public AnimationClip comboAnimatationClip;
    public float comboAnimationLength;
    public int iterator = 0;

    public void Start()
    {
        for(int i = 0; i< comboTextsArraySize; i ++)
        {
            comboTexts[i] = Instantiate(comboText, transform.parent);
            comboTexts[i].SetActive(false);
        }
        comboAnimationLength = comboAnimatationClip.length;
    }

    public IEnumerator ComboUp(int nowCombo)
    {
        string combo = nowCombo + "X";
        int nowIterator = iterator;
        iterator++;
        iterator %= comboTextsArraySize;
        //scoreCombo.GetComponent<Animator>().SetTrigger("Combo");
        scoreCombo.GetComponent<Animator>().Play("Score_Combo", -1, 0);

        comboTexts[nowIterator].SetActive(true);
        comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().SetText(combo);
        switch (nowCombo / 2)
        {
            case 1:
                comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().faceColor = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                break;
            case 2:
                comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().faceColor = new Color32((byte)100, (byte)100, (byte)255, (byte)255);
                break;
            case 3:
                comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().faceColor = new Color32((byte)255, (byte)100, (byte)255, (byte)255);
                break;
        }
        yield return new WaitForSeconds(comboAnimationLength - 0.1f);
        comboTexts[nowIterator].SetActive(false);
    }
}
