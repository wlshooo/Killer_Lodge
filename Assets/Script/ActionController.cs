using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false; //아이템 습득 가능할 시 True;

    private RaycastHit hitInfo; //충돌체 정보 저장

    [SerializeField] LayerMask layerMask;  //특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    [SerializeField]
    private Text actionText;    //행동을 보여 줄 텍스트;

   

   
   
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 를 여시겠습니까? " + "<color=yellow>" + "[마우스 좌클릭]" + "</color>";
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 메모를 읽을 수 있습니다. " + "<color=yellow>" + "[마우스 좌클릭]" + "</color>";
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
