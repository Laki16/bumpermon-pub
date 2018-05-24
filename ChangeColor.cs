using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    public float timeInterval = 2.0f;
    public float speed = 1.0f;

    void Start()
    {
        StartCoroutine(ColorChangeRoutine());
    }

    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        Vector2 position = new Vector3(Random.Range(-75, 75), Random.Range(400, 550), 0);

        while (timer < timeInterval)
        {
            timer += Time.deltaTime * speed;
            GetComponent<Text>().color = Color32.Lerp(GetComponent<Text>().color, color, timer);
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, position, timer);
            yield return null;
        }

        // On finish recursively call the same routine
        StartCoroutine(ColorChangeRoutine());
    }
    
}
