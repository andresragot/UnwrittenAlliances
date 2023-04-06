//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClickableObject : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        //if(eventData.pointerId == -1 && !MenuControl.MouseInvert)
        //{
        //    GetComponent<Button>().onClick.Invoke();
        //    return;
        //}
        if(eventData.pointerId == -2 && MenuControl.MouseInvert)
        {
            GetComponent<Button>().onClick.Invoke();
            return;
        }
    }
}
