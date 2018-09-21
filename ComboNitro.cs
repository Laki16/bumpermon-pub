using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboNitro : MonoBehaviour {
    private static int comboTextsArraySize = 15;
    public GameObject comboText;
    public GameObject[] comboTexts = new GameObject[comboTextsArraySize];
    public GameManager gameManager;

    public AnimationClip comboAnimatationClip;
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
    }

    public IEnumerator NitroCombo(int nitroCombo)
    {
        string combo = nitroCombo + " BREAK";
        int beforeIterator = iterator;
        nowIterator = beforeIterator + 1;
        nowIterator %= comboTextsArraySize;
        iterator++;
        iterator %= comboTextsArraySize;

        comboTexts[nowIterator].SetActive(true);
        comboTexts[nowIterator].GetComponent<Text>().text = (combo);
        yield return new WaitForSeconds(comboAnimationLength - 0.7f);
        comboTexts[beforeIterator].SetActive(false);
        yield return new WaitForSeconds(comboAnimationLength - 0.1f - (comboAnimationLength - 0.6f));
        comboTexts[nowIterator].SetActive(false);
        gameManager.brokenBoxes++;
    }
}
