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
        panelText.text = "이 신발은 한문이 신발인데.....그럼 앞에 저 시체는........";
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
        ComputerText.text = "Esc누를 시 종료 근데 쳐다 보고 있어야 함 ㅋㅋㅋㅋㅋ";
        ComputerPanel.SetActive(true);
        ComputerText.gameObject.SetActive(true);

    }
    public void EnterComputerMemo()
    {
        ComputerPanel.SetActive(false);
        ComputerText.gameObject.SetActive(false);
    }
}
