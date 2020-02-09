﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour { 
    public PlayerController player;
    public Text scoreText;
    public Text comboText;
    public GameManager gameManager;
    public Color32 originColor;

    public float speedColor = 2.0f;
    public int score;
    public float playerPos;

    private void Start()
    {
        player = gameManager.player.GetComponent<PlayerController>();
        score = 0;
        playerPos = player.transform.position.x;
    }
    private void Update()
    {
       
        if (player.transform.position.x > playerPos + 1)
        {
            score += (player.nowCombo + 1);
            playerPos = player.transform.position.x;
            scoreText.text = (score + "");
        }
        if (player.nowCombo > 0)
        {
            //comboText.GetComponent<TextMeshProUGUI>().margin = new Vector4(
            //    190 + scoreText.GetComponent<TextMeshProUGUI>().bounds.extents.x, 0, 0, 0);
            //comboText.ForceMeshUpdate();
            comboText.text = (" x" + (player.nowCombo + 1));
        }
        else
        {
            comboText.text = string.Empty;
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
        scoreText.color = originColor;
    }

    Coroutine colorCoroutine;
    private IEnumerator ColorChangeRoutine()
    {
        float timer = 0.0f;
        float timeIntervalColor = 0.5f;
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        while (timer < timeIntervalColor)
        {
            timer += Time.deltaTime * speedColor;
            scoreText.color = Color32.Lerp(scoreText.color, color, timer);
            yield return null;
        }
        // On finish recursively call the same routine
        colorCoroutine = StartCoroutine(ColorChangeRoutine());
    }
}