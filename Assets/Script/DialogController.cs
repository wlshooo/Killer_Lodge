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

     void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    private void Update()
    {
        CheckItem();
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
        Transform transform = canvas.transform;
        panel = transform.Find("Panel").gameObject;
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
}
