using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleLogic : MonoBehaviour
{
    public GameObject projectToggle;
    public GameObject todayToggle;
    public GameObject achievementsToggle;
    public GameObject projectsCanvas;
    public GameObject todayCanvas;
    public GameObject achievementsCanvas;
    //private bool proj = false;
    //private bool today = false;
    //private bool achiev = false;
    //private bool buttonsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        initial();
    }

    void initial()
    {
        if (projectsCanvas.activeSelf)
        {
            achievementsToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(true);
                initial();
            });

            todayToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(true);
                achievementsCanvas.SetActive(false);
                initial();
            });
        } 
        else if (todayCanvas.activeSelf)
        {
            projectToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(true);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(false);
                initial();
            });

            achievementsToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(true);
                initial();
            });
        }  
        else if (achievementsCanvas.activeSelf)
        {
            todayToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(true);
                achievementsCanvas.SetActive(false);
                initial();
            });

            projectToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(true);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(false);
                initial();
            });
        }
    }

    /*
    void initial()
    {
        if (projectsCanvas.activeSelf == true)
        {
            Debug.Log("Project: "+projectsCanvas.activeSelf);
            proj = true;
            today = false;
            achiev = false;
            projectToggle.GetComponent<Button>().onClick.AddListener(delegate{Expand();});

            todayToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(true);
                achievementsCanvas.SetActive(false);
                resetToggles(projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>());
                initial();
            });
            achievementsToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(true);
                resetToggles(projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>());
                initial();
            });
        }
        
        else if (todayCanvas.activeSelf == true)
        {
            Debug.Log("todayCanvas: "+todayCanvas.activeSelf);
            proj = false;
            today = true;
            achiev = false;
            todayToggle.GetComponent<Button>().onClick.AddListener(delegate{Expand();});

            projectToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(true);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(false);
                resetToggles(todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>());
                initial();
            });
            achievementsToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(true);
                resetToggles(todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>());
                initial();
            });
        }

        else if (achievementsCanvas.activeSelf == true)
        {
            Debug.Log("achievementsCanvas: "+achievementsCanvas.activeSelf);
            proj = false;
            today = false;
            achiev = true;
            achievementsToggle.GetComponent<Button>().onClick.AddListener(delegate{Expand();});

            todayToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(false);
                todayCanvas.SetActive(true);
                achievementsCanvas.SetActive(false);
                resetToggles(achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>());
                initial();
            });
            projectToggle.GetComponent<Button>().onClick.AddListener(delegate{
                projectsCanvas.SetActive(true);
                todayCanvas.SetActive(false);
                achievementsCanvas.SetActive(false);
                resetToggles(achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>());
                initial();
            });
        }

        turnButtonsOff();
    }

    void turnButtonsOn()
    {
        buttonsOn = true;
        if (proj)
        {
            projectToggle.SetActive(false);
            todayToggle.SetActive(true);
            achievementsToggle.SetActive(true);
        }
        else if (today)
        {
            projectToggle.SetActive(true);
            todayToggle.SetActive(false);
            achievementsToggle.SetActive(true);
        }
        else if (achiev)
        {
            projectToggle.SetActive(true);
            todayToggle.SetActive(true);
            achievementsToggle.SetActive(false);
        }
    }
    void turnButtonsOff()
    {
        buttonsOn = false;
        if (proj)
        {
            Debug.Log("proj: "+proj);
            projectToggle.SetActive(true);
            todayToggle.SetActive(false);
            achievementsToggle.SetActive(false);
        }
        else if (today)
        {
            Debug.Log("today: "+today);
            projectToggle.SetActive(false);
            todayToggle.SetActive(true);
            achievementsToggle.SetActive(false);
        }
        else if (achiev)
        {
            Debug.Log("achiev: "+achiev);
            projectToggle.SetActive(false);
            todayToggle.SetActive(false);
            achievementsToggle.SetActive(true);
        }
    }

    void Expand()
    {
        turnButtonsOn();
        if (proj)
        {
            moveToggles(projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>());
        }
        else if (today)
        {
            moveToggles(todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>());
        }
        else if (achiev)
        {
            moveToggles(achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>());
        }
    }

    public void moveToggles(RectTransform p0, RectTransform p1, RectTransform p2)
    {
        LeanTween.moveX(p1.GetComponent<RectTransform>(), p1.GetComponent<RectTransform>().localPosition.x-75f, .35f).setEaseInOutExpo();
        LeanTween.moveX(p2.GetComponent<RectTransform>(), p2.GetComponent<RectTransform>().localPosition.x+75f, .35f).setEaseInOutExpo();
    }

    public void resetToggles(RectTransform p0, RectTransform p1, RectTransform p2)
    {
        LeanTween.moveX(p1.GetComponent<RectTransform>(), p1.GetComponent<RectTransform>().localPosition.x+75f, .35f).setEaseInExpo();
        LeanTween.moveX(p2.GetComponent<RectTransform>(), p2.GetComponent<RectTransform>().localPosition.x-75f, .35f).setEaseInExpo().setOnComplete(turnButtonsOff);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buttonsOn)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit  = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.zero, 0);
            if (hit.collider == null || !hit.collider.CompareTag("Toggle"))
            {
                if (proj)
                {
                    resetToggles(projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>());
                }
                else if (today)
                {
                    resetToggles(todayToggle.GetComponent<RectTransform>(), achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>());
                }
                else if (achiev)
                {
                    resetToggles(achievementsToggle.GetComponent<RectTransform>(), projectToggle.GetComponent<RectTransform>(), todayToggle.GetComponent<RectTransform>());
                }
            }
        }
    }
    */
}
