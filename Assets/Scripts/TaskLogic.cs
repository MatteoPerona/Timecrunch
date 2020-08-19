using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskLogic : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField description;
    public TMP_InputField day;
    public TMP_InputField month;
    public TMP_InputField year;
    public TMP_InputField hours;
    public TMP_InputField minutes;
    public Button deleteButton;

    void Start()
    {
        deleteButton.onClick.AddListener(delegate{FindObjectOfType<ProjectLogic>().deleteTask();});
        int[] defaultDate = FindObjectOfType<TodayManager>().getDate();
        day.text = defaultDate[0].ToString();
        month.text = defaultDate[1].ToString();
        year.text = defaultDate[2].ToString();
    }
    public void saveTaskAs(){
        string T = title.text;
        string D = description.text;
        string d0 = day.text;
        string d1 = month.text;
        string d2 = year.text;
        int[] d = new int[]{int.Parse(d0), int.Parse(d1), int.Parse(d2)};
        string t0 = hours.text;
        string t1 = minutes.text;
        int[] t = new int[]{int.Parse(t0), int.Parse(t1)};
        Task task = new Task(T, D, d, t);
        FindObjectOfType<ProjectLogic>().addTask(task);
    }
    public void saveTask(){
        string T = title.text;
        string D = description.text;
        string d0 = day.text;
        string d1 = month.text;
        string d2 = year.text;
        int[] d = new int[]{int.Parse(d0), int.Parse(d1), int.Parse(d2)};
        string t0 = hours.text;
        string t1 = minutes.text;
        int[] t = new int[]{int.Parse(t0), int.Parse(t1)};
        int index = FindObjectOfType<ProjectLogic>().getSelectedTaskIndex();
        FindObjectOfType<ProjectLogic>().tasks[index].setTitle(T);
        FindObjectOfType<ProjectLogic>().tasks[index].setDescription(D);
        FindObjectOfType<ProjectLogic>().tasks[index].setDate(d);
        FindObjectOfType<ProjectLogic>().tasks[index].setTime(t);
        FindObjectOfType<ProjectLogic>().getSelectedTaskButton().GetComponentInChildren<TMP_Text>().text = T;
    }
    
    
}
