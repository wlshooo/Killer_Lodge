using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    
    public bool isCount = false;

    public Text gameTimeUI;

    float setTime = 65;
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
            }
        }
    }
}
