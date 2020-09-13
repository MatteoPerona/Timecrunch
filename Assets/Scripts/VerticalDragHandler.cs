using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerticalDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject dragger;
    private GameObject currentDragger;
    private GameObject content;
    private RectTransform rect;
    public float animTime;
    private int siblingIndex;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        content = transform.parent.gameObject;
        siblingIndex = gameObject.transform.GetSiblingIndex();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (content == null)
        {
            content = transform.parent.gameObject;
            siblingIndex = gameObject.transform.GetSiblingIndex();
            Debug.Log("content was null");
        }
        Debug.Log("Current sibling index of this child: "+siblingIndex);

        currentDragger = Instantiate(dragger, transform.position, transform.rotation);
        currentDragger.transform.SetParent(content.transform.parent);
        LeanTween.alpha(currentDragger.GetComponent<RectTransform>(), 1f, animTime);
        GetComponent<RectTransform>().LeanAlpha(.05f, animTime);
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDragger.transform.position = eventData.position;
        Button[] children = content.GetComponentsInChildren<Button>();
        //Debug.Log(children.Length+" children");

        if (siblingIndex < children.Length-1 && children[siblingIndex+1].transform.position.y > eventData.position.y)//down
        {
            siblingIndex++;
            transform.SetSiblingIndex(siblingIndex);
            //children[siblingIndex-1].SetSiblingIndex(siblingIndex-1);
            Debug.Log("sibling index added 1: "+siblingIndex);
        }
        else if (siblingIndex > 0 && children[siblingIndex-1].transform.position.y < eventData.position.y)//up
        {
            siblingIndex--;
            transform.SetSiblingIndex(siblingIndex);
            //children[siblingIndex+1].SetSiblingIndex(siblingIndex+1);
            Debug.Log("sibling index subtracted 1: "+siblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetSiblingIndex(siblingIndex);
        Debug.Log("sibling index: " + siblingIndex);
        GetComponent<RectTransform>().LeanAlpha(1f, animTime);
        LeanTween.alpha(currentDragger.GetComponent<RectTransform>(), 0f, animTime).setOnComplete(destroyDragger);
    }

    void destroyDragger()
    {
        Destroy(currentDragger);
    }
}
