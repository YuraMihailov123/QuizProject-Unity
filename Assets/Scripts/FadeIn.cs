using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
        DOTween.Init();
        myText.DOFade(1, 1);
    }
}
