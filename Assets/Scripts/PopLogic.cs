using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopLogic : MonoBehaviour
{
    public Button yes;
    public Button no;

    // Start is called before the first frame update
    void Start()
    {
        yes.onClick.AddListener(delegate{
            //removal from project
            Task t = FindObjectOfType<ViewerLogic>().getCurrentTask();
            int index = FindObjectOfType<AchievementLogic>().getProjectIndex();
            List<Task> tList = FindObjectOfType<ProjectViewLogic>().projects[index].getProjectTasks();
            tList.Add(t);
            List<Task> ctList = FindObjectOfType<ProjectViewLogic>().projects[index].getCompletedTasks();
            ctList.Remove(t);
            FindObjectOfType<ProjectViewLogic>().projects[index].setProjectTasks(tList);
            FindObjectOfType<ProjectViewLogic>().projects[index].setCompletedTasks(ctList);
            //resetting progress on today
            foreach (Task ct in FindObjectOfType<TodayManager>().getCompletedToday())
            {
                if (t == ct)
                {
                    FindObjectOfType<TodayManager>().removeCompletedToday(t);
                    break;
                }
            }
            //buttons removal
            FindObjectOfType<ViewerLogic>().removeCompletedTask(t);
            LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseOutBack().setOnComplete(destroyMe);
        });

        no.onClick.AddListener(delegate{
            FindObjectOfType<ViewerLogic>().getCurrentButton().gameObject.SetActive(true);
            LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseOutBack().setOnComplete(destroyMe);
        });
    }

    void destroyMe()
    {
        Destroy(gameObject);
    }
}
