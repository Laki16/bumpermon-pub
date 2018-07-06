using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour { 
    public PlayerController player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public Color32 originColor = new Color32((byte)0, (byte)0, (byte)0, (byte)255);

    public float timeIntervalColor = 0.5f;
    public float speedColor;

    private void Update()
    {
        scoreText.SetText(((int)player.transform.position.x + 20) + "");
        if (player.nowCombo > 0)
        {
            comboText.SetText(" X" + player.nowCombo);
        }
        else
        {
            comboText.SetText("");
        }
    }

    public IEnumerator FeverOn()
    {
        colorCoroutine = StartCoroutine(ColorChangeRoutine());
        while (player.useNitro)
        {
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(colorCoroutine);
        scoreText.faceColor = originColor;
    }

    Coroutine colorCoroutine;
    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        float timeIntervalColor = 0.5f;
        //Debug.Log("AAAA");
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        while (timer < timeIntervalColor)
        {
            //Debug.Log("HI");
            timer += Time.deltaTime * speedColor;
            //timer += .1f;
            scoreText.GetComponent<TextMeshProUGUI>().color = Color32.Lerp(scoreText.GetComponent<TextMeshProUGUI>().color, color, timer);
            yield return null;
        }
        // On finish recursively call the same routine
        colorCoroutine = StartCoroutine(ColorChangeRoutine());
    }
}
