using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClick()
    {
        SceneManager.LoadScene("MainStage");
    }

    public void ExitButtonClick()
    {
        Debug.Log("게임종료!");
        Application.Quit();
    }
}
