using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user : MonoBehaviour
{
    public string userName;
    public List<Project> projects;
    public float timeWorked;
    public float timeWorkedToday;
    public float points;
    public float exp;
    public float level;

    void Start()
    {
        UserData u = SaveSystem.LoadUser();
        if (u == null)
        {
            u = new UserData("Name Here");
        }
        userName = u.getName();
        timeWorked = u.getTimeWorked();
        timeWorkedToday = u.getTimeWorkedToday();
        points = u.points;
    }

    public void saveUser()
    {
        
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
