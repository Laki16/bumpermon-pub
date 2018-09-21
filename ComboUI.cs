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
    //니트로 때 ComboUp(원래콤보? 아니면 10정도?)
    float redColorChange;
    float colorChange;
    public IEnumerator ComboUp(int nowCombo)
    {
        string combo = "x" + (nowCombo + 1);
        int nowIterator = iterator;
        iterator++;
        iterator %= comboTextsArraySize;
        redColorChange = nowCombo;
        colorChange = Mathf.Pow(nowCombo, 1.5f);
        redColorChange = Mathf.Clamp(redColorChange, 0, 150);
        colorChange = Mathf.Clamp(colorChange, 0, 240);
        scoreCombo.GetComponent<TextMeshProUGUI>().faceColor = new Color32((byte)(255 - redColorChange), (byte)(255 - colorChange), (byte)(255 - colorChange), (byte)255);
        scoreCombo.GetComponent<Animator>().Play("Score_Combo", -1, 0);

        comboTexts[nowIterator].SetActive(true);
        comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().SetText(combo);

        redColorChange = nowCombo;
        colorChange = Mathf.Pow(nowCombo, 1.5f);
        redColorChange = Mathf.Clamp(redColorChange, 0, 150);
        colorChange = Mathf.Clamp(colorChange, 0, 240);

        comboTexts[nowIterator].GetComponent<TextMeshProUGUI>().faceColor = new Color32((byte)(255 - redColorChange), (byte)(255 - colorChange), (byte)(255 - colorChange), (byte)255);

        yield return new WaitForSeconds(comboAnimationLength - 0.1f);
        comboTexts[nowIterator].SetActive(false);
    }
}
