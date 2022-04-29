using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cJoyStick : MonoBehaviour
{
    [Header("Camera")] 
    public GameObject cameraPosition; //메인카메라 부모오브젝트
    public GameObject main_camera;    //메인카메라
    camera_script camera_script;      //메인카메라 스크립트

    [Header("JoyStick")]
    public Image Joy; //조이스틱 조작 이미지
    private Vector3 Pos; //
    float StickDIstance = 0; //조이스틱이 갈수 있는 범위

    Vector3 dir;
    float MoveDistance;

    public bool MoveChek = false;

    //메인플레이어
    public GameObject Player;
    player_script player_script;
    public bool MoveChekRun = false;
    public bool MoveChekWalk = false;

    //속도
    public float speed;
    float screenSpeed = 0;
    public float static_speed = 0;
    public float speed2 = 0;
    void Start()
    {
        Pos = Joy.transform.position;
       // Debug.Log(Pos);

        //해상도별 //1920 * 1080 -> 0.43
        StickDIstance = Joy.rectTransform.sizeDelta.x  * (Screen.height * 0.0004f); 
        //sizeDelta는 앵커사이에 따른 사각형의 크기
        //카메라스크립트
        camera_script = main_camera.GetComponent<camera_script>();
        //플레이어스크립트
        player_script = Player.GetComponent<player_script>();
        //초기속도  해상도별 // 1920 * 1080 -> 0.13
        speed = 0.16f;
    }
    void Update()
    {
        if (MoveChek && player_script.step != player_script.STEP.NOACTION && player_script.step != player_script.STEP.SHOPPING)//이동중
        {
            //Vector3 PlayerMove = new Vector3(dir.x, 0, dir.y);
            //Player.transform.Translate(PlayerMove * MoveDistance / 100 * Time.deltaTime);
            

            if (Player.GetComponent<player_script>().Col_Check.Equals(false))
            {
                static_speed = 20f * speed;
                if (MoveDistance > Screen.height * 0.03)
                    static_speed = 30f * speed;
                if (MoveDistance > Screen.height * 0.04)
                    static_speed = 40f * speed;
                if (MoveDistance > Screen.height * 0.05)
                    static_speed = 50f * speed;
                if (MoveDistance > Screen.height * 0.06)
                    static_speed = 60f * speed;
                if (MoveDistance > Screen.height * 0.07)
                    static_speed = 70f * speed;
                if (MoveDistance > Screen.height * 0.08)
                    static_speed = 80f * speed;
                if (MoveDistance > Screen.height * 0.09)
                    static_speed = 90f * speed;
                if (MoveDistance > Screen.height * 0.10)
                    static_speed = 100f * speed;
                if (MoveDistance > Screen.height * 0.11)
                    static_speed = 110f * speed;

                Player.transform.Translate(Vector3.forward * static_speed * Time.deltaTime);
            }
            else if (Player.GetComponent<player_script>().Col_Check.Equals(true))
            {
                Player.transform.Translate(Vector3.forward * MoveDistance / 60f * Time.deltaTime);
            }
        }
        else
        {
            MoveChekRun = false;
            MoveChekWalk = false;
        }

        if (player_script.step == player_script.STEP.MOVE)
        {
            if (camera_script.move_camera == false)
                cameraPosition.transform.SetParent(Player.transform);
        }
        speed2 = MoveDistance * speed;
    }

    /// <summary>
    /// 조이스틱 터치로 메인플레이어 이동 구현
    /// </summary>
    public void Drag_Mobile()
    {
        MoveChek = true;

        if (Joy == null)
            return;

        //마우스의 x,y좌표 받아오기
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //싱글터치
        Touch touch = Input.GetTouch(0);

        //터치한 x,y좌표 받아오기
        Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, Pos.z);

        //움직인방향 - 조이스틱이 원래 있었던 방향 = 조이스틱이 움직인방향
        dir = (touchPos - Pos).normalized;

        //이동속도를 정하기위해 조이스틱이 움직인 거리구하기
        float touchDistance = Vector3.Distance(Pos, touchPos);                         

        MoveDistance = Vector3.Distance(Pos, MousePos);

        //예외처리
        if (touchDistance > 50000f)
            touchDistance = 110f;

        //속도에따라 WALK RUN판단

        //Run
        if (touchDistance > 80f)
        {
            MoveChekRun = true;
            MoveChekWalk = false;
        }
        //Walk
        else
        {
            MoveChekRun = false;
            MoveChekWalk = true;
        }

        //조이스틱 위치 조정 함수 호출
        JoyPosAdjust(touchDistance, touch);

        //플레이어 회전처리 함수 호출
        RotationPlayer();
    }

    /// <summary>조이스틱 위치 조정</summary>
    /// <param name="touchDistance">터치 드래그로 조이스틱이 움직인 범위</param>
    /// <param name="touch">터치정보</param>
    void JoyPosAdjust(float touchDistance,Touch touch)
    {
        //조이스틱이 반경을 넘어갈경우
        if (touchDistance > StickDIstance) 
        {
            //정해진 범위로 돌아가도록 조이스틱 이미지 위치 조정
            Joy.rectTransform.position = Pos + (dir * StickDIstance); 
        }
        else
        {
            //반경내일경우 드래그 된 위치로 조이스틱 이미지 위치 조정
            Joy.rectTransform.position = touch.position;              
        }

    }


    ///<summary>플레이어 회전처리</summary>
    void RotationPlayer()
    {
        //플레이어가 이동불가인 상태가 아닐경우
        if (player_script.step != player_script.STEP.NOACTION && player_script.step != player_script.STEP.SHOPPING)
        {
            //카메라는 같이 회전하지 않으므로 종속 해제
            cameraPosition.transform.SetParent(null);

            //플레이어 y축 회전
            Player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, 0);
            
            //회전이후 카메라 부모 재설정
            if (camera_script.move_camera == false)
                cameraPosition.transform.SetParent(Player.transform);
        }
    }

    public void Drag_PC()
    {
        MoveChek = true;//움직임 감지 플래그

        if (Joy == null)
            return;

        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);//마우스의 x,y좌표 받아오기

        dir = (MousePos - Pos).normalized;
        //조이스틱이 움직인 방향

        MoveDistance = Vector3.Distance(Pos, MousePos);
        if (MoveDistance > 50000)
        {
            MoveDistance = 110;
            Debug.Log("정지");
        }

        //속도에따라 WALK RUN판단
        if (MoveDistance > 80)
        {
            MoveChekRun = true;
            MoveChekWalk = false;
        }
        else
        {
            MoveChekRun = false;
            MoveChekWalk = true;
        }

        //조이스틱이 얼마나 움직였는지 체크
        if (MoveDistance > StickDIstance)
        {
            Joy.rectTransform.position = Pos + (dir * StickDIstance);
            //반경을 넘어갈 경우 방향으로 정해진 길이만 가도록 설정           
        }
        else
        {
            Joy.transform.position = MousePos;
        }

        //회전처리
        if (player_script.step != player_script.STEP.NOACTION && player_script.step != player_script.STEP.SHOPPING)//이동중
        {
            cameraPosition.transform.SetParent(null);
            Player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, 0);

            if (camera_script.move_camera == false)
                cameraPosition.transform.SetParent(Player.transform);
        }
        
        //eulerAngles : 각도 단위로 오일러 각도로 회전
        //Mathf.Atan2 : 각도를 탄젠트 y/x인 라디안으로 반환합니다.
        //Mathf.Rad2Deg : 라디안에서 하위 수준으로의 변환 상수
    }

    /// <summary>
    /// 플레이어 이동종료
    /// </summary>
    public void EndDrag()
    {
        MoveChek = false;

        if (Joy != null)
        {
            //터치가 끝나면 조이스틱 원위치
            Joy.rectTransform.position = Pos;
        }
    }
}
