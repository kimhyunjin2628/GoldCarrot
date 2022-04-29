using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    //애니메이터
    Animator player_animator;
    //컬라이더
    Collider player_collider;

    //스토어패널UI
    public GameObject StorePanel;

    //옥션패널UI
    public GameObject AuctionPanel;
    //스크립트참조

    //게임매니지먼트
    public GameObject gameM;
    gameManagement gamemanagment_script;

    //조이스틱
    public GameObject joysctick;
    cJoyStick cjoystick_script;

    //카메라포지션
    public GameObject cameraPosition;
    cameraPosition_script cameraPosition_script;

    //메인카메라
    public GameObject mainCamera;
    camera_script camera_script;

    //ButtonScript
    public GameObject ButtonScriptGameObject;
    ButtonScript buttonScript;

    //FeildChange
    public GameObject FeildChange;
    FeildChange FeildChangeScript;


    //플레이어모자
    public GameObject[] PlayerHat = new GameObject[6];

    //store포인터_ShoppingStreet
    public GameObject StorePointer_ShoppingStreet_P_SH;//쇼핑거리
    public GameObject StorePointer_ShoppingStreet_P_AP;//아폴루데
    public GameObject StorePointer_MH_P_AP;//상점본부
    public GameObject StorePointer_Atlantia_AP;//아틀란티아
    public GameObject StorePointer_YotaPlant_P_AP;//요타플랜트
    public GameObject StorePointer_Ambraylose_P_AP;//앰브레이로스
    public GameObject StorePointer_ELL_P_AP;//엘
    public GameObject StorePointer_CROWALLEY_P_AP;//크로우뒷골목암시장
    public GameObject StorePointer_TRAINSTATION_P_AP;//상인전용기차역
    public GameObject StorePointer_ARTEDE_P_AP;//아르테데
    public GameObject[] StorePointer_ShoppingStreet;
    public Material StorePointerRed;//스토어포인터 레드메트리얼

    //HJPointer
    public GameObject HJPointer_1; //ShoppingStreet
    public GameObject HJPointer_2; //Appolude
    public GameObject HJPointer_3; //MH
    public GameObject HJPointer_4; //Atlantia
    public GameObject HJPointer_5; //YotaPlant
    public GameObject HJPointer_6; //Ambraylose
    public GameObject HJPointer_7; //ELL
    public GameObject HJPointer_8; //CrowAlley
    public GameObject HJPointer_9; //TrainStation
    public GameObject HJPointer_10; //Artede

    //AuctionPointer
    public GameObject AuctionPointer_1; //ShoppingStreet
    public GameObject AuctionPointer_2; //Appolude
    public GameObject AuctionPointer_3; //MH
    public GameObject AuctionPointer_4; //Atlantia
    public GameObject AuctionPointer_5; //YotaPlant
    public GameObject AuctionPointer_6; //Ambraylose
    public GameObject AuctionPointer_7; //ELL
    public GameObject AuctionPointer_8; //CrowAlley
    public GameObject AuctionPointer_9; //TrainStation
    public GameObject AuctionPointer_10; //Artede
    //Store 관리 스크립트
    public GameObject storeManager;
    storeScript storeScript;

    //초기화값
    Vector3 defaultMainCameraP;
    Vector3 defaultMainCameraR;

    //동작

    //벽컬라이더 충돌판정
    public bool Col_Check;

    //현재 쇼핑중인 상점정보
    GameObject currentStorePointer;

    //NORMAL상태 랜덤애니메이션 재생
    public float normalTime;

    //가방
    public GameObject bagLv1;
    public GameObject bagLv2;
    public GameObject bagLv3;
    public int currentBagNum = 1;

    //플레이어
    public enum STEP
    {
        //기본IDLE상태
        NORMAL,
        
        //Walk or Run 이동중인 상태
        MOVE,//1

        //아무것도 동작하면 안되는 상태
        NOACTION,//2

        //상점에 들어간 상태
        SHOPPING,//3

        //경매장에 들어간 상태
        AUCTION//4
    };

    //레이캐스트
    RaycastHit hit;
    public float ray_distance = 0.0f;//플레이어와 지면사이의 거리
    Vector3 Rayvec;

    //아틀란티아벽
    public GameObject atlantiaWall;

    public STEP step = STEP.NORMAL;
    public STEP next_step = STEP.NORMAL;

    //애니메이션 플래그
    bool normal;//기본
    bool walk;//걷기
    bool run;//뛰기
    bool normalA1;//기본상태 애니메이션1
    bool normalA2;//기본상태 애니메이션2

    //오디오
    new public AudioSource audio;
    public AudioClip stepChangeSound;
    public AudioClip MoveSound;
    public AudioClip WaterSound;

    // Start is called before the first frame update
    void Start()
    {
        Col_Check = false;
        //컴포넌트
        player_animator = GetComponent<Animator>();
        player_collider = GetComponent<CapsuleCollider>();

        //스크립트
        cjoystick_script = joysctick.GetComponent<cJoyStick>();//조이스틱
        camera_script = mainCamera.GetComponent<camera_script>();//메인카메라
        cameraPosition_script = cameraPosition.GetComponent<cameraPosition_script>();//카메라포지션
        storeScript = storeManager.GetComponent<storeScript>();//상점관리스크립트
        buttonScript = ButtonScriptGameObject.GetComponent<ButtonScript>();//버튼스크립트
        gamemanagment_script = gameM.GetComponent<gameManagement>();//게임매니저 스크립트
        FeildChangeScript = FeildChange.GetComponent<FeildChange>();//필드체인지 스크립트

        //카메라위치 기본값 (메인카메라,카메라포지션)
        defaultMainCameraP = new Vector3(0f, 0f, 0f);
        defaultMainCameraR = new Vector3(5.3f, 0.2f, 0f);

        //오디오
        audio = GetComponent<AudioSource>();

        //게임 스타트씬에서 정보받아와 모자변경
        PlayerChangeHat();
    }

    // Update is called once per frame
    void Update()
    {
        //레이캐스트
        Rayvec = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
        Debug.DrawRay(this.transform.position + this.transform.forward * 0.03f + this.transform.right * 0.02f, -this.transform.up * 20f, Color.red);


        if (Physics.Raycast(this.transform.position + this.transform.forward * 0.03f + this.transform.right * 0.02f, -this.transform.up, out hit, 350f))
        {
            if (this.hit.collider.gameObject.tag.Equals("NextStage"))//다음스테이지로 이동 쇼핑스트리트->다음스테이지(게임중 한번만실행)
            {
                //오디오
                audio.clip = WaterSound;
                audio.loop = false;
                audio.volume = 1.2f;
                audio.pitch = 0.7f;
                audio.Play();

                //한번만실행
                this.hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;//상대 컬라이더 off
                cameraPosition_script.cameraMoveAgent = true;//카메라 이동시작

                //필드체인지
                FeildChangeScript.MainScreenButtonOff();//버튼비활성화

                //메인카메라이동
                cameraPosition.transform.SetParent(null);
                this.next_step = STEP.NOACTION;

                camera_script.r_destination = new Vector3(60f,1f,1f);//회전좌표
                camera_script.t_camera_arrive = true;//카메라포지션은이동X

                camera_script.camera_potation_x_arrive = true;//x축이동X
                camera_script.camera_potation_y_arrive = false;//y축이동O
                camera_script.camera_potation_z_arrive = true;//z축이동X

                camera_script.MoveOnlyCamera(this.mainCamera.transform.localPosition,new Vector3(this.mainCamera.transform.localPosition.x, 60.0f, this.mainCamera.transform.localPosition.x));//메인카메라이동

                camera_script.MoveCamera(this.mainCamera.transform.localEulerAngles, camera_script.r_destination, 2.0f, 2);//카메라회전
            }
            if (this.hit.collider.gameObject.tag.Equals("StorePointer"))//(쇼핑스트리트 StorePointer)
            {
                //오디오
                audio.clip = stepChangeSound;
                audio.loop = false;
                audio.volume = 0.46f;
                audio.pitch = 0.8f;
                audio.Play();

                //한번 입장한 포인터 더이상 입장불가
                if (hit.collider.gameObject.transform.GetChild(1).name != "30")
                {
                    hit.collider.gameObject.transform.parent.transform.GetChild(0).GetComponent<MeshRenderer>().material = StorePointerRed;
                    hit.collider.GetComponent<BoxCollider>().enabled = false;
                }

                //패널 on
                StorePanel.SetActive(true);
                //버튼비활성화
                buttonScript.StoreButtonEnableOff();
                //현재위치상점
                storeScript.storePointerIndex = int.Parse(this.hit.collider.gameObject.transform.GetChild(1).name);
                //상점입장 플래그
                storeScript.storeEnter = true;
                 
                //상점스크롤 맨위로
                buttonScript.StoreButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(3f, 1f, 0f);//Store
                buttonScript.MyStorePanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(3f, 1f, 0f);//MyStore

                //한번만실행

                if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//쇼핑거리
                {
                    StorePointer_ShoppingStreet_P_SH.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_1.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)//아폴루데
                {
                    StorePointer_ShoppingStreet_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_2.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.MH)//MH
                {
                    StorePointer_MH_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_3.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)//ATLANTIA
                {
                    StorePointer_Atlantia_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_4.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)//YOTAPLANT
                {
                    StorePointer_YotaPlant_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_5.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)//AMBRAYLOSE
                {
                    StorePointer_Ambraylose_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_6.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.ELL)//ELL
                {
                    StorePointer_ELL_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_7.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)//CROWALLEY
                {
                    StorePointer_CROWALLEY_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_8.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)//TRAINSTATION
                {
                    StorePointer_TRAINSTATION_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_9.SetActive(false);
                    }
                }
                else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)//TRAINSTATION
                {
                    StorePointer_ARTEDE_P_AP.SetActive(false);
                    currentStorePointer = this.hit.collider.gameObject;
                    if (storeScript.storePointerIndex == 30)//HJPointer
                    {
                        HJPointer_10.SetActive(false);
                    }
                }


                //카메라이동
                cameraPosition.transform.SetParent(null);
                this.next_step = STEP.SHOPPING;
                this.player_collider.enabled = false;//컬라이더 잠시해제

                camera_script.r_destination = hit.collider.gameObject.transform.localEulerAngles;//회전좌표
                camera_script.t_destination = hit.collider.gameObject.transform.localPosition;//카메라포지션 이동좌표
                camera_script.camera_potation_y_arrive = true;//y축은고정(navmeshAgent_offset)

                camera_script.MoveCamera(this.mainCamera.transform.localEulerAngles, camera_script.r_destination, 1.0f, 3);//카메라회전
            }

            if (this.hit.collider.gameObject.tag.Equals("AuctionPointer"))//Auction
            {
                //오디오
                audio.clip = stepChangeSound;
                audio.loop = false;
                audio.volume = 0.46f;
                audio.pitch = 0.8f;
                audio.Play();

                //패널 on
                AuctionPanel.SetActive(true);
                //버튼비활성화
                buttonScript.AuctionButtonEnableOff();
                //스크롤맨위고정
                buttonScript.InventoryPanel2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-36.184f, -500f, 0f);//Store

                if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//쇼핑거리
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_1.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)//아폴루데
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_2.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.MH)//상인본부
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_3.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)//아틀란티아
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_4.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)//요타플랜트
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_5.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)//엠브라이로스
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_6.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.ELL)//엘
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_7.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)//크로우뒷골목 암시장
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_8.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)//기차역
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_9.SetActive(false);
                }
                if (gamemanagment_script.town == gameManagement.Town.ARTEDE)//아르테데
                {
                    currentStorePointer = this.hit.collider.gameObject;
                    AuctionPointer_10.SetActive(false);
                }
                //카메라이동
                cameraPosition.transform.SetParent(null);
                this.next_step = STEP.AUCTION;
                this.player_collider.enabled = false;//컬라이더 잠시해제

                camera_script.r_destination = hit.collider.transform.parent.gameObject.transform.localEulerAngles;//회전좌표
                camera_script.t_destination = hit.collider.transform.parent.gameObject.transform.localPosition;//카메라포지션 이동좌표
                camera_script.camera_potation_y_arrive = true;//y축은고정(navmeshAgent_offset)

                camera_script.MoveCamera(this.mainCamera.transform.localEulerAngles, camera_script.r_destination, 1.0f, 4);//카메라회전
            }
        }
        

        //상태변환
        if (step == STEP.NORMAL)
        {
            //이동가능 NORMAL -> WALK
            if (cjoystick_script.MoveChek == true)
            {
                //오디오
                audio.clip = MoveSound;
                audio.loop = true;
                audio.volume = 0.1f;
                audio.pitch = 0.83f;
                audio.Play();

                this.next_step = STEP.MOVE;
            }

            //계속normal상태일경우 랜덤애니메이션재생
            if (normalTime > 6.0f)
            {
                if (Random.Range(0, 2) == 1)
                    normalA1 = true;
                else
                    normalA2 = true;

                normalTime = 0.0f;
            }
            else
            {
                normalA1 = false;
                normalA2 = false;
            }
        }
        else if (step == STEP.MOVE)
        {
            //이동정지 WALK -> NORMAL
            if (cjoystick_script.MoveChek == false)
            {
                //오디오
                audio.Stop();

                this.next_step = STEP.NORMAL;
            }
        }

        if (step != STEP.NORMAL)//normal상태이외
            normalTime = 0.0f;

        //쇼핑중
        if (step == STEP.SHOPPING)
        {
            //쇼핑해제(한번만실행)
            if (buttonScript.StoreExit2 == true)
            {
                //이동불가
                //joy.SetActive(false);

                //exitPosition
                this.transform.position = currentStorePointer.transform.GetChild(0).transform.localPosition;
                this.transform.localEulerAngles = currentStorePointer.transform.GetChild(0).transform.localEulerAngles;

                //상태초기화
                //한번만실행
                if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//쇼핑거리
                {
                    StorePointer_ShoppingStreet_P_SH.SetActive(true);
                    HJPointer_1.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)//아폴루데
                {
                    StorePointer_ShoppingStreet_P_AP.SetActive(true);
                    HJPointer_2.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.MH)//아폴루데
                {
                    StorePointer_MH_P_AP.SetActive(true);
                    HJPointer_3.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)//아폴루데
                {
                    StorePointer_Atlantia_AP.SetActive(true);
                    HJPointer_4.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)//YOTAPLANT
                {
                    StorePointer_YotaPlant_P_AP.SetActive(true);
                    HJPointer_5.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)//AMBRAYLOSE
                {
                    StorePointer_Ambraylose_P_AP.SetActive(true);
                    HJPointer_6.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ELL)//ELL
                {
                    StorePointer_ELL_P_AP.SetActive(true);
                    HJPointer_7.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)//CROWALLEY
                {
                    StorePointer_CROWALLEY_P_AP.SetActive(true);
                    HJPointer_8.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)//TRAINSTATION
                {
                    StorePointer_TRAINSTATION_P_AP.SetActive(true);
                    HJPointer_9.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)//ARTEDE
                {
                    StorePointer_ARTEDE_P_AP.SetActive(true);
                    HJPointer_10.SetActive(true);
                }

                this.next_step = STEP.NOACTION;
                this.player_collider.enabled = true;//컬라이더 on

                camera_script.r_destination = defaultMainCameraR;//회전좌표
                camera_script.t_destination = new Vector3(this.transform.localPosition.x, 10f, this.transform.localPosition.z - 14f);//카메라포지션 이동좌표
                camera_script.camera_potation_y_arrive = true;//y축은고정(navmeshAgent_offset)

                camera_script.MoveCamera(this.mainCamera.transform.localEulerAngles, camera_script.r_destination, 1.0f, 0);//카메라회전   

                //한번만실행
                buttonScript.StoreExit2 = false;
            }
        }

        //쇼핑중
        if (step == STEP.AUCTION)
        {
            //쇼핑해제(한번만실행)
            if (buttonScript.AuctionExit2 == true)
            {
                //이동불가
                //joy.SetActive(false);

                //exitPosition
                this.transform.position = currentStorePointer.transform.parent.transform.GetChild(0).transform.localPosition;
                this.transform.localEulerAngles = currentStorePointer.transform.parent.transform.GetChild(0).transform.localEulerAngles;

                //상태초기화
                //한번만실행
                if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//쇼핑거리
                {
                    AuctionPointer_1.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)//아폴루데
                {
                    AuctionPointer_2.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.MH)//상인본부
                {
                    AuctionPointer_3.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)//아틀란티아
                {
                    AuctionPointer_4.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)//YOTAPLANT
                {
                    AuctionPointer_5.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)//AMBRAYLOSE
                {
                    AuctionPointer_6.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ELL)//ELL
                {
                    AuctionPointer_7.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)//CROWALLEY
                {
                    AuctionPointer_8.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)//TRAINSTATION
                {
                    AuctionPointer_9.SetActive(true);
                }
                else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)//ARTEDE
                {
                    AuctionPointer_10.SetActive(true);
                }

                this.next_step = STEP.NOACTION;
                this.player_collider.enabled = true;//컬라이더 on

                camera_script.r_destination = defaultMainCameraR;//회전좌표
                camera_script.t_destination = new Vector3(this.transform.localPosition.x, 10f, this.transform.localPosition.z - 14f);//카메라포지션 이동좌표
                camera_script.camera_potation_y_arrive = true;//y축은고정(navmeshAgent_offset)

                camera_script.MoveCamera(this.mainCamera.transform.localEulerAngles, camera_script.r_destination, 1.0f, 0);//카메라회전   

                //한번만실행
                buttonScript.AuctionExit2 = false;
            }
        }

        //아틀란티아
        if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)
        {
            if (this.transform.localPosition.z > 85.0f)
            {
                if (this.step == STEP.SHOPPING || this.step == STEP.NOACTION)
                {
                    atlantiaWall.GetComponent<MeshRenderer>().enabled = true;
                }else
                atlantiaWall.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                atlantiaWall.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        //플레이어 애니메이션 변경
        SetAnim();
        ChangeAnim();

        //동작관리
        step = next_step;
    }

    /// <summary>
    /// 플레이어 상태에 따라 애니메이션 설정 
    /// </summary>
    public void SetAnim()
    {
        //플레이어 상태별 동작
        switch (step)
        {
            //기본 IDLE 상태
            case STEP.NORMAL:
                //NORMALTIME
                normalTime += Time.deltaTime;
                //애니메이션 플래그
                normal = true;
                walk = false;
                run = false;
                break;

            //Run or Walk 이동중인 상태
            case STEP.MOVE:
                //애니메이션 플래그
                normal = false;
                normalA1 = false;
                normalA2 = false;
                if (cjoystick_script.MoveChekRun == true)//RUN
                {
                    run = true;
                    walk = false;
                }
                else                                    //Walk
                {
                    run = false;
                    walk = true;
                }
                break;

            //아무것도 동작하면 안되는 상태
            case STEP.NOACTION:
            case STEP.SHOPPING:
            case STEP.AUCTION:
                //애니메이션 플래그
                normal = true;
                walk = false;
                run = false;
                normalA1 = false;
                normalA2 = false;
                break;
        }
    }

    /// <summary>
    /// 플레이어 상태에 따라 애니메이션 변경
    /// </summary>
    public void ChangeAnim()
    {
        //애니메이션 관리
        player_animator.SetBool("NORMAL", normal);
        player_animator.SetBool("WALK", walk);
        player_animator.SetBool("RUN", run);
        player_animator.SetBool("NORMALA1", normalA1);
        player_animator.SetBool("NORMALA2", normalA2);
    }

    public void PlayerChangeHat()//모자 바꾸기
    {
        //Debug.Log("sakldjsakdljsdakl");
        PlayerHat[StartSceneInfo.hatIndex].SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag.Equals("Wall"))
        {
            Col_Check = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag.Equals("Wall"))
        {
            Col_Check = false;
        }
    }

}
