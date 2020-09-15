using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TImeEstimateLogic : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField hours;
    public TMP_InputField minutes;

    void Start()
    {
        slider.value = 0f;
        hours.text = "00";
        minutes.text = "00";
    }

    void Update() {
        updateText();    
    }

    public void updateText()
    {
        float hrs= float.Parse(hours.text.Substring(0,2));
        float mins = float.Parse(minutes.text.Substring(3));
        Debug.Log("Hours: "+hrs);
        Debug.Log("Minutes: "+mins);
    }
}
