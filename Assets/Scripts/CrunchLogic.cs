using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrunchLogic : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text time;
    private Task taskToCrunch;
    void Start()
    {
        int i = FindObjectOfType<TodayManager>().getSelectedTaskIndex();
        taskToCrunch = FindObjectOfType<TodayManager>().getTodayTasks()[i];
        title.text = taskToCrunch.getTitle();
        description.text = taskToCrunch.getDescription();
    }
    void Update()
    {
    }
    public void crunch()
    {
        //remove task from todayManager
        FindObjectOfType<TodayManager>().todayTasks.Remove(taskToCrunch);
        //remove task from project and task button from completable buttons list in today manager
        bool outlier = true;
        foreach(Project p in FindObjectOfType<ProjectViewLogic>().projects){
            List<Task> cTasks = p.getProjectTasks();
            foreach(Task t in cTasks){
                if (taskToCrunch == t){
                    outlier = false;
                    FindObjectOfType<TodayManager>().removeCompletebleTask(FindObjectOfType<TodayManager>().getCurrentButton());
                    Destroy(FindObjectOfType<TodayManager>().getCurrentButton());
                    cTasks.Remove(t);
                    p.addToCompletedTasks(t);
                    p.setProjectTasks(cTasks);
                    break;
                }
            }
        }
        if (outlier){
            FindObjectOfType<TodayManager>().removeCompletebleTask(FindObjectOfType<TodayManager>().getCurrentButton());
            Destroy(FindObjectOfType<TodayManager>().getCurrentButton());
            FindObjectOfType<ProjectViewLogic>().completedOutliers.Add(taskToCrunch);
        }
        //remove crunch screen & reset isCrunching
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), .1f).setOnComplete(destroyScreen);
        FindObjectOfType<TodayManager>().setIsCrunching(false);
    }
    public void checkUnfinishedCrunch()
    {
        if(FindObjectOfType<TodayManager>().getIsCrunching()){
            //set leftover time 
            string stopwatchTime = FindObjectOfType<stopwatch>().getText();
            int t0 = int.Parse(stopwatchTime.Substring(0, 2));
            int t1 = int.Parse(stopwatchTime.Substring(3, 2));
            if (t0 == 0 && t1 < 15){
                t1 = 15;
            }
            int[] newTime = new int[] {t0, t1};
            int i = FindObjectOfType<TodayManager>().getSelectedTaskIndex();
            taskToCrunch = FindObjectOfType<TodayManager>().getTodayTasks()[i];
            taskToCrunch.setTime(newTime);
            
            //instantiate the old task button
            FindObjectOfType<TodayManager>().getCurrentButton().SetActive(true);
            //int scrollIndex = FindObjectOfType<TodayManager>().selectedScrolElementIndex;
            //LeanTween.scaleY(FindObjectOfType<TodayManager>().getCurrentCrunchScreen(), 0f, .1f).setOnComplete(destroyScreen);
            destroyScreen();
            FindObjectOfType<TodayManager>().setIsCrunching(false);
        }
    }
    void destroyScreen()
    {
        Destroy(FindObjectOfType<TodayManager>().getCurrentCrunchScreen());
    }
}
