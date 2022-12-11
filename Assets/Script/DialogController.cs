using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private float range; // 아이템 습득이 가능한 최대 거리

    private RaycastHit hitInfo; //충돌체 정보 저장
    [SerializeField] LayerMask layerMask;  //특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.
    [SerializeField]
    public GameObject panel;
    private GameObject canvas;
    [SerializeField]
    public Text panelText;

    [SerializeField]
    private GameObject SafeImage;

    [SerializeField]
    private GameObject keyImage;

    [SerializeField]
    private GameObject KeyFrame;

 

    //컴퓨터 UI
    [SerializeField]
    public GameObject ComputerPanel;
    [SerializeField]
    private Text ComputerText;


    [SerializeField]
    private GameObject Pause;

    [SerializeField]
    public GameObject Ending;

  
    AudioSource audioSource;

    [SerializeField]
    AudioClip buttonSoundClip;

    AudioSource FIrstFloorBgm;

    [SerializeField]
    AudioClip OpenDoor;

    [SerializeField]
    AudioClip CloseDoor;

    [SerializeField]
    AudioClip tryCloseDoor;

    [SerializeField]
    AudioClip tryOpenDoor;



    private bool isKeyCodeQ = false;          //메모를 읽지 않고 Q를 눌렀을 때 카운트와 풍선이 나오는걸 방지하는 bool 변수
    public bool isKey = false;
     void Start()
    {
        canvas = GameObject.Find("Canvas");
        Transform transform = canvas.transform;
        panel = transform.Find("Panel").gameObject;
        ComputerPanel = transform.Find("Computer Panel").gameObject;
        FIrstFloorBgm = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
       

    }
    private void Update()
    {
        CheckItem();

        if(Input.GetKeyDown(KeyCode.Q)&& isKeyCodeQ )
        {
            EnterComputerMemo();
            GameObject.Find("GameDirector").GetComponent<GameDirector>().isCount = true;
            EnterSafe();
            GameObject.Find("Balloon").GetComponent<BalloonController>().ShowBalloon1();

        }

        if(Input.GetKeyDown(KeyCode.Escape)) //Esc 키 누를시 일시정지, 시작
        {
            if(GameObject.Find("Player").GetComponent<PlayerController>().isInput==true )
            {
                GameObject.Find("Player").GetComponent<PlayerController>().isInput = false;
                ComputerPanel.SetActive(true);
                Pause.SetActive(true);


                Time.timeScale = 0; //제한 시간 일시정지
            }
            else if(GameObject.Find("Player").GetComponent<PlayerController>().isInput == false)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
                ComputerPanel.SetActive(false);
                Pause.SetActive(false);
                Time.timeScale = 1; //제한시간 다시 시작
            }
        }
    }
    public void offKeyImage()
    {
        KeyFrame.SetActive(false);
        keyImage.SetActive(false);
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Shose")
            {
                ShoseTextAppear();
            }
            if (hitInfo.transform.tag == "FirstDoor")
            {
                DoorTextAppear();
            }
            if(hitInfo.transform.tag =="Killer")
            {
                StartCoroutine(EndingSceneDelay());
            }

        }
        else
        {
            TextDisappear();
        }
    }

    public IEnumerator EndingSceneDelay()
    {
        yield return new WaitForSeconds(1f);
        //GameObject.Find("Camera").GetComponent<PlayerController>().isInput = false;
        Ending.SetActive(true);
    }

    /*public IEnumerator ChangeToInit()
    {
       
        GameObject.Find("Camera").GetComponent<AudioSource>().Stop();
        Ending.SetActive(false);
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("InitScene");
        
    }
    */
    private void DoorTextAppear()   //first stage ->1floor stage (DOOR)
    {

        if (canvas == null)
        {
            return;
        }

        if (panel == null)
        {
            return;
        }
        panelText.text = "문이 잠겨 있지 않은거 같다.... 들어가 볼까...?        " + "<color=yellow> " + "[ 마우스 좌 클릭시 문을 열고 들어갑니다. ]" + " </color>\n";
        panel.SetActive(true);
        panelText.gameObject.SetActive(true);
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("FirstFloor");
            FIrstFloorBgm.Play();
      
            audioSource.PlayOneShot(CloseDoor);
        }
    }
 
    private void ShoseTextAppear()
    {
       
        if(canvas==null)
        {
            return;
        }
    
        if(panel ==null)
        {
            return;
        }
        panelText.text = "이 신발은 한문이 신발인데.....그럼 앞에 저 시체는........\n\n" +
            "(컴퓨터에 메모가 쓰여 있는거 같다. 확인해보자)";
        panel.SetActive(true);
        panelText.gameObject.SetActive(true);
        
    }
    private void TextDisappear()
    {
        panel.SetActive(false);
        panelText.gameObject.SetActive(false);
    }
    public void ClickComputerMemo()
    {
        isKeyCodeQ = true;
        GameObject.Find("Camera").GetComponent<ActionController>().isComputer = false;
        if (canvas == null)
        {
            return;
        }

        if (ComputerPanel == null)
        {
            return;
        }
        ComputerText.text = "너가 이 메모를 읽고 있다는 건 나는 이곳에서 탈출하지 못했고\n\n" +
            "너와 다시 만날 수 없다는거겠지 이 별장은 살인마의 별장이야\n\n" +
            "너도 알고 있겠지만 들어오는 순간 문이 잠겨 나갈 수 없어\n\n" +
            "살인마가 본인의 유흥을 위해 우리를 유인하고 가지고 노는거 같아\n\n" +
            "나는 문제를 풀지 못했어 너는 문제를 풀고 꼭 탈출하길 바래\n\n" +
            "끌어들여서 미안해\n\n"
            + "- 한문이가 -\n\n"
            + "<color=yellow>" + "[Q]를 누를 시 메모창이 꺼집니다." + "</color>\n"
            + "<color=red>" + "메모가 사라지는 순간 10분의 제한시간이 생깁니다." + "</color>";
        ComputerPanel.SetActive(true);
        ComputerText.gameObject.SetActive(true);

    }
    public void EnterComputerMemo()
    {
        ComputerPanel.SetActive(false);
        ComputerText.gameObject.SetActive(false);
    }

    public void ClickSafe()
    {
        if (canvas == null)
        {
            return;
        }

        if (ComputerPanel == null)
        {
            return;
        }
        ComputerPanel.SetActive(true);
        SafeImage.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerController>().isInput = false;

    }
    public void EnterSafe()
    {
        ComputerPanel.SetActive(false);
        SafeImage.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
    }

    public void ShowKeyImage()
    {
        keyImage.SetActive(true);
        KeyFrame.SetActive(true);
        isKey = true;
    }

    public void trueKey()
    {
        

        if (canvas == null)
            {
                return;
            }

            if (panel == null)
            {
                return;
            }
            audioSource.PlayOneShot(tryOpenDoor);
            panelText.text = "문이 열렸다....!";
            panel.SetActive(true);
            panelText.gameObject.SetActive(true);
        StartCoroutine(ClearScene1Delay());




    }

    IEnumerator ClearScene1Delay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("ClearScene1");
    }
    public void falseKey()
    {
        if (canvas == null)
        {
            return;
        }

        if (panel == null)
        {
            return;
        }
        audioSource.PlayOneShot(tryCloseDoor);
        panelText.text = "문이 잠겨서 열쇠가 없으면 문을 열 수 없을거 같다. 열쇠를 찾고 열어보자..!";
        panel.SetActive(true);
        panelText.gameObject.SetActive(true);
    }

    public void GoBackGameButton()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
        ComputerPanel.SetActive(false);
        Pause.SetActive(false);
        Time.timeScale = 1; //제한시간 다시 시작
    }

    public void ExitGameButton()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        Debug.Log("게임종료!");
        Application.Quit();
    }

    
}
