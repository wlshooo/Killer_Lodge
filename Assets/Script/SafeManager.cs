using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SafeManager : MonoBehaviour
{
    

    [SerializeField]
    private Text password;  //현재 플레이어가 입력한 비밀번호

    string checkPassword = "1774"; //내가 설정하는 비밀번호


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
                GameObject.Find("Camera").GetComponent<DialogController>().EnterSafe();    // 비밀번호가 맞으면 Correct Message가 뜨고 1초 후 UI 종료
                GameObject.Find("Box").GetComponent<SafeAnimation>().SafeAnimationPlay();
                GameObject.Find("Camera").GetComponent<ActionController>().isSafe = false;
                Safe.GetComponent<BoxCollider>().enabled = false;
               
                

            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace)&& password.text.Length>0)
        {
            password.text = password.text.Substring(0, password.text.Length - 1);   //마지막 글자 제거
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
