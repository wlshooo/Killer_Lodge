using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutManager : MonoBehaviour
{

    public Image image;     //fadeOut에 쓰이는 black Image
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1;
        while(fadeCount>0.0f)
        {
            fadeCount -= 0.001f;
            yield return new WaitForSeconds(0.001f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
