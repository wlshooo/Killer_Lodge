using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToInitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GameObject.Find("Camera").GetComponent<DialogController>().ChangeToInit());
        StartCoroutine(ChangeToInit());
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }

    public IEnumerator ChangeToInit()
    {

        GameObject.Find("Camera").GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(10.0f);
        Application.Quit();

    }
}
