﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSpriteClick : MonoBehaviour
{
    UnityEvent myEvent;
    GameObject myObject;
    ParticleSystem myParticleSystem;
    public string myObjectValue = "";

    void Start()
    {
        myObject = transform.Find("object").gameObject;
        myParticleSystem = transform.Find("stars").GetComponent<ParticleSystem>();
        transform.localScale = new Vector3(1, 1, 1);

        if (myEvent == null)
            myEvent = new UnityEvent();

        myEvent.AddListener(Clicked);

        StartCoroutine("BounceObject");
    }

    IEnumerator BounceObject()
    {
        float a = 0;
        for(int i = 0; i < 12; i++)
        {
            a += 0.1f;
            transform.localScale = new Vector3(a, a, a);
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 2; i++)
        {
            a -= 0.1f;
            transform.localScale = new Vector3(a, a, a);
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BounceObjectWithCorrectTap()
    {
        myParticleSystem.Play();
        float a = 1;
        for (int i = 0; i < 10; i++)
        {
            a += 0.02f;
            myObject.transform.localScale = new Vector3(a, a, a);
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 20; i++)
        {
            a -= 0.02f;
            myObject.transform.localScale = new Vector3(a, a, a);
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 10; i++)
        {
            a += 0.02f;
            myObject.transform.localScale = new Vector3(a, a, a);
            yield return null;
            //yield return new WaitForSeconds(0.01f);
        }

        GameController.Instance.IncreaseLevel();
    }

    IEnumerator EaseInBounce() {
        
        myObject.transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.99f, 0.99f, 0.99f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.01f);
        myObject.transform.localScale = new Vector3(1f, 1f, 1f);

        

        yield return null;
    }


    void OnMouseDown()
    {
        myEvent.Invoke();
    }

    void Clicked()
    {
        if(GameController.Instance.currentTask == myObjectValue && GameController.Instance.isPlaying)
        {
            Debug.Log("YEP!");
            StartCoroutine("BounceObjectWithCorrectTap");
        }
    }
}
