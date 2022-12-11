using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SafeManager : MonoBehaviour
{
    

    [SerializeField]
    private Text password;  //���� �÷��̾ �Է��� ��й�ȣ

    string checkPassword = "1774"; //���� �����ϴ� ��й�ȣ


    GameObject Safe;

    private float DelayTime = 1.0f;
    private float tempDelay;
    private bool check = false;
  

   
    // Start is called before the first frame update
    void Start()
    {
        Safe = GameObject.Find("Safe");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (check)
        {
            tempDelay += Time.deltaTime;

            if (tempDelay >= DelayTime)
            {
                GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();    // ��й�ȣ�� ������ Correct Message�� �߰� 1�� �� UI ����
                GameObject.Find("Box").GetComponent<SafeAnimation>().SafeAnimationPlay();
                GameObject.Find("Camera").GetComponent<ActionController>().isSafe = false;
                Safe.GetComponent<BoxCollider>().enabled = false;
               
                

            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace)&& password.text.Length>0)
        {
            password.text = password.text.Substring(0, password.text.Length - 1);   //������ ���� ����
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
            password.text = " T R U E !";
            check = true;
        }
        else
        {
            password.text = " F A L S E !";
            StartCoroutine(FailSceneLoad());
        }
    }

    IEnumerator FailSceneLoad()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("FailEnding");
        GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();
       
    }

   
  
}
