using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SafeManager : MonoBehaviour
{
    

    [SerializeField]
    private Text password;  //���� �÷��̾ �Է��� ��й�ȣ

    string checkPassword = "12345"; //���� �����ϴ� ��й�ȣ

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
                GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();    // ��й�ȣ�� ������ Correct Message�� �߰� 1�� �� UI ����
                GameObject.Find("Box").GetComponent<SafeAnimation>().SafeAnimationPlay();
                GameObject.Find("Camera").GetComponent<ActionController>().isSafe = false;
               

            }
        }
    }

    public void InputNumber()       //��ư Ŭ���� ȣ�� �Լ�
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //Debug.Log(clickObject.name + "," + clickObject.GetComponentInChildren<Text>().text);    //TextCheck

        password.text += clickObject.GetComponentInChildren<Text>().text;
        Debug.Log(password.text);
    }
    public void CheckPassword()     //Enter Button Ŭ���� ȣ�� �Լ�
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
