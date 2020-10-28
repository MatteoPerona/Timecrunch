using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    private string name;
    // private int avatarIndex;
    private float level;
    private float exp;
    private float timeWorkedToday;
    private float timeWorked;
    public float points;
    
    public UserData (string n)
    {
        name = n;
        //int avatar index
        level = 1f;
        exp = 0f;
        timeWorked = 0f;
        timeWorkedToday = 0f;
    }

    public void setName(string n)
    {
        name = n;
    }
    public string getName()
    {
        return name;
    }

    public float getLevel()
    {
        return level;
    }
    public float getExp()
    {
        return exp;
    }
    public float getTimeWorkedToday()
    {
        return timeWorkedToday;
    }
    public void setTimeWorkedToday(float f)
    {
        timeWorkedToday = f;
    }
    public float getTimeWorked()
    {
        return timeWorked;
    }

    


}
