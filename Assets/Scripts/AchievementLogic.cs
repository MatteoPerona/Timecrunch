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
    public TMP_Text timeWorkedToday;
    public Button twtDescript;
    public Button projectBtn;
    public TMP_Text playerName;
    public TMP_Text level;
    public UserData user;
    public List<Project> achievementProjects = new List<Project>();
    private List<Task> allCompletedTasks = new List<Task>();
    private List<GameObject> activeProjectButtons;
    private int selectedProjectIndex;
    private Button selectedButton;
    private bool updated = false;
    private GameObject currentViewer;
    private int oldInds;

    void Start()
    {
        oldInds = 0;
        user = new UserData("Name Here");
        calculateLevel();
    	if (activeProjectButtons == null)
    	{
    		activeProjectButtons = new List<GameObject>();
    	}
        twtDescript.onClick.AddListener(delegate{
            describeTimeWorkedToday();
        });
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
        Debug.Log("oldInds: "+oldInds);
        achievementProjects = FindObjectOfType<ProjectViewLogic>().projects;
        Debug.Log("achievementProjectsCount: "+achievementProjects.Count);
        int newInds = achievementProjects.Count-oldInds;
        Debug.Log("newInds: "+newInds);
        if (newInds > 0)
        {
            for (int i = 0; i < newInds; i++)
            {
                addProjectButton(achievementProjects[oldInds+i]);
            }
        }

        foreach (Project p in achievementProjects)
        {
            foreach (Task t in p.getCompletedTasks())
            {
                bool isNew = true;
                foreach (Task ct in allCompletedTasks)
                {
                    if (t == ct)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    allCompletedTasks.Add(t);
                    user.rewardTaskCompletion(t);
                    calculateLevel();
                }
            }
        }
        updateTimeWorkedToday();
        oldInds += newInds;
    }

    public void calculateLevel()
    {
        float limit = user.getLevel()*3600f;
        experienceBar.GetComponent<Image>().fillAmount = user.getExp()/limit;
        if (user.getTimeWorkedToday() > 0)
        {
            timeWorkedToday.text = user.getTimeWorkedToday().ToString();
        }
        level.text = "Lv. " + user.getLevel().ToString();
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

    public void destroyProject(string t)
    {
        foreach (GameObject b in activeProjectButtons)
        {
            if (b.GetComponentInChildren<Text>().text == t)
            {
                activeProjectButtons.Remove(b);
                Destroy(b);
                break;
            }
        }
    }

    public void updateTimeWorkedToday()
    {
        float time = user.getTimeWorkedToday();
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        timeWorkedToday.text = hours + ":" + minutes + ":" + seconds;
    }

    public void describeTimeWorkedToday()
    {
        if (timeWorkedToday.text != "Time Worked Today")
        {
            timeWorkedToday.text = "Time Worked Today";
        }
        else
        {
            updateTimeWorkedToday();
        }
    }
}
