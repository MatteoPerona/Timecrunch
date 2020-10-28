using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompletableTaskObjectLogic : MonoBehaviour
{
    public Button forwardDay;
    public Button backDay;
    private Task task;

    void Start()
    {
        forwardDay.onClick.AddListener(delegate{forward();});
        backDay.onClick.AddListener(delegate{back();});
    }

    public void setTask(Task t)
    {
        task = t;
    }

    public void forward()
    {
        foreach (Project p in FindObjectOfType<ProjectViewLogic>().projects)
        {
            foreach (Task t in p.getProjectTasks())
            {
                if (t == task)
                {
                    t.addDay();
                    FindObjectOfType<TodayManager>().removeSavedTask(t);
                    FindObjectOfType<TodayManager>().moveUpdate(t);
                    break;
                }
            }
        }        
    }

    public void back()
    {
        foreach (Project p in FindObjectOfType<ProjectViewLogic>().projects)
        {
            foreach (Task t in p.getProjectTasks())
            {
                if (t == task)
                {
                    t.subtractDay();
                    FindObjectOfType<TodayManager>().removeSavedTask(t);
                    FindObjectOfType<TodayManager>().moveUpdate(t);
                    break;
                }
            }
        }
    }
}
