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

    public void rewardTaskCompletion(Task t)
    {
        float reward = 100f;
        reward += t.getTimeWorked();
        exp += reward;

        timeWorked += t.getTimeWorked();
        timeWorkedToday += t.getTimeWorked();
        checkLevelUp();
    }

    public void rewardProjectCompletion(Project p)
    {
        float reward = 100f;
        reward += p.getTimeWorked();
        exp += reward;

        checkLevelUp();
    }
    
    public void checkLevelUp()
    {
        float limit = 3600f*level;
        if (exp >= limit)
        {
            Debug.Log("limit hit: "+ limit);
            exp -= limit;
            level += 1f;
        }
        Debug.Log("level: " + level);
        Debug.Log("exp: " + exp);
    }


}
