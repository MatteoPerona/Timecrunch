using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectLogic : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField description;
    public GameObject scrollView;
    public GameObject taskObject;
    public GameObject taskEditorObject;
    public Button delete;
    public Button taskButton;
    private Project project;
    public List<Task> tasks = new List<Task>();
    private int selectedTaskIndex;
    private Button selectedTaskButton;
    public Button deleteButton;
    private GameObject currentTaskEditor;
    private bool isNew;

    void Start()
    {
        deleteButton.onClick.AddListener(delegate{FindObjectOfType<ProjectViewLogic>().deleteProject();});
    }
    public void saveAs()
    {
        string t = title.text;
        string d = description.text;
        project = new Project(t, d, tasks);
        FindObjectOfType<ProjectViewLogic>().addProject(project);
    }
    public void save()
    {
        int index = FindObjectOfType<ProjectViewLogic>().getSelectedProjectIndex();
        FindObjectOfType<ProjectViewLogic>().projects[index].setTitle(title.text);
        FindObjectOfType<ProjectViewLogic>().projects[index].setDescription(description.text);
        FindObjectOfType<ProjectViewLogic>().projects[index].setProjectTasks(tasks);
        FindObjectOfType<ProjectViewLogic>().getSelectedButton().GetComponentInChildren<Text>().text = title.text;
    }
    public void addTask(Task task)
    {
        tasks.Add(task);
        Button button = Instantiate(taskButton, transform.position, transform.rotation);
        button.GetComponentInChildren<TMP_Text>().text = task.getTitle();
        button.transform.SetParent(scrollView.transform);
        button.onClick.AddListener(delegate{selectedTaskIndex = tasks.IndexOf(task);});
        button.onClick.AddListener(delegate{selectedTaskButton = button;});
        button.onClick.AddListener(delegate{openTaskEditor();});
        Destroy(currentTaskEditor);  
    }
    public void setTasks(List<Task> t){
        tasks = t;
        foreach(Task task in tasks){
            Button button = Instantiate(taskButton, transform.position, transform.rotation);
            button.GetComponentInChildren<TMP_Text>().text = task.getTitle();
            button.transform.SetParent(scrollView.transform);
            button.onClick.AddListener(delegate{selectedTaskIndex = tasks.IndexOf(task);});
            button.onClick.AddListener(delegate{selectedTaskButton = button;});
            button.onClick.AddListener(delegate{openTaskEditor();});
        }
    }
    public void createNewTask()
    {
        GameObject newTaskObject = Instantiate(taskObject, transform.position, transform.rotation);
        currentTaskEditor = newTaskObject;
        isNew = true;
    }
    public void openTaskEditor()
    {
        Task t = tasks[selectedTaskIndex];
        GameObject taskEditor = Instantiate(taskEditorObject, transform.position, transform.rotation);
        currentTaskEditor = taskEditor;
        isNew = false;
        TMP_InputField[] inputFields = taskEditor.GetComponentsInChildren<TMP_InputField>();
        TMP_Dropdown[] dropdowns = taskEditor.GetComponentsInChildren<TMP_Dropdown>();
        inputFields[0].text = t.getTitle();
        inputFields[1].text = t.getDescription();
        inputFields[2].text = t.getTime()[0].ToString();
        inputFields[3].text = t.getTime()[1].ToString();
        inputFields[4].text = t.getDate()[0].ToString();
        inputFields[5].text = t.getDate()[1].ToString();
        inputFields[6].text = t.getDate()[2].ToString();
    }
    public int getSelectedTaskIndex(){
        return selectedTaskIndex;
    }
    public Button getSelectedTaskButton(){
        return selectedTaskButton;
    }
    public void deleteTask()
    {
        if(!isNew && !FindObjectOfType<ProjectViewLogic>().getNewStatus()){
            tasks.Remove(tasks[selectedTaskIndex]);
            int index = FindObjectOfType<ProjectViewLogic>().getSelectedProjectIndex();
            List<Task> t = FindObjectOfType<ProjectViewLogic>().projects[index].getProjectTasks();
            FindObjectOfType<ProjectViewLogic>().projects[index].setProjectTasks(t);
            Destroy(currentTaskEditor);
            Destroy(selectedTaskButton.gameObject);
        }
        else if (!isNew){
            tasks.Remove(tasks[selectedTaskIndex]);
            Destroy(currentTaskEditor);
            Destroy(selectedTaskButton.gameObject);
        }
        else{
            Destroy(currentTaskEditor);
        }
    }
}
