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
    public GameObject popUp;
    private List<Project> pList = new List<Project>();
    private Project currentProject;
    private List<Task> tList = new List<Task>();
    private Task currentTask;
    private Button currentButton;
    
    // Start is called before the first frame update
    void Start()
    {
        pList = FindObjectOfType<ProjectViewLogic>().projects;
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
                currentButton = b;
                activatePopUp();
                b.gameObject.SetActive(false);
            });
        }
        doneButton.onClick.AddListener(delegate{destroyMe();});
    }

    void activatePopUp()
    {
        //ask give the user a choice to set the task as active again
        GameObject newPop = Instantiate(popUp, transform.position, transform.rotation);
        newPop.transform.SetParent(scroll.transform);
        //LeanTween.scale(newPop, Vector3.zero, 0f);
        LeanTween.scale(newPop, new Vector3(1f, 1f, 1f), .5f).setEasePunch();
        
    }
    void destroyMe()
    {
        Destroy(gameObject);
    }

    public Task getCurrentTask()
    {
        return currentTask;
    }
    public Button getCurrentButton()
    {
        return currentButton;
    }
    public void removeCompletedTask(Task t)
    {
        tList.Remove(t);
    }
}
