using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false; //������ ���� ������ �� True;

    private RaycastHit hitInfo; //�浹ü ���� ����

    [SerializeField] LayerMask layerMask;  //Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    [SerializeField]
    private Text actionText;    //�ൿ�� ���� �� �ؽ�Ʈ;

   

   
   
    // Update is called once per frame
    void Update()
    {
        CheckComputer();
        CheckSafe();
        //TryAction();
    }
    private void CheckComputer()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hitInfo , range, layerMask))
        {
            if(hitInfo.transform.tag =="Computer")
            {
                ComputerInfoAppear();
            }
            
        }
        else
        {
            ComputerInfoDisappear();
        }
    }
    private void CheckSafe()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Safe")
            {
                SafeInfoAppear();
            }

        }
        else
        {
            SafeInfoDisappear();
        }
    }
    private void SafeInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " �� ���ðڽ��ϱ�? " + "<color=yellow>" + "[���콺 ��Ŭ��]" + "</color>";
    }
    
    private void SafeInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void ComputerInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " �޸� ���� �� �ֽ��ϴ�. " + "<color=yellow>" + "[���콺 ��Ŭ��]" + "</color>";
        if(Input.GetMouseButton(0))
        {
          GameObject.Find("Camera").GetComponent<DialogController>().ClickComputerMemo();

        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Camera").GetComponent<DialogController>().EnterComputerMemo();
            GameObject.Find("GameDirector").GetComponent<GameDirector>().isCount = true;
        }
    }
    private void ComputerInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
