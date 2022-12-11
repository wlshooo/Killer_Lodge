using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private float range; // ������ ������ ������ �ִ� �Ÿ�

    private RaycastHit hitInfo; //�浹ü ���� ����
    [SerializeField] LayerMask layerMask;  //Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.
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

 

    //��ǻ�� UI
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



    private bool isKeyCodeQ = false;          //�޸� ���� �ʰ� Q�� ������ �� ī��Ʈ�� ǳ���� �����°� �����ϴ� bool ����
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

        if(Input.GetKeyDown(KeyCode.Escape)) //Esc Ű ������ �Ͻ�����, ����
        {
            if(GameObject.Find("Player").GetComponent<PlayerController>().isInput==true )
            {
                GameObject.Find("Player").GetComponent<PlayerController>().isInput = false;
                ComputerPanel.SetActive(true);
                Pause.SetActive(true);


                Time.timeScale = 0; //���� �ð� �Ͻ�����
            }
            else if(GameObject.Find("Player").GetComponent<PlayerController>().isInput == false)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
                ComputerPanel.SetActive(false);
                Pause.SetActive(false);
                Time.timeScale = 1; //���ѽð� �ٽ� ����
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
        panelText.text = "���� ��� ���� ������ ����.... �� ����...?        " + "<color=yellow> " + "[ ���콺 �� Ŭ���� ���� ���� ���ϴ�. ]" + " </color>\n";
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
        panelText.text = "�� �Ź��� �ѹ��� �Ź��ε�.....�׷� �տ� �� ��ü��........\n\n" +
            "(��ǻ�Ϳ� �޸� ���� �ִ°� ����. Ȯ���غ���)";
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
        ComputerText.text = "�ʰ� �� �޸� �а� �ִٴ� �� ���� �̰����� Ż������ ���߰�\n\n" +
            "�ʿ� �ٽ� ���� �� ���ٴ°Ű��� �� ������ ���θ��� �����̾�\n\n" +
            "�ʵ� �˰� �ְ����� ������ ���� ���� ��� ���� �� ����\n\n" +
            "���θ��� ������ ������ ���� �츮�� �����ϰ� ������ ��°� ����\n\n" +
            "���� ������ Ǯ�� ���߾� �ʴ� ������ Ǯ�� �� Ż���ϱ� �ٷ�\n\n" +
            "����鿩�� �̾���\n\n"
            + "- �ѹ��̰� -\n\n"
            + "<color=yellow>" + "[Q]�� ���� �� �޸�â�� �����ϴ�." + "</color>\n"
            + "<color=red>" + "�޸� ������� ���� 10���� ���ѽð��� ����ϴ�." + "</color>";
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
            panelText.text = "���� ���ȴ�....!";
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
        panelText.text = "���� ��ܼ� ���谡 ������ ���� �� �� ������ ����. ���踦 ã�� �����..!";
        panel.SetActive(true);
        panelText.gameObject.SetActive(true);
    }

    public void GoBackGameButton()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
        ComputerPanel.SetActive(false);
        Pause.SetActive(false);
        Time.timeScale = 1; //���ѽð� �ٽ� ����
    }

    public void ExitGameButton()
    {
        audioSource.PlayOneShot(buttonSoundClip);
        Debug.Log("��������!");
        Application.Quit();
    }

    
}
