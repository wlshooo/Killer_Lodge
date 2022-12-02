using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   //���ǵ� ���� ����
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;
    private float applyCrouchPosY; //�� ������ ���� crouch ��Ʈ��

    [SerializeField]
    private float jumpForce;
    //���º���
    private bool isCrouch = false; //�ɾ��ִ��� �ƴ��� üũ
    private bool isRun = false;
    private bool isGround = true; //���� �ִ��� üũ
    public bool isInput = true; //�н����� �Է����Ͻ�-false
    
    [SerializeField]
    private float crouchPosY;  // �ɾ����� �󸶳� ������ �����ϴ� ����
    private float originPosY;   //�� �־������� ��ġ�� ����س� ����

    private CapsuleCollider capsuleCollider; //������ �پ� �ִ��� �Ǵ�


    [SerializeField]
    private float lookSensitivity;  //ī�޶� �ΰ���

    [SerializeField]
    private float cameraRotationLimit;  //ī�޶� ���� �Ѱ�ġ
    private float currentCameraRotationX = 0f;

    //�ʿ��� ���۳�Ʈ
    [SerializeField]
    private Camera theCamera;   //ī�޶� �÷��̾��� �ڽ� ��ü�� ���ֱ� ������, ���� ī�޶� �Ѱ��� �ִ°� �ƴ� �� �����Ƿ�

    private Rigidbody myRigid;


    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;   //�÷��̾� ��� ī�޶��� y�� ���� �̶� ī�޶� �÷��̾� �ȿ� ���� �����Ƿ� ������� ����
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
        ItemPickup();

    }
    /* private bool CheckObject()
     {
         if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 1))
         {
             return true;
         }
         return false;
     }    
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Door")
        {
          
            SceneManager.LoadScene("FirstFloor");
        }
  
    }
    private void ItemPickup()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PickupCoroutine());
        }
    }
    IEnumerator PickupCoroutine()
    {
        anim.SetTrigger("ItemPickup");
        yield return null;
    }
    private void TryCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }
    private void Crouch()
    {
        isCrouch = !isCrouch;

        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroution());
        
    }
    IEnumerator CrouchCoroution()   //���������� �̵��ϴ� �Լ��� �ڵ�� �޸� �ڷ�ƾ�� ������ �Ǹ� �ڷ�ƾ�� ���������� ���డ���ϴ�.
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        while(_posY!=applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.5f);//���� �Լ� ->�Ű�����1���� �Ű�����2���� �Ű�����3�� ������ �����Ѵ�.
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;   //null -> 1�����Ӹ��� ���
        }
        theCamera.transform.localPosition = new Vector3(0f, applyCrouchPosY, 0f);
      
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y +0.1f);
        //�÷��̾��� �����ǿ��� �Ʒ� �������� capsuleCollider ������ y�� �ݸ��� ������ ������ �߻� +�ణ�� ���� 
    }
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround==true)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isCrouch)
            Crouch();
        myRigid.velocity = transform.up * jumpForce;
    }
    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
           
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
           
        }
    }

    private void Running()
    {   
        isRun = true;
        applySpeed = runSpeed;
        anim.SetBool("Run", true);
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
        anim.SetBool("Run", false);
    }
    private void Move()
    {
        if(isInput)
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal");   //�¿� �Է½� 1,0,-1 return
            float _moveDirZ = Input.GetAxisRaw("Vertical");     //����,�� �Է½�


            Vector3 _moveHorizontal = transform.right * _moveDirX; //�⺻(1,0,0)�� �Էµ� X���� �����־� �¿� Ȯ��
            Vector3 _moveVertical = transform.forward * _moveDirZ;//(0,0,1)�� �Էµ� Z���� �����־� �� �Ʒ� ����

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; //��ǥ �� �ջ� �� normalized�Ͽ� ����Ƽ���� ����ϱ� ���ϰ� ��ȯ *�ӵ�

            if (_velocity == new Vector3(0, 0, 0))
            {
                anim.SetBool("Walk", false);
            }
            else
                anim.SetBool("Walk", true);
            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); //���� ��ǥ�� �ջ������� �ѹ��� �ջ��ϰ� �Ǹ� �����̵� �ϹǷ� delta���� ������ �ջ�
        }
        
       
    }
    private void CameraRotation()
    {
        if(isInput)
        {
            //���� ĳ���� ȸ��
            float _xRotation = Input.GetAxisRaw("Mouse Y");//���콺�� 2�����̹Ƿ� x,y�ۿ� ����
            float _cameraRotationX = _xRotation * lookSensitivity;
            currentCameraRotationX -= _cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
      

    }
    private void CharacterRotation()
    {
        if(isInput)
        {
            //�¿� ĳ���� ȸ��
            float _yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        }
      

    }

}
