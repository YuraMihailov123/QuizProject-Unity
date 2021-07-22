using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine("FadeIn_Corouitne");
    }

    IEnumerator FadeIn_Corouitne()
    {
        float a = 0;
        for (int i = 0; i < 10; i++)
        {
            a += 0.1f;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, a);
            yield return null;
        }
    }
}
