using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OnSpriteClick : MonoBehaviour
{
    UnityEvent myEvent;
    GameObject myObject;
    ParticleSystem myParticleSystem;
    public bool shouldBounceOnStart = true;
    public string myObjectValue = "";

    void Start()
    {
        myObject = transform.Find("object").gameObject;
        myParticleSystem = transform.Find("stars").GetComponent<ParticleSystem>();
        

        if (myEvent == null)
            myEvent = new UnityEvent();

        myEvent.AddListener(Clicked);

        if (shouldBounceOnStart)
        {
            transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine("BounceObject");
        }else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator BounceObject()
    {
        transform.DOScale(1.2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(1, 0.2f);
    }

    IEnumerator BounceObjectWithCorrectTap()
    {
        myParticleSystem.Play();
        myObject.transform.DOScale(1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        myObject.transform.DOScale(0.8f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        myObject.transform.DOScale(1.0f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        GameController.Instance.IncreaseLevel();
    }



    void OnMouseDown()
    {
        myEvent.Invoke();
    }

    void Clicked()
    {
        if(GameController.Instance.currentTask == myObjectValue && GameController.Instance.isPlaying)
        {
            StartCoroutine("BounceObjectWithCorrectTap");
        }else if (GameController.Instance.currentTask != myObjectValue && GameController.Instance.isPlaying)
        {
            myObject.transform.DOShakePosition(0.5f,0.25f);
        }
    }
}
