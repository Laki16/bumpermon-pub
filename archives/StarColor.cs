using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarColor : MonoBehaviour {

    public float timeInterval = 2.0f;
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(ColorChangeRoutine());
	}

    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

        while (timer < timeInterval)
        {
            timer += Time.deltaTime * speed;
            GetComponent<Renderer>().material.color = Color32.Lerp(GetComponent<Renderer>().material.color, color, timer);
            yield return null;
        }

        // On finish recursively call the same routine
        StartCoroutine(ColorChangeRoutine());
    }
}
