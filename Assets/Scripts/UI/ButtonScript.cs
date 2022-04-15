using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent clickDown;
    public UnityEvent clickUp;
    public void OnPointerDown(PointerEventData eventData)
    {
        clickDown.Invoke();
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        clickUp.Invoke();
        
    }
}
