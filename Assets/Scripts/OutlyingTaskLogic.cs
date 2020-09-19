using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutlyingTaskLogic : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField description;
    public TMP_InputField hours;
    public TMP_InputField minutes;
    private Task newOutlier;
    public void save()
    {
        string T = title.text;
        string D = description.text;
        string t0 = hours.text;
        string t1 = minutes.text;
        int[] t = new int[]{int.Parse(t0), int.Parse(t1)};
        int[] d = FindObjectOfType<TodayManager>().getDate();
        newOutlier = new Task(T, D, d, t);
        List<Task> newTasks = FindObjectOfType<ProjectViewLogic>().projects[0].getProjectTasks();
        newTasks.Add(newOutlier);
        FindObjectOfType<ProjectViewLogic>().projects[0].setProjectTasks(newTasks);
        Destroy(FindObjectOfType<TodayManager>().getCurrentOutlier());
        FindObjectOfType<TodayManager>().saveOutlier(newOutlier);
    }
    public void delete()
    {
        Destroy(FindObjectOfType<TodayManager>().getCurrentOutlier());
    }
}
