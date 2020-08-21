using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementLogic : MonoBehaviour
{
    public GameObject scroll;
    public GameObject experienceBar;
    public TMP_Text level;
    private float experience;
    private float totalTimeWorked;
    private float timeWorkedToday;
    private List<Project> achievementProjects = new List<Project>();
    private List<GameObject> activeProjectButtons;

    void Start()
    {
    	if (activeProjectButtons == null)
    	{
    		activeProjectButtons = new List<GameObject>();
    	}
    }

    void Update()
    {

    }

    public void addCompletedProject(Project p)
    {
    	completedProjects.Add(p);
    }

    public void updateProjectButtons()
    {
    	//if (FindObjectOfType<ProjectViewLogic>().)
    	//iterate through all projects in project view logic to determine if there are new completed 
    	//tasks/new projects and then update the achevements panel accordingly
    }

    public void updateTotalTimeWorked()
    {
    	//itterate through all projects and tasks within to calculate total time worked
    }
}
