using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverUI : MonoBehaviour {

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI feverText;
    public Image feverBarL;
    public Image feverBarR;

    [Header("Change Size")]
    public float timeIntervalSize = 0.5f;
    public float speedSize = 2.0f;
    [Header("Change Color")]
    public float timeIntervalColor = 0.5f;
    public float speedColor = 2.0f;
    [Header("Fever System")]
    public float feverTime = 10.0f;
    public bool isFever = false;
    // Fever mode : 1.0f, non Fever mode : 0.5f
    private float feverWeight = 0.5f;

    private IEnumerator feverSwitch;
    private IEnumerator colorChangeSwitch;

    //저스트액션 할때마다 콤보증가(시간(2초쯤 동안 못하면 콤보 실패))
    //20콤보쯤하면 피버모드 발동

    
    private void Start()
    {
        feverSwitch = FeverOn();
        colorChangeSwitch = ColorChangeRoutine();
    }

    private void Update()
    {
        if(feverTime >= 10)
        {
            StartCoroutine(feverSwitch);
        }
        if(feverTime > 0 && isFever)
        {
            feverTime -= feverWeight * Time.deltaTime;
            Mathf.Clamp(feverTime, 0, 10);
        }
        feverBarL.fillAmount = feverTime / 10.0f;
        feverBarR.fillAmount = feverTime / 10.0f;
    }

    //public void ComboUp()
    //{

    //}

    public void FeverExtend()
    {
        //if (isFever)
        //{
            feverTime += 0.5f;
            Mathf.Clamp(feverTime, 0, 10);
        //}
    }

    public IEnumerator FeverOn()
    {
        if(!isFever)
        {
            isFever = true;
            feverWeight = 1.0f;
            StartCoroutine(colorChangeSwitch);
            //feverTime = 10.0f;
            feverBarL.fillAmount = 1;
            feverBarR.fillAmount = 1;
            while (feverTime < 0)
            {
                feverTime -= Time.deltaTime;
                yield return null;
            }
            StopCoroutine(colorChangeSwitch);
            FeverOff();
        }
    }

    public void FeverOff()
    {
        feverText.faceColor = new Color32(0, 0, 0, 0);
        feverWeight = 0.5f;
        isFever = false;
    }

    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

        while (timer < timeIntervalColor)
        {
            timer += Time.deltaTime * speedColor;
            feverText.faceColor = Color32.Lerp(feverText.faceColor, color, timer);
            yield return null;
        }

        // On finish recursively call the same routine
        StartCoroutine(ColorChangeRoutine());
    }

    private IEnumerator SizeChangeRoutine()
    {
        float timer = 0.0f;
        float bumpSize = 170;

        while (timer < timeIntervalSize)
        {
            comboText.fontSize = Mathf.Lerp(140, )                                      
        }
    }
}
