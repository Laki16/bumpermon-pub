using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCalculate : MonoBehaviour {

    public UnityAdsHelper unityAdsHelper;

    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;

    public GameObject price1;
    public GameObject price2;
    public GameObject price3;

    public GameObject timer1;
    public GameObject timer2;
    public GameObject timer3;

    double time1sec;
    double time2sec;
    double time3sec;

    DateTime currentTime;
    DateTime pastTime1;
    DateTime pastTime2;
    DateTime pastTime3;
    TimeSpan timeCal1;
    TimeSpan timeCal2;
    TimeSpan timeCal3;

    private void Awake()
    {
        UpdateTime();
    }

    void LateUpdate()
    {
        currentTime = DateTime.Now;

        //timer1
        pastTime1 = Convert.ToDateTime(PlayerPrefs.GetString("Time1"));
        timeCal1 = currentTime - pastTime1;
        time1sec = timeCal1.TotalSeconds;
        if (time1sec < 10800)
        {
            timer1.SetActive(true);
            btn1.GetComponent<Button>().interactable = false;
            price1.SetActive(false);

            timer1.GetComponent<Text>().text
                = string.Format("{0:00}:{1:00}:{2:00}", 2-(int)time1sec/3600, 59-((int)time1sec/60)%60, 59-((int)time1sec%60));
        }
        else
        {
            timer1.SetActive(false);
            btn1.GetComponent<Button>().interactable = true;
            price1.SetActive(true);
        }

        //timer2
        pastTime2 = Convert.ToDateTime(PlayerPrefs.GetString("Time2"));
        timeCal2 = currentTime - pastTime2;
        time2sec = timeCal2.TotalSeconds;
        if (time2sec < 18000)
        {
            timer2.SetActive(true);
            btn2.GetComponent<Button>().interactable = false;
            price2.SetActive(false);

            timer2.GetComponent<Text>().text
                = string.Format("{0:00}:{1:00}:{2:00}", 4-(int)time2sec/3600, 59-((int)time2sec/60)%60, 59-((int)time2sec%60));
        }
        else
        {
            timer2.SetActive(false);
            btn2.GetComponent<Button>().interactable = true;
            price2.SetActive(true);
        }

        //timer3
        pastTime3 = Convert.ToDateTime(PlayerPrefs.GetString("Time3"));
        timeCal3 = currentTime - pastTime3;
        time3sec = timeCal3.TotalSeconds;
        if(time3sec < 28800)
        {
            timer3.SetActive(true);
            btn3.GetComponent<Button>().interactable = false;
            price3.SetActive(false);

            timer3.GetComponent<Text>().text
                = string.Format("{0:00}:{1:00}:{2:00}", 7-(int)time3sec/3600, 59-((int)time3sec/60)%60, 59-((int)time3sec%60));
        }
        else
        {
            timer3.SetActive(false);
            btn3.GetComponent<Button>().interactable = true;
            price3.SetActive(true);
        }
    }

    public void UpdateTime()
    {
        if (!PlayerPrefs.HasKey("Time1"))
        {
            string temp = "1/1/2018 0:00:00 PM";

            PlayerPrefs.SetString("Time1", temp);
            PlayerPrefs.SetString("Time2", temp);
            PlayerPrefs.SetString("Time3", temp);
        }
    }

    public void ResetTimer(int index)
    {
        unityAdsHelper.ShowRewardedAd(index);
    }
}
