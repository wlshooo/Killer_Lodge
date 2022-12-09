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

    AudioSource InitSceneBgm;

    [SerializeField]
    AudioClip buttonSoundClip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        InitSceneBgm = GetComponent<AudioSource>();
        InitSceneBgm.Play();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(buttonSoundClip);
        FadeOutPanel.SetActive(true);
        isStart = true;
        GameObject.Find("GameManager").GetComponent<FadeOutManager>().FadeOut();

    }

    public void ExitButtonClick()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        Debug.Log("게임종료!");
        Application.Quit();
    }

    public void ManualButtonClick()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        ManualPanel.SetActive(true);
    }
    public void ManualExitButton()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        ManualPanel.SetActive(false);
    }
}
