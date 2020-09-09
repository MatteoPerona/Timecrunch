using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopLogic : MonoBehaviour
{
    public TMP_Text title;
    public Button yes;
    public Button no;
    private string ogTitle;
    public float animTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        ogTitle = title.text;
        gameObject.GetComponent<Button>().onClick.AddListener(delegate{
            activatePopUp();
        });
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
            gameObject.LeanScale(Vector3.zero, animTime).setOnComplete(destroyMe);
        });

        no.onClick.AddListener(delegate{
            closePopUp();
        });
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
    }

    void activatePopUp()
    {
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        LeanTween.size(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().sizeDelta* new Vector2(1f, 1.3f), animTime).setEaseInCubic();
        LeanTween.alphaText(title.rectTransform, 0f, animTime);
        changeText();
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);
    }
    void changeText()
    {
        if (title.text == ogTitle)
        {
            title.text = "Re-Activate?";
            LeanTween.moveLocalY(title.gameObject, title.transform.localPosition.y+75f, animTime);
            LeanTween.alphaText(title.rectTransform, 1f, animTime);
        }
        else
        {
            LeanTween.moveLocalY(title.gameObject, title.transform.localPosition.y-75f, animTime);
            title.text = ogTitle;
            LeanTween.alphaText(title.rectTransform, 1f, animTime);
        }
    }
    void closePopUp()
    {
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        LeanTween.alphaText(title.rectTransform, 0f, animTime);
        changeText();
        LeanTween.size(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().sizeDelta* new Vector2(1f, .76923f), animTime).setEaseInCubic();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate{
            activatePopUp();
        });
    }
    void destroyMe()
    {
        Destroy(gameObject);
    }
}
