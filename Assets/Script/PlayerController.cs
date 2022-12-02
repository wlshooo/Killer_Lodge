using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   //스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;
    private float applyCrouchPosY; //이 변수를 통해 crouch 컨트롤

    [SerializeField]
    private float jumpForce;
    //상태변수
    private bool isCrouch = false; //앉아있는지 아닌지 체크
    private bool isRun = false;
    private bool isGround = true; //땅에 있는지 체크
    public bool isInput = true; //패스워드 입력중일시-false
    
    [SerializeField]
    private float crouchPosY;  // 앉았을때 얼마나 앉을지 결정하는 변수
    private float originPosY;   //서 있었을때의 수치를 백업해놀 변수

    private CapsuleCollider capsuleCollider; //지형과 붙어 있는지 판단


    [SerializeField]
    private float lookSensitivity;  //카메라 민감도

    [SerializeField]
    private float cameraRotationLimit;  //카메라 각도 한계치
    private float currentCameraRotationX = 0f;

    //필요한 컴퍼논트
    [SerializeField]
    private Camera theCamera;   //카메라가 플레이어의 자식 객체로 들어가있기 때문에, 또한 카메라가 한개만 있는게 아닐 수 있으므로

    private Rigidbody myRigid;


    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;   //플레이어 대신 카메라의 y를 내림 이때 카메라가 플레이어 안에 속해 있으므로 상대적인 변수
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
    IEnumerator CrouchCoroution()   //순차적으로 이동하는 함수의 코드와 달리 코루틴을 만나게 되면 코루틴과 병렬적으로 실행가능하다.
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        while(_posY!=applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.5f);//보간 함수 ->매개변수1에서 매개변수2까지 매개변수3의 비율로 증가한다.
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;   //null -> 1프레임마다 대기
        }
        theCamera.transform.localPosition = new Vector3(0f, applyCrouchPosY, 0f);
      
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y +0.1f);
        //플레이어의 포지션에서 아래 방향으로 capsuleCollider 사이즈 y의 반만한 길이의 레이저 발사 +약간의 여유 
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
            float _moveDirX = Input.GetAxisRaw("Horizontal");   //좌우 입력시 1,0,-1 return
            float _moveDirZ = Input.GetAxisRaw("Vertical");     //정면,뒤 입력시


            Vector3 _moveHorizontal = transform.right * _moveDirX; //기본(1,0,0)에 입력된 X값을 곱해주어 좌우 확인
            Vector3 _moveVertical = transform.forward * _moveDirZ;//(0,0,1)에 입력된 Z값을 곱해주어 위 아래 구분

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; //좌표 값 합산 후 normalized하여 유니티에서 계산하기 편하게 변환 *속도

            if (_velocity == new Vector3(0, 0, 0))
            {
                anim.SetBool("Walk", false);
            }
            else
                anim.SetBool("Walk", true);
            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); //현재 좌표에 합산하지만 한번에 합산하게 되면 순간이동 하므로 delta으로 나누어 합산
        }
        
       
    }
    private void CameraRotation()
    {
        if(isInput)
        {
            //상하 캐릭터 회전
            float _xRotation = Input.GetAxisRaw("Mouse Y");//마우스는 2차원이므로 x,y밖에 없다
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
            //좌우 캐릭터 회전
            float _yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        }
      

    }

}
