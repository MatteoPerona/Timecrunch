using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TImeEstimateLogic : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField hours;
    public TMP_InputField minutes;
    public float maxHours = 3f;
    private bool sliding;

    void Start()
    {
        if (FindObjectOfType<ProjectLogic>().getIsNew())
        {
            hours.text = "00";
            minutes.text = "00";
        }
        sliding = false;
        updateSlider();
        slider.onValueChanged.AddListener(delegate{updateText();});
        hours.onValueChanged.AddListener(delegate{updateSlider();});
        minutes.onValueChanged.AddListener(delegate{updateSlider();});
    }

    public void updateText()
    {
        sliding = true;
        float minEstim = slider.value * maxHours * 60;
        minEstim = Mathf.Round(minEstim);
        float hEstim = Mathf.Floor(minEstim/60f);
        minEstim -= hEstim*60f;
        hours.text = hEstim.ToString("00");
        minutes.text = minEstim.ToString("00");
        sliding = false;
    }

    public void updateSlider()
    {
        if (!sliding)
        {
            string hrs = hours.text;
            string min = minutes.text;
            if (hrs.Substring(0) == "0")
            {
                hrs = hrs.Remove(0);
            }
            if (min.Substring(0) == "0")
            {
                min = min.Remove(0);
            }
            float h = float.Parse(hrs);
            float m = float.Parse(min);
            float tot = h*60f+m;
            if (tot <= maxHours*60f)
            {
                slider.value = tot/(maxHours*60f);
            }
            else
            {
                slider.value = 1f;
            }
        }
    }
}
