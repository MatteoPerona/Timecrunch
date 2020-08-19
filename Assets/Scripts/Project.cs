using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project
{
    private string Title;
    private string Description;
    private GameObject obj;
    private List<Task> tasks;
    private List<Task> completedTasks = new List<Task>();
    public Project(string T, string D, List<Task> t){
        Title = T;
        Description = D;
        tasks = t;
    }
    public string getTitle()
    {
        return Title;
    }
    public string getDescription()
    {
        return Description;
    }
    public void setTitle(string T)
    {
        Title = T;
    }
    public void setDescription(string D)
    {
        Description = D;
    }
    public void setProjectTasks(List<Task> t)
    {
        tasks = t;
    }
    public List<Task> getProjectTasks()
    {
        return tasks;
    }
    public void addToCompletedTasks(Task t)
    {
        completedTasks.Add(t);
    }
    public List<Task> getCompletedTasks()
    {
        return completedTasks;
    }
    public float getProgress()
    {
        float count_incomplete = tasks.Count;
        float count_completed = completedTasks.Count;
        float total = count_incomplete + count_completed;
        float progress = count_completed/total;
        return progress;
    }
}
