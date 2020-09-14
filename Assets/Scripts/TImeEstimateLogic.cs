using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TImeEstimateLogic : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField timeEstimateInput;
    private float maxVal = 6*60f;

    void Start()
    {
        slider.value = 0f;
        timeEstimateInput.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        float hrs= float.Parse(timeEstimateInput.text.Substring(0,2));
        float mins = float.Parse(timeEstimateInput.text.Substring(3));
        if ((hrs*60f+mins)/maxVal != slider.value)
        {
            float totMin = slider.value * maxVal;
            float minutes = 0f;
            while (totMin % 60f != 0)
            {
                totMin-=1f;
                minutes+=1f;
            }
            float hours = totMin/60f;
            timeEstimateInput.text = hours.ToString()+":"+minutes.ToString();
        }
    }
}
