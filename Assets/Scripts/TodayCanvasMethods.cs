using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodayCanvasMethods : MonoBehaviour
{
    private void OnDisable() {
        FindObjectOfType<TodayManager>().resetDate();    
    } 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
