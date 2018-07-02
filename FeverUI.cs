using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverUI : MonoBehaviour {

    public TextMeshProUGUI feverText;
    public Image feverBarL;
    public Image feverBarR;

    [Header("Change Color")]
    public float timeInterval = 0.5f;
    public float speed = 2.0f;
    [Header("Fever System")]
    public float feverTime = 10.0f;
    public bool isFever = false;

    저스트액션 할때마다 콤보증가(시간(2초쯤 동안 못하면 콤보 실패))
    20콤보쯤하면 피버모드 발동
    private void Update()
    {
        feverBarL.fillAmount = feverTime / 10.0f;
        feverBarR.fillAmount = feverTime / 10.0f;
    }

    public void FeverExtend()
    {
        if (isFever)
        {
            feverTime += 0.5f;
            Mathf.Clamp(feverTime, 0, 10);
        }
    }

    public IEnumerator FeverOn()
    {
        if(!isFever)
        {
            isFever = true;
            StartCoroutine(ColorChangeRoutine());
            feverTime = 10.0f;
            feverBarL.fillAmount = 1;
            feverBarR.fillAmount = 1;
            while (feverTime < 0)
            {
                feverTime -= Time.deltaTime;
                yield return null;
            }
            StopAllCoroutines();
            FeverOff();
        }
    }

    public void FeverOff()
    {
        feverText.faceColor = new Color32(0, 0, 0, 0);
        isFever = false;
    }

    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

        while (timer < timeInterval)
        {
            timer += Time.deltaTime * speed;
            feverText.faceColor = Color32.Lerp(feverText.faceColor, color, timer);
            yield return null;
        }

        // On finish recursively call the same routine
        StartCoroutine(ColorChangeRoutine());
    }

}
