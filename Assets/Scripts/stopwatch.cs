﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stopwatch : MonoBehaviour
{
    public TMP_Text text;
    public float speed = 1;
    private float time;
    private bool playing;
    private Task taskToCrunch;
    void Start()
    {
        int i = FindObjectOfType<TodayManager>().getSelectedTaskIndex();
        taskToCrunch = FindObjectOfType<TodayManager>().getTodayTasks()[i];
        int index = 0;
        for(int x = 0; x < taskToCrunch.getTime().Length; x++){
            if(index == 0){
                time+=taskToCrunch.getTime()[x]*60*60;
            }
            else{
                time+=taskToCrunch.getTime()[x]*60;
            }
            index++;
        }
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
    }
    void Update()
    {
        if (playing){
            time -= Time.deltaTime * speed;
            string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;
        }
    }
    public string getText()
    {
        return text.text;
    }
    public void pausePlay()
    {
        if(playing){
            playing = false;
        }
        else{
            playing = true;
        }
    }
    public void addFive()
    {
        time += 5*60;
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
    }
}
