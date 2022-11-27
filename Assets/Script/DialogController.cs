using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private float range; // ������ ������ ������ �ִ� �Ÿ�

    private RaycastHit hitInfo; //�浹ü ���� ����
    [SerializeField] LayerMask layerMask;  //Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.
    [SerializeField]
    private GameObject panel;
    private GameObject canvas;
    [SerializeField]
    private Text panelText;

    //��ǻ�� UI
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
        panelText.text = "�� �Ź��� �ѹ��� �Ź��ε�.....�׷� �տ� �� ��ü��........";
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
        ComputerText.text = "Esc���� �� ���� �ٵ� �Ĵ� ���� �־�� �� ����������";
        ComputerPanel.SetActive(true);
        ComputerText.gameObject.SetActive(true);

    }
    public void EnterComputerMemo()
    {
        ComputerPanel.SetActive(false);
        ComputerText.gameObject.SetActive(false);
    }
}
