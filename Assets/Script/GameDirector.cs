using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    
    public bool isCount = false;

    public Text gameTimeUI;

    private bool isCorountine = true;
    float setTime = 600;
    int min;
    float sec;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(isCount)
        {
            gameTimeUI.gameObject.SetActive(true);
            setTime -= Time.deltaTime;
            min = (int)setTime / 60;
            sec = setTime % 60;
            if (setTime>= 60f)
            {
               
                gameTimeUI.text = "���� �ð� : " + min + "�� " + (int)sec + "��";
            }
            if(setTime < 60f)
            {
                gameTimeUI.text = "���� �ð� : "  + (int)sec + "��";
            }
            if(setTime <=0)
            {
                gameTimeUI.text = "���� �ð� : 0��";
               if(isCorountine)
                {
                    StartCoroutine(FailSceneLoad());
                }
              
              
            }
        }
    }


    IEnumerator FailSceneLoad()
    {
        isCorountine = false;
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("FailEnding");
        GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();
    }

}

