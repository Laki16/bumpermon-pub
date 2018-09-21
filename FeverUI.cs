using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverUI : MonoBehaviour
{

    public Text comboText;
    public Text feverText;
    public Image feverBarL;
    public Image feverBarR;

    [Header("Change Size")]
    public float timeIntervalSize = 0.5f;
    public float speedSize = 2.0f;
    [Header("Change Color")]
    public float timeIntervalColor = 0.5f;
    public float speedColor = 2.0f;
    [Header("Fever System")]
    public float feverTime = 0.0f;
    public bool isFever = false;
    public bool isCombo = true;
    private float feverWeight = 0.5f;

    private IEnumerator feverSwitch;
    private IEnumerator colorChangeSwitch;

    //저스트액션 할때마다 콤보증가(시간(2초쯤 동안 못하면 콤보 실패))
    //20콤보쯤하면 피버모드 발동

    private float velocity = 0.0f;
    private void Start()
    {
        ComboOff();
        FeverOff();

    }

    private void Update()
    {
        if(feverTime <=0 && isCombo)
        {
            ComboOff();
        }
        if (feverTime >= 10)
        {
            if (!isFever)
                StartCoroutine(FeverOn());
        }
        if (feverTime > 0)
        {
            feverTime -= feverWeight * Time.deltaTime;
        }
        feverBarL.fillAmount = feverTime / 10.0f;
        feverBarR.fillAmount = feverTime / 10.0f;
    }

    public void ComboOn()
    {
        isCombo = true;
        comboText.color = new Color32(255, 255, 255, 255);
    }

    public void ComboOff()
    {
        isCombo = false;
        comboText.color = new Color32(0, 0, 0, 0);
    }

    public void FeverExtend()
    {
        if(feverTime < 11.0f)
        feverTime += 1.0f;
        if(!isCombo && !isFever)
        {
            ComboOn();
        }
    }

    public IEnumerator FeverOn()
    {
        if (!isFever)
        {
            ComboOff();
            isFever = true;
            feverWeight = 1.0f;
            StartCoroutine(ColorChangeRoutine());
            while (feverTime > 0)
            {
                yield return new WaitForEndOfFrame();
            }
            StopAllCoroutines();
            FeverOff();
        }
    }

    public void FeverOff()
    {
        feverText.color = new Color32(0, 0, 0, 0);
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
            feverText.color = Color32.Lerp(feverText.color, color, timer);
            yield return null;
        }

        // On finish recursively call the same routine
        StartCoroutine(ColorChangeRoutine());
    }
    
    public IEnumerator SmoothHighlight()
    {
        float timer = 0.0f;

        while (timer < 1.0f)
        {
            timer += 1.5f * Time.deltaTime;
            float temp = 30*Mathf.Sin(Mathf.PI * (Mathf.Pow(timer - 1, 4)));
            comboText.GetComponent<RectTransform>().sizeDelta = new Vector2(temp, temp);
            //feverText.fontSize = 140 + 30.0f*Mathf.Sin(Mathf.PI * (Mathf.Pow(timer - 1, 4)));
            yield return null;
        }
    }
}
