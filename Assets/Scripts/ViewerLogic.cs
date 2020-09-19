using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewerLogic : MonoBehaviour
{
    public Button taskButton;
    public Button doneButton;
    public TMP_Text title;
    public TMP_Text description;
    public GameObject scroll;
    public GameObject canvas;
    private List<Project> pList = new List<Project>();
    private Project currentProject;
    private List<Task> tList = new List<Task>();
    private Task currentTask;

    // Start is called before the first frame update
    void Start()
    {
        pList = FindObjectOfType<AchievementLogic>().achievementProjects;
        currentProject = pList[FindObjectOfType<AchievementLogic>().getProjectIndex()];
        title.text = currentProject.getTitle();
        description.text = currentProject.getDescription();
        tList = currentProject.getCompletedTasks();
        foreach (Task t in tList)
        {
            Button b = Instantiate(taskButton, transform.position, transform.rotation);
            b.transform.SetParent(scroll.transform);
            b.GetComponentInChildren<TMP_Text>().text = t.getTitle();
            b.onClick.AddListener(delegate{
                currentTask = t;
            });
        }
        doneButton.onClick.AddListener(delegate{destroyMe();});
    }

    void destroyMe()
    {
        Destroy(gameObject);
    }

    public Task getCurrentTask()
    {
        return currentTask;
    }

    public void removeCompletedTask(Task t)
    {
        tList.Remove(t);
    }
}
