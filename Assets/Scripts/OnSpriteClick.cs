using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSpriteClick : MonoBehaviour
{
    UnityEvent myEvent;

    void Start()
    {
        if (myEvent == null)
            myEvent = new UnityEvent();

        myEvent.AddListener(Clicked);
    }

    void OnMouseDown()
    {
        myEvent.Invoke();
    }

    void Clicked()
    {
        Debug.Log("Clicked!");
    }
}
