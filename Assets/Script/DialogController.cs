using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private float range; // 아이템 습득이 가능한 최대 거리

    private RaycastHit hitInfo; //충돌체 정보 저장
    [SerializeField] LayerMask layerMask;  //특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.
    [SerializeField]
    private GameObject panel;
    private GameObject canvas;
    [SerializeField]
    private Text panelText;

    [SerializeField]
    private GameObject SafeImage;

    //컴퓨터 UI
    [SerializeField]
    private GameObject ComputerPanel;
    [SerializeField]
    private Text ComputerText;
    
     void Start()
    {
        canvas = GameObject.Find("Canvas");
        Transform transform = canvas.transform;
        panel = transform.Find("Panel").gameObject;
        ComputerPanel = transform.Find("Computer Panel").gameObject;
       
    }
    private void Update()
    {
        CheckItem();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            EnterComputerMemo();
            GameObject.Find("GameDirector").GetComponent<GameDirector>().isCount = true;
            EnterSafe();

        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Shose")
            {
                ShoseTextAppear();
            }

        }
        else
        {
            ShoseTextDisappear();
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
    private void ShoseTextDisappear()
    {
        panel.SetActive(false);
        panelText.gameObject.SetActive(false);
    }
    public void ClickComputerMemo()
    {
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
    }
    public void EnterSafe()
    {
        ComputerPanel.SetActive(false);
        SafeImage.SetActive(false);
    }
}
