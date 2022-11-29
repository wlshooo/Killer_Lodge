using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SafeManager : MonoBehaviour
{
    

    [SerializeField]
    private Text password;  //현재 플레이어가 입력한 비밀번호

    string checkPassword = "12345"; //내가 설정하는 비밀번호

    private float DelayTime = 1.0f;
    private float tempDelay;
    private bool check = false;

   
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            tempDelay += Time.deltaTime;

            if(tempDelay >=DelayTime)
            {
                GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();    // 비밀번호가 맞으면 Correct Message가 뜨고 1초 후 UI 종료
                GameObject.Find("Box").GetComponent<SafeAnimation>().SafeAnimationPlay();
                GameObject.Find("Camera").GetComponent<ActionController>().isSafe = false;
               

            }
        }
    }

    public void InputNumber()       //버튼 클릭시 호출 함수
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //Debug.Log(clickObject.name + "," + clickObject.GetComponentInChildren<Text>().text);    //TextCheck

        password.text += clickObject.GetComponentInChildren<Text>().text;
        Debug.Log(password.text);
    }
    public void CheckPassword()     //Enter Button 클릭시 호출 함수
    {
        
        if(password.text==checkPassword)
        {
            password.text = "Correct !";
            check = true;
        }
        else
        {
            password.text = "";
        }
    }
  
}
