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

    public bool isSafe = true;
    public bool isComputer = true;
    public bool isBalloon1 = true;



    // Update is called once per frame
    void Update()
    {
        CheckComputer();
        CheckSafe();
        CheckKey();
        CheckDoor();
        CheckBalloon();
        //TryAction();
    }
    private void CheckComputer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Computer")
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
        if (isSafe)
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

    }
    private void SafeInfoAppear()
    {
        if (isSafe)
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 를 여시겠습니까? " + "<color=yellow>" + "[마우스 좌클릭]" + "</color>";
            if (Input.GetMouseButton(0))
            {
                GameObject.Find("Camera").GetComponent<DialogController>().ClickSafe();

            }
        }

    }

    private void SafeInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void ComputerInfoAppear()
    {
        if (isComputer)
        {

            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 메모를 읽을 수 있습니다. " + "<color=yellow>" + "[마우스 좌클릭]" + "</color>";
            if (Input.GetMouseButton(0))
            {
                GameObject.Find("Camera").GetComponent<DialogController>().ClickComputerMemo();

            }

            /*if (Input.GetKeyDown(KeyCode.Escape)) //DialogController의 Update로 수정
            {
                GameObject.Find("Camera").GetComponent<DialogController>().EnterComputerMemo();
                GameObject.Find("GameDirector").GetComponent<GameDirector>().isCount = true;
            }
            */
        }

    }
    private void ComputerInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }


    private void CheckKey()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {

            if (hitInfo.transform.tag == "Key")
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName);
                KeyInfoAppear();
            }

        }
        else
        {
            KeyInfoDisappear();
        }



    }

    private void KeyInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 를 습득하시겠습니까? " + "<color=yellow>" + "습득(E)" + "</color>";
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("key").GetComponent<KeyController>().Getkey();
            GameObject.Find("Camera").GetComponent<DialogController>().ShowKeyImage();
        }

    }


    private void KeyInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }


    private void CheckDoor()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {

            if (hitInfo.transform.tag == "Door")
            {

                DoorInfoAppear();
            }

        }
        else
        {
            DoorDisappear();
        }
    }
    private void DoorInfoAppear()
    {

        actionText.gameObject.SetActive(true);
        actionText.text = "문을 여시겠습니까? " + "<color=yellow>" + "열기(F)" + "</color>";
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameObject.Find("Camera").GetComponent<DialogController>().isKey == true)
            {
                GameObject.Find("Camera").GetComponent<DialogController>().trueKey();
            }
            else if (GameObject.Find("Camera").GetComponent<DialogController>().isKey == false)
            {
                GameObject.Find("Camera").GetComponent<DialogController>().falseKey();
            }
        }
    }
    private void DoorDisappear()
    {
        actionText.gameObject.SetActive(false);
        GameObject.Find("Camera").GetComponent<DialogController>().panel.SetActive(false);

        GameObject.Find("Camera").GetComponent<DialogController>().panelText.gameObject.SetActive(false);
    }


    private void CheckBalloon()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {

            if (hitInfo.transform.name == "Balloon1")
            {

                Balloon1InfoAppear();
            }
            //Balloon2,3 코드 복붙 추가 
        }
        else
        {
            BalloonDisappear();
        }
    
      

    }
    private void Balloon1InfoAppear()
    {

        actionText.gameObject.SetActive(true);
        actionText.text =  "<color=red>" + "좌클릭 시 1번 문제를 오픈 할 수 있습니다." + "</color>";
        if (Input.GetMouseButton(0))
        {

            GameObject.Find("Balloon").GetComponent<BalloonController>().ShowB1Question();
        }

    }

    //Balloon2,3 코드 복붙 추가 

    private void BalloonDisappear()
    {
        actionText.gameObject.SetActive(false);
        
    }
}
