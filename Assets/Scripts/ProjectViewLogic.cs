
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectViewLogic : MonoBehaviour
{
    //Main
    public Canvas mainCanvas;
    public Camera mainCamera;
    public GameObject achievementScreen;
    public GameObject todayView;
    public GameObject projectScrollContent;
    //Project
    public GameObject projectObject;
    public GameObject projectEditorObject;
    public List<Project> projects = new List<Project>();
    //Task
    public GameObject taskObject;
    public Button activeProjectButton;
    //Private Logic
    private bool accounted;
    private int selectedProjectIndex;
    private Button selectedButton;
    private GameObject currentEditor;
    private bool isNew;
    private List<GameObject> activeProjectButtons;
    public List<Task> completedOutliers = new List<Task>();
    //public List<Project> completedProjects = new List<Project>();
    void Start()
    {
        todayView.SetActive(false);
        achievementScreen.SetActive(false);
        if (activeProjectButtons == null)
        {
            activeProjectButtons = new List<GameObject>();
        }
    }
    void Update()
    {
        setProjectProgress();
        //Debug.Log(activeProjectButtons.Count);
    }

    public void createNewProject()
    {
        GameObject newProjectObject = Instantiate(projectObject, transform.position, transform.rotation);
        currentEditor = newProjectObject;
        isNew = true;
    } 
    public void addProject(Project project)
    {
        projects.Add(project);
        Button button = Instantiate(activeProjectButton, transform.position, transform.rotation);
        button.GetComponentInChildren<Text>().text = project.getTitle();
        button.transform.SetParent(projectScrollContent.transform);
        button.onClick.AddListener(delegate{selectedProjectIndex = projects.IndexOf(project);});
        button.onClick.AddListener(delegate{selectedButton = button;});
        button.onClick.AddListener(delegate{openProjectEditor();});
        activeProjectButtons.Add(button.gameObject);
        Destroy(currentEditor);

        //Debug.Log(activeProjectButtons.Count);
    }
    
    public void openProjectEditor()
    {
        Project p = projects[selectedProjectIndex];
        GameObject projectEditor = Instantiate(projectEditorObject, transform.position, transform.rotation) as GameObject;
        currentEditor = projectEditor;
        isNew = false;
        TMP_InputField[] inputFields = projectEditor.GetComponentsInChildren<TMP_InputField>();
        inputFields[0].text = p.getTitle();
        inputFields[1].text = p.getDescription();
        FindObjectOfType<ProjectLogic>().setTasks(p.getProjectTasks());
    }
    public int getSelectedProjectIndex(){
        return selectedProjectIndex;
    }
    public Button getSelectedButton(){
        return selectedButton;
    }
    public void deleteProject()
    {
        if(!isNew){
            projects.Remove(projects[selectedProjectIndex]);
            activeProjectButtons.Remove(selectedButton.gameObject);
            Destroy(currentEditor);
            Destroy(selectedButton.gameObject);
        }
        else{
            Destroy(currentEditor);
        }
    }
    public bool getNewStatus()
    {
        return isNew;
    }
    public void setProjectProgress()
    {
        foreach (GameObject ap in activeProjectButtons)
        {
            string t = ap.GetComponentInChildren<Text>().text;
            foreach (Project p in projects)
            {
                if (t == p.getTitle())
                {
                	Image im = ap.GetComponentsInChildren<Image>()[1];
                    TMP_Text utCount = im.GetComponentInChildren<TMP_Text>();
                    utCount.text = p.getProjectTasks().Count.ToString();
                    im.fillAmount = p.getProgress();
                }
            }
        }
    }
}
