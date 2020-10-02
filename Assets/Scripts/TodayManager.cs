using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TodayManager : MonoBehaviour
{
    private string date;
    private string dateToday;
    public TMP_Text title;
    public List<Task> todayTasks = new List<Task>();
    private List<Task> newTodayTasks = new List<Task>();// clears on update
    public float multiplier = 1/18f;
    public GameObject completableTask;
    public GameObject completableTaskScroll;
    //public GameObject todayCanvas;
    private List<GameObject> completableTasks;
    //private int selectedTaskIndex;
    private List<int> taskProjectIndices = new List<int>();// clears on update
    private List<Project> projects = new List<Project>();// clears on update
    public GameObject crunchScreen;
    private GameObject currentButton;
    private GameObject currentCrunchScreen;
    private bool isCrunching;
    public int selectedScrolElementIndex;
    public GameObject outlyingTask;
    private GameObject currentOutlyingTaskEditor;
    private float totalTasksToday;
    private float completedTasksToday;
    public GameObject progressBar;
    private List<Task> completedToday = new List<Task>();
    public GameObject todayCanvas;
    private bool refreshed;
    public System.DateTime dateNow;
    public GameObject backArrow;
    public GameObject forwardArrow;
    private bool newDayProtocol = false;
    public GameObject newOutlierBtn;
    public TMP_Text todayTasksNumText;
    
    
    void Start()
    {
        refreshed = false;
        isCrunching = false;
        if(completableTasks == null){
            completableTasks = new List<GameObject>();
        }
        resetDate();
        today();
    }
    void Update()
    {
        if (!isCrunching && todayCanvas.activeSelf && !refreshed)
        {
            today();
            refreshed = true;
        }
        else if (refreshed && !todayCanvas.activeSelf)
        {
            refreshed = false;
        }
    }
    public void today()
    {
        
        // check if title and date match
        // if they don't it will set the new date and reset all initial data
        if (newDayProtocol)
        {
            title.text = date;
            completedTasksToday = 0f;
            totalTasksToday = 0f;
            todayTasks.Clear();
            completedToday.Clear();
            foreach (GameObject cTask in completableTasks)
            {
                Destroy(cTask);
            }
            completableTasks.Clear();
            FindObjectOfType<AchievementLogic>().user.setTimeWorkedToday(0f);
            newDayProtocol = false;
        }
        else if (title.text != date)
        {
            title.text = date;
            todayTasks.Clear();
            foreach (GameObject cTask in completableTasks)
            {
                Destroy(cTask);
            }
            completableTasks.Clear();
        }

        projects = FindObjectOfType<ProjectViewLogic>().projects;
        for(int x = 0; x < projects.Count; x++){
            Project p = projects[x];
            List<Task> pTasks = p.getProjectTasks();
            for(int j = 0; j < pTasks.Count; j++){
                Task t = pTasks[j];
                bool exists = false;
                foreach(Task tT in todayTasks){
                    if(tT == t){
                        exists = true;
                        break;
                    }
                }
                if(!exists){
                    string day = t.getDate()[0].ToString();
                    string month = t.getDate()[1].ToString();
                    string year = t.getDate()[2].ToString();
                    if(day.Length < 2){
                        day = "0"+day;
                    }
                    if(month.Length < 2){
                        month = "0"+month;
                    }
                    if(year.Length < 2){
                        year = "0"+year;
                    }
                    string taskDate = day+"/"+month+"/"+year;
                    if(date == taskDate){
                        newTodayTasks.Add(t);
                        taskProjectIndices.Add(projects.IndexOf(p)); 
                    }
                }
            }      
        }
        foreach(Task t in newTodayTasks){
            addCompletableTask(t);
        }
        foreach(Task t in newTodayTasks){
            todayTasks.Add(t);
            totalTasksToday += 1f;
        }
        if(todayTasks != null){
            newTodayTasks.Clear();
        }
        if(taskProjectIndices != null){
            taskProjectIndices.Clear();
        }
        
        todayTasksNumText.text = completableTasks.Count.ToString();

        updateButtons();
        updateTodayProgress();
    }
    public void addCompletableTask(Task t){
        float h = 0f;
        int index = 0;
        foreach(int i in t.getTime()){
            if(index == 0){
                h+=i*60*60;
            }
            else if(index == 1){
                h+=i*60;
            }
            else{
                h+=i;
            }
            index++;
        }
        if (h < 3600)
        {
            h = 3600;
        }
        h *= multiplier;
        GameObject nct = Instantiate(completableTask, transform.position, transform.rotation) as GameObject;
        nct.transform.SetParent(completableTaskScroll.transform);
        RectTransform rt = (RectTransform)nct.transform;
        float w = rt.rect.width;
        rt.sizeDelta = new Vector2(w, h);
        nct.GetComponentInChildren<CompletableTaskObjectLogic>().setTask(t);
        nct.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = t.getTitle();
        nct.GetComponentInChildren<Button>().onClick.AddListener(delegate{
                if (isCrunching)
                {
                    FindObjectOfType<CrunchLogic>().checkUnfinishedCrunch();
                }
            });
        nct.GetComponentInChildren<Button>().onClick.AddListener(delegate{currentButton = nct;});
        nct.GetComponentInChildren<Button>().onClick.AddListener(delegate{openCrunchScreen();});
        completableTasks.Add(nct);
        updateTodayProgress();
    }

    public int getSelectedTaskIndex()
    {
        int index = 0;
        string t = currentButton.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text;
        for(int x = 0; x < todayTasks.Count; x++){
            if(todayTasks[x].getTitle() == t){
                index = x;
                break;
            }
        }
        return index;
    }

    public List<Task> getTodayTasks()
    {
        return todayTasks;
    }

    public GameObject getCurrentButton(){
        return currentButton;
    }

    public void setIsCrunching(bool isIt){
        isCrunching = isIt;
    }

    public bool getIsCrunching()
    {
        return isCrunching;
    }

    public void openCrunchScreen()
    {
        //Debug.Log(currentButton.GetComponentInChildren<TMP_Text>().text);
        if(isCrunching==false)
        {
            isCrunching = true;
            int buttonScrollIndex = 0;
            Button[] buttons = completableTaskScroll.GetComponentsInChildren<Button>();
            //Debug.Log("buttons: "+buttons.Length);
            for (int x = 0; x < buttons.Length; x++)
            {
                try
                {
                    //Debug.Log("button title: "+buttons[x].GetComponentInChildren<TMP_Text>().text);
                    if (buttons[x].GetComponentInChildren<TMP_Text>().text == currentButton.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text)
                    {
                        buttonScrollIndex = x;
                        selectedScrolElementIndex = buttonScrollIndex;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            foreach(GameObject cTask in completableTasks)
            {
                //Debug.Log(cTask.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text+" "+buttons[buttonScrollIndex].GetComponentInChildren<TMP_Text>().text);
                if(cTask.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text == buttons[buttonScrollIndex].GetComponentInChildren<TMP_Text>().text)
                {
                    cTask.SetActive(false);
                    break;
                }
            }
            GameObject newCrunch = Instantiate(crunchScreen, transform.position, transform.rotation);
            newCrunch.transform.SetParent(completableTaskScroll.transform);
            newCrunch.transform.SetSiblingIndex(buttonScrollIndex);
            currentCrunchScreen = newCrunch;
        }
        else{
            FindObjectOfType<CrunchLogic>().checkUnfinishedCrunch();
            //openCrunchScreen();
        }
    }

    public GameObject getCurrentCrunchScreen()
    {
        return currentCrunchScreen;
    }

    public void updateButtons(){

        for(int x = 0; x < todayTasks.Count; x++){
            //Check if date is still a match
            string day = todayTasks[x].getDate()[0].ToString();
            string month  = todayTasks[x].getDate()[1].ToString();
            string year = todayTasks[x].getDate()[2].ToString();
            if(day.Length < 2){
                day = "0"+day;
            }
            if(month.Length < 2){
                month = "0"+month;
            }
            if(year.Length < 2){
                year = "0"+year;
            }
            string taskDate = day+"/"+month+"/"+year;
            if(taskDate != date){
                Destroy(completableTasks[x]);
                completableTasks.Remove(completableTasks[x]);
                todayTasks.Remove(todayTasks[x]);
                totalTasksToday -= 1;
            }

            //Resize button if necessary
            float h = 0f;
            int index = 0;
            foreach(int i in todayTasks[x].getTime()){
                if(index == 0){
                    h+=i*60*60;
                }
                else if(index == 1){
                    h+=i*60;
                }
                else{
                    h+=i;
                }
                index++;
            }
            if (h < 3600)
            {
                h = 3600;
            }
            h *= multiplier;
            RectTransform rt = (RectTransform)completableTasks[x].transform;
            float w = rt.rect.width;
            rt.sizeDelta = new Vector2(w, h);

            completableTasks[x].GetComponentInChildren<TMP_Text>().text = todayTasks[x].getTitle();
            updateTodayProgress();
        }
    }
    public void newOutlyingTask()
    {
        currentOutlyingTaskEditor = Instantiate(outlyingTask, transform.position, transform.rotation);
    }
    public int[] getDate()
    {
        int day = int.Parse(date.Substring(0, 2));
        int month = int.Parse(date.Substring(3, 2));
        int year = int.Parse(date.Substring(6, 2));
        return new int[] {day, month, year};
    }
    public GameObject getCurrentOutlier()
    {
        return currentOutlyingTaskEditor;
    }
    public void saveOutlier(Task t)
    {
        addCompletableTask(t);
        todayTasks.Add(t);
        totalTasksToday += 1;
        updateTodayProgress();
    }
    public void removeCompletebleTask(GameObject cTask)
    {
        completableTasks.Remove(cTask);
        completedTasksToday += 1;
        updateTodayProgress();
    }
    public void updateTodayProgress()
    {
        if (completedTasksToday == 0)
        {
            progressBar.GetComponent<Image>().fillAmount = 0f;
        }
        else
        {
            progressBar.GetComponent<Image>().fillAmount = completedTasksToday/totalTasksToday;
        }
    }
    public void addCompletedToday(Task t)
    {
        completedToday.Add(t);
    }
    public void removeCompletedToday(Task t)
    {
        completedToday.Remove(t);
        completedTasksToday -= 1;
        totalTasksToday -= 1;
    }
    public List<Task> getCompletedToday()
    {
        return completedToday;
    }
    public void removeTodayTask(Task t)
    {
        int index = todayTasks.IndexOf(t);
        todayTasks.Remove(t);
        totalTasksToday -= 1;
        Destroy(completableTasks[index]);
        completableTasks.Remove(completableTasks[index]);
        updateTodayProgress();
    }

    public void forwardDate()
    {
        if (date == "Past Due")
        {
            resetDate();
            today();
        }
        else
        {
            dateNow = dateNow.AddDays(1);
            date = dateNow.ToString("dd/MM/yy");
            today();
        } 
    }

    public void backDate()
    {
        if (dateNow.ToString("dd/MM/yy") == System.DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yy"))
        {
            date = "Past Due";
            backArrow.SetActive(false);
            today();
        }
        else
        {
            dateNow = dateNow.AddDays(-1);
            date = dateNow.ToString("dd/MM/yy");
            today();
        }
        if (backArrow.activeSelf == false)
        {
            backArrow.SetActive(true);
        }
    }

    public void resetDate()
    {
        dateNow = System.DateTime.UtcNow.ToLocalTime();
        date = dateNow.ToString("dd/MM/yy");
        if (date != dateToday)
        {
            newDayProtocol = true;
            dateToday = date;
        }
    }
}
