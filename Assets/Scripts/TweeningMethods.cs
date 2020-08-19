using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningMethods : MonoBehaviour
{
    public GameObject canvas;
    private GameObject screenFiller;
    public void fillScreen()
    {
        Vector3 touchPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenFiller = Instantiate(gameObject, touchPos, transform.rotation);
        LeanTween.scale(gameObject, new Vector3(canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z), .5f).setOnComplete(destroyMe);
    }
    void destroyMe()
    {
        //This might need a delay
        Destroy(screenFiller);
    }
}
