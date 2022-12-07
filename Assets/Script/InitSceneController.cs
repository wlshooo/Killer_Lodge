using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitSceneController : MonoBehaviour
{
    
    public GameObject ManualPanel;

    [SerializeField]
    private GameObject FadeOutPanel;

    private bool isStart = false;

    private float fDestroyTime = 2f;
    private float fTickTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    void Update()
    {
        if(isStart)
        {
            fTickTime += Time.deltaTime;

            if (fTickTime >= fDestroyTime)
            {
                // 2초 뒤에 실행
                
                SceneManager.LoadScene("MainStage");
            }
        }
       
    }
    public void StartButtonClick()
    { 
        FadeOutPanel.SetActive(true);
        isStart = true;
        GameObject.Find("GameManager").GetComponent<FadeOutManager>().FadeOut();

    }

    public void ExitButtonClick()
    {
        Debug.Log("게임종료!");
        Application.Quit();
    }

    public void ManualButtonClick()
    {
        ManualPanel.SetActive(true);
    }
    public void ManualExitButton()
    {
        ManualPanel.SetActive(false);
    }
}
