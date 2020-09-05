using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementLogic : MonoBehaviour
{
    public GameObject canvas;
    public GameObject scroll;
    public GameObject experienceBar;
    public GameObject projectViewer;
    public Button projectBtn;
    private float level;
    private float exp;
    public TMP_Text playerName;
    private float experience;
    private float totalTimeWorked;
    private float timeWorkedToday;
    private List<Project> achievementProjects = new List<Project>();
    private List<GameObject> activeProjectButtons;
    private int selectedProjectIndex;
    private Button selectedButton;
    private bool updated = false;
    private GameObject currentViewer;

    void Start()
    {
    	if (activeProjectButtons == null)
    	{
    		activeProjectButtons = new List<GameObject>();
    	}
    }

    void Update()
    {
        if(canvas.activeSelf && !updated)
        {
            updateProjectButtons();
            updated = true;
        }
        else if(!canvas.activeSelf && updated)
        {
            updated = false;
        }
    }

    public void addCompletedProject(Project p)
    {
    	achievementProjects.Add(p);
    }

    public void updateProjectButtons()
    {
        achievementProjects = FindObjectOfType<ProjectViewLogic>().projects;
        int newInds = achievementProjects.Count-activeProjectButtons.Count;
        Debug.Log("New Inds Count for ACHIEVEMENTS: "+newInds);
        if (newInds > 0)
        {
            for (int i = 0; i < newInds; i++)
            {
                addProjectButton(achievementProjects[activeProjectButtons.Count+i]);
            }
        }
    }
    public void addProjectButton(Project project)
    {
        Button button = Instantiate(projectBtn, transform.position, transform.rotation);
        button.GetComponentInChildren<Text>().text = project.getTitle();
        button.transform.SetParent(scroll.transform);
        button.onClick.AddListener(delegate{selectedProjectIndex = achievementProjects.IndexOf(project);});
        button.onClick.AddListener(delegate{selectedButton = button;});
        button.onClick.AddListener(delegate{openProjectViewer();});
        activeProjectButtons.Add(button.gameObject);
    }

    public void openProjectViewer()
    {
        currentViewer = Instantiate(projectViewer, transform.position, transform.rotation);
    }

    public int getProjectIndex()
    {
        return selectedProjectIndex;
    }

    public void updateTotalTimeWorked()
    {
    	//itterate through all projects and tasks within to calculate total time worked
    }

    public void destroyProject(string t)
    {
        foreach (Button b in scroll.GetComponentsInChildren<Button>())
        {
            if (b.GetComponentInChildren<Text>().text == t)
            {
                Destroy(b.gameObject);
            }
        }
    }
}
