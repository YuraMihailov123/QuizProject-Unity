using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSpriteClick : MonoBehaviour
{
    UnityEvent myEvent;
    GameObject myObject;

    void Start()
    {
        myObject = transform.Find("object").gameObject;
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
        Debug.Log("Clicked:" + gameObject.name);
        if(gameObject.name == "button")
        {
            GameController.Instance.IncreaseLevel();
        }
    }
}
