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
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " �� ���ðڽ��ϱ�? " + "<color=yellow>" + "[���콺 ��Ŭ��]" + "</color>";
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
            actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " �޸� ���� �� �ֽ��ϴ�. " + "<color=yellow>" + "[���콺 ��Ŭ��]" + "</color>";
            if (Input.GetMouseButton(0))
            {
                GameObject.Find("Camera").GetComponent<DialogController>().ClickComputerMemo();

            }

            /*if (Input.GetKeyDown(KeyCode.Escape)) //DialogController�� Update�� ����
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
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " �� �����Ͻðڽ��ϱ�? " + "<color=yellow>" + "����(E)" + "</color>";
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
        actionText.text = "���� ���ðڽ��ϱ�? " + "<color=yellow>" + "����(F)" + "</color>";
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
            //Balloon2,3 �ڵ� ���� �߰� 
        }
        else
        {
            BalloonDisappear();
        }
    
      

    }
    private void Balloon1InfoAppear()
    {

        actionText.gameObject.SetActive(true);
        actionText.text =  "<color=red>" + "��Ŭ�� �� 1�� ������ ���� �� �� �ֽ��ϴ�." + "</color>";
        if (Input.GetMouseButton(0))
        {

            GameObject.Find("Balloon").GetComponent<BalloonController>().ShowB1Question();
        }

    }

    //Balloon2,3 �ڵ� ���� �߰� 

    private void BalloonDisappear()
    {
        actionText.gameObject.SetActive(false);
        
    }
}
