﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private string Title;
    private string Description;
    private int[] date;
    private int[] time;
    private float timeWorked;
    public Task(string T, string D, int[] d, int[] t)
    {
        Title = T;
        Description = D;
        date = d;
        time = t;
    }
    public string getTitle(){
        return Title;
    }
    public string getDescription(){
        return Description;
    }
    public int[] getDate(){
        return date;
    }
    public int[] getTime(){
        return time;
    }
    public void setTitle(string T){
        Title = T;
    }
    public void setDescription(string D){
        Description = D;
    }
    public void setDate(int[] d){
        date = d;
    }
    public void setTime(int[] t){
        time = t;
    }
    public void addTime(float f)
    {
        timeWorked += f;
    }
    public float getTimeWorked()
    {
        return timeWorked;
    }
    public void addDay()
    {
        date[0] += 1;
    }
    public void subtractDay()
    {
        date[0] -= 1;
    }
}
