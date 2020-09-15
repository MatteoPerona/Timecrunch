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
        if (FindObjectOfType<ProjectLogic>().getIsNew())
        {
            day.text = defaultDate[0].ToString();
            month.text = defaultDate[1].ToString();
            year.text = defaultDate[2].ToString();
        }  
    }
    public void saveTaskAs(){
        string T = title.text;
        string D = description.text;
        string d0 = day.text;
        string d1 = month.text;
        string d2 = year.text;
        int[] d = new int[]{int.Parse(d0), int.Parse(d1), int.Parse(d2)};
        string h = hours.text;
        string m = minutes.text;
        int[] t = new int[]{int.Parse(h), int.Parse(m)};
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
        string h = hours.text;
        string m = minutes.text;
        int[] t = new int[]{int.Parse(h), int.Parse(m)};
        int index = FindObjectOfType<ProjectLogic>().getSelectedTaskIndex();
        FindObjectOfType<ProjectLogic>().tasks[index].setTitle(T);
        FindObjectOfType<ProjectLogic>().tasks[index].setDescription(D);
        FindObjectOfType<ProjectLogic>().tasks[index].setDate(d);
        FindObjectOfType<ProjectLogic>().tasks[index].setTime(t);
        FindObjectOfType<ProjectLogic>().getSelectedTaskButton().GetComponentInChildren<TMP_Text>().text = T;
    }
    
    public void addDay()
    {
        int dNum = int.Parse(day.text);
        dNum++;
        day.text = dNum.ToString();
    }
    public void addMonth()
    {
        int mNum = int.Parse(month.text);
        mNum++;
        month.text = mNum.ToString();
    }
    public void addYear()
    {
        int yNum = int.Parse(year.text);
        yNum++;
        year.text = yNum.ToString();
    }
    public void subtractDay()
    {
        int dNum = int.Parse(day.text);
        dNum--;
        day.text = dNum.ToString();
    }
    public void subtractMonth()
    {
        int mNum = int.Parse(month.text);
        mNum--;
        month.text = mNum.ToString();
    }
    public void subtractYear()
    {
        int yNum = int.Parse(year.text);
        yNum--;
        year.text = yNum.ToString();
    }
}
