using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoGame : MonoBehaviour
{
    [SerializeField] private bool timerStop, timeRestart, timeRecord, timePlayer;
    [SerializeField] private Text[] texts;
    private float timer;
    void Start()
    {
        if (timeRecord == false)
        {
            if (timeRestart == false) timer = PlayerPrefs.GetFloat("Time");
            if (timerStop == false) texts[0].text = "x " + PlayerPrefs.GetInt("Death");
            else texts[0].text = "" + PlayerPrefs.GetInt("Death");
        }
        else timer = PlayerPrefs.GetFloat("TimeRecord");
        UpdateTimerText();
    }
    void Update()
    {
        if(timerStop == false)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        string timerString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        texts[1].text = timerString;
        if(timeRestart == false && timeRecord == false) PlayerPrefs.SetFloat("Time", timer);

    }
    private void OnDisable()
    {
        if (PlayerPrefs.GetFloat("TimeRecord") < timer && timeRestart && timePlayer) PlayerPrefs.SetFloat("TimeRecord", timer);
    }
}
