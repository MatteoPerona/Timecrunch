using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stopwatch : MonoBehaviour
{
    public Button pausePlayBtn;
    public Button addTimeBtn;
    private Image[] pausePlayIms; 
    public TMP_Text text;
    public float speed = 1;
    private float time;
    private bool playing;
    private Task taskToCrunch;
    private float startTime;
    private bool paused;
    private System.DateTime timePaused;
    private float pauseDiff;
    void Start()
    {
        pausePlayIms = pausePlayBtn.gameObject.GetComponentsInChildren<Image>();
        pausePlayIms[0].gameObject.SetActive(true);
        pausePlayIms[1].gameObject.SetActive(false);
        pausePlayBtn.targetGraphic = pausePlayIms[0];

        startTime = Time.time;
        int i = FindObjectOfType<TodayManager>().getSelectedTaskIndex();
        taskToCrunch = FindObjectOfType<TodayManager>().getTodayTasks()[i];
        int index = 0;
        for(int x = 0; x < taskToCrunch.getTime().Length; x++){
            if(index == 0){
                time+=taskToCrunch.getTime()[x]*60*60;
            }
            else{
                time+=taskToCrunch.getTime()[x]*60;
            }
            index++;
        }
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
    }
    void Update()
    {
        if (playing){
            time -= Time.deltaTime * speed;
            string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        paused = pauseStatus;
        if (paused && playing)
        {
            timePaused = System.DateTime.Now;
        }
        else if (!paused && playing)
        {
            float s = (float)System.DateTime.Now.Second-timePaused.Second;
            float m = (float)System.DateTime.Now.Minute*60-timePaused.Minute*60;
            float h = (float)System.DateTime.Now.Hour*60*60-timePaused.Hour*60*60;
            float totalDiff = s+m+h;
            pauseDiff += totalDiff;
            time -= totalDiff;
            string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    public string getText()
    {
        return text.text;
    }
    public void pausePlay()
    {
        if(playing){
            playing = false;
            pausePlayIms[0].gameObject.SetActive(true);
            pausePlayIms[1].gameObject.SetActive(false);
            pausePlayBtn.targetGraphic = pausePlayIms[0];
            updateTaskTimeWorked();
        }
        else{
            playing = true;
            startTime = Time.time;
            pausePlayIms[0].gameObject.SetActive(false);
            pausePlayIms[1].gameObject.SetActive(true);
            pausePlayBtn.targetGraphic = pausePlayIms[1];
        }
    }
    public void addFive()
    {
        time += 60;
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
        if(!Input.GetMouseButtonUp(0))
        {
            addFive();
        }
    }

    public void updateTaskTimeWorked()
    {
        float now = Time.time;
        float timeWorked = now-startTime+pauseDiff;
        pauseDiff = 0f;
        Debug.Log("time worked: "+timeWorked);
        List<Project> projects = FindObjectOfType<ProjectViewLogic>().projects;
        bool outlier = true;
        foreach (Project p in projects)
        {
            foreach (Task t in p.getProjectTasks())
            {
                if (taskToCrunch == t)
                {
                    t.addTime(timeWorked);
                    outlier = false;

                }
            }
        }
        if (outlier)
        {
            int i = FindObjectOfType<TodayManager>().getTodayTasks().IndexOf(taskToCrunch);
            FindObjectOfType<TodayManager>().getTodayTasks()[i].addTime(timeWorked);
        }
    }
}
