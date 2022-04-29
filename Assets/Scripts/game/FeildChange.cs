using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FeildChange : MonoBehaviour
{
    //메인카메라
    public GameObject mainCamera;
    camera_script camera_script;

    //카메라포지션 
    public GameObject cameraPosition;
    cameraPosition_script cameraPositionScript;

    //메인플레이어
    public GameObject mainPlayer;
    player_script player_script;

    //게임매니지먼트
    public GameObject gameM;
    gameManagement gamemanagment_script;

    //스토어매니저
    public GameObject storeManager;
    storeScript store_script;

    //아이템 데이터 베이스
    public GameObject ItemDB;
    itemDatabase ItemDbScript;

    //백터기본값
    Vector3 mainPlayerDefaultVector = new Vector3(-7f, 2.74f, -13.9f);
    Vector3 cameraPositionDefaultVector = new Vector3(-7.16f, 9.075f, -26.35f);
    Vector3 mainCameraPositionDefaultVector = new Vector3(0f,0f,0f);
    Vector3 mainCameraRotationDefaultVector = new Vector3(5.3f, 0.2f, 0);

    //필드
    public GameObject[] Field;

    //필드 인덱스
    public int[] fieldIndexArray = { 1, 2, 3, 4, 5, 6 , 7 ,8 , 9};
    public int fieldIndex = 0;

    //필드텍스트
    public TextMeshProUGUI FieldNum;

    // int fieldIndex = 0;
    bool changeField = true;

    //필드바뀔때 비활성화되는 버튼
    public GameObject InventoryPanel;
    public GameObject InventroyButton;
    public GameObject compassJoyImage;

    //필드네임UI
    public TextMeshProUGUI FieldName;
    public TextMeshProUGUI BonusGold;
    public bool FieldNameUI;
    public bool FieldNameUI2;

    //게임정보
    public GameObject GameInfoButton;
    public GameObject GameInfoPanel;

    //게임옵션
    public Button GameOptionButton;
    public GameObject GameoptionPanel;

    //SkyBox
    public Material[] SkyBox;
    //Light
    public Light DirectionalLight;

    // Start is called before the first frame update
    void Start()
    {
        cameraPositionScript = cameraPosition.GetComponent<cameraPosition_script>();
        gamemanagment_script = gameM.GetComponent<gameManagement>();
        player_script = mainPlayer.GetComponent<player_script>();
        camera_script = mainCamera.GetComponent<camera_script>();
        store_script = storeManager.GetComponent<storeScript>();
        ItemDbScript = ItemDB.GetComponent<itemDatabase>();//아이템 데이터 베이스

        mixArray(fieldIndexArray);//배열셔플

        FieldNameUI = true;//처음필드
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPositionScript.fadeBlack == true)//화면가림상태
        {
            fieldIndex++;

            Field[fieldIndexArray[fieldIndex]].SetActive(true);

            //당근포인트 적립
            ItemDbScript.arrItemData[10].itemAveragePrice += ItemDbScript.carrotPoint * 2; //+당근포인트
            int FieldChangePoint = ItemDbScript.arrItemData[10].itemAveragePrice / 10 ; //필드체인지 포인트
            ItemDbScript.arrItemData[10].itemAveragePrice += FieldChangePoint; //+필드체인지 포인트
            ItemDbScript.carrotPoint = 0;//당근포인트 초기화

            //플레이어,카메라 포지션
            mainPlayer.transform.position = mainPlayerDefaultVector;
            mainPlayer.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            cameraPosition.transform.SetParent(null);
            cameraPosition.transform.position = cameraPositionDefaultVector;
            mainCamera.transform.SetParent(cameraPosition.transform);
            mainCamera.transform.localPosition = mainCameraPositionDefaultVector;
            mainCamera.transform.localEulerAngles = mainCameraRotationDefaultVector;
            player_script.step = player_script.STEP.NORMAL;
            player_script.next_step = player_script.STEP.NORMAL;

            //필드변경
            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)
            {
                Field[0].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)
            {
                Field[1].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.MH)
            {
                Field[2].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)
            {
                Field[3].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)
            {
                Field[4].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)
            {
                Field[5].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.ELL)
            {
                Field[6].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)
            {
                Field[7].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)
            {
                Field[8].SetActive(false);
            }
            else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)
            {
                Field[9].SetActive(false);
            }

            //필드별 상태변경
            switch (fieldIndexArray[fieldIndex])
            {
                case 0:
                    gamemanagment_script.town = gameManagement.Town.SHOPPINGSTREET;
                    gamemanagment_script.next_town = gameManagement.Town.SHOPPINGSTREET;
                    FieldName.text = "상점 밀집 구역" + "\n" + "무인 상점 1번가";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[0];//스카이박스변경
                    DirectionalLight.intensity = 1.0f;
                    DirectionalLight.color = new Color(255f/255f, 235f/255f, 179f/255f);
                    break;
                case 1:
                    gamemanagment_script.town = gameManagement.Town.APOLLUDE;
                    gamemanagment_script.next_town = gameManagement.Town.APOLLUDE;
                    FieldName.text = "아폴루데" + "\n" + "상점 거리";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[0];//스카이박스변경
                    DirectionalLight.intensity = 0.9f;
                    DirectionalLight.color = new Color(255f/255f, 255f/255f, 255f/255f);
                    break;
                case 2:
                    gamemanagment_script.town = gameManagement.Town.MH;
                    gamemanagment_script.next_town = gameManagement.Town.MH;
                    FieldName.text = "상인 본부" + "\n" + "거래 구역";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[2];//스카이박스변경
                    DirectionalLight.intensity = 0.6f;
                    DirectionalLight.color = new Color(187f/255f, 179f/255f, 255f/255f);
                    break;
                case 3:
                    gamemanagment_script.town = gameManagement.Town.ATLANTIA;
                    gamemanagment_script.next_town = gameManagement.Town.ATLANTIA;
                    FieldName.text = "아틀란티아" + "\n" + "상인 전용 선박";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[1];//스카이박스변경
                    DirectionalLight.intensity = 0.5f;
                    DirectionalLight.color = new Color(255f/255f, 255f/255f, 255f/255f);
                    break;
                case 4:
                    gamemanagment_script.town = gameManagement.Town.YOTAPLANT;
                    gamemanagment_script.next_town = gameManagement.Town.YOTAPLANT;
                    FieldName.text = "요타 플랜트" + "\n" + "외곽 거래 구역";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[1];//스카이박스변경
                    DirectionalLight.intensity = 0.5f;
                    DirectionalLight.color = new Color(212f / 255f, 145f / 255f, 83f / 255f);
                    break;
                case 5:
                    gamemanagment_script.town = gameManagement.Town.AMBRAYLOSE;
                    gamemanagment_script.next_town = gameManagement.Town.AMBRAYLOSE;
                    FieldName.text = "엠브라이로스" + "\n" + "입구 상점 구역";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[0];//스카이박스변경
                    DirectionalLight.intensity = 0.7f;
                    DirectionalLight.color = new Color(255f/255f, 255f/255f, 255f/255f);
                    break;
                case 6:
                    gamemanagment_script.town = gameManagement.Town.ELL;
                    gamemanagment_script.next_town = gameManagement.Town.ELL;
                    FieldName.text = "엘" + "\n" + "거래 허가 구역";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[5];//스카이박스변경
                    DirectionalLight.intensity = 0.8f;
                    DirectionalLight.color = new Color(213f/255f, 222f/255f, 255f/255f);
                    break;
                case 7:
                    gamemanagment_script.town = gameManagement.Town.CROWALLEY;
                    gamemanagment_script.next_town = gameManagement.Town.CROWALLEY;
                    FieldName.text = "크로우 뒷골목" + "\n" + "암시장";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[3];//스카이박스변경
                    DirectionalLight.intensity = 0.3f;
                    DirectionalLight.color = new Color(130f/255f, 130f/255f, 130f/255f);
                    break;
                case 8:
                    gamemanagment_script.town = gameManagement.Town.TRAINSTATION;
                    gamemanagment_script.next_town = gameManagement.Town.TRAINSTATION;
                    FieldName.text = "기차역" + "\n" + "상인 전용 플랫폼";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[4];//스카이박스변경
                    DirectionalLight.intensity = 0.3f;
                    DirectionalLight.color = new Color(91f/255f, 91f/255f, 91f/255f);
                    break;
                case 9:
                    gamemanagment_script.town = gameManagement.Town.ARTEDE;
                    gamemanagment_script.next_town = gameManagement.Town.ARTEDE;
                    FieldName.text = "아르테데" + "\n" + "시장 1번가";
                    FieldNum.text = fieldIndex + 2 + "/7";
                    mainCamera.GetComponent<Skybox>().material = SkyBox[2];//스카이박스변경
                    DirectionalLight.intensity = 1.0f;
                    DirectionalLight.color = new Color(235f/255f, 255f/255f, 235f/255f);
                    break;
            }

            camera_script.camera_position_y = 0;

            //스테이지 변환 완료 -> 상점갱신
            store_script.storeItemSet = true;

            //스테이집 변환 완료 -> 모든카메라 이동중지
            camera_script.camera_potation_x_arrive = true;
            camera_script.camera_potation_y_arrive = true;
            camera_script.camera_potation_z_arrive = true;
            camera_script.camera_rotation_x_arrive = true;
            camera_script.camera_rotation_y_arrive = true;
            camera_script.camera_rotation_z_arrive = true;

            //한번만실행
            cameraPositionScript.fadeBlack = false;
        }

        if (FieldNameUI == true)
        {
            FieldName.gameObject.SetActive(true);
            FieldName.alpha += Time.deltaTime * 0.6f;
            if (fieldIndex + 2 != 1)//첫번째스테이지
            {
                BonusGold.gameObject.SetActive(true);
                BonusGold.alpha += Time.deltaTime * 0.6f;
            }
            if (FieldName.alpha >= 1.5)
            {
                FieldNameUI2 = true;
                FieldNameUI = false;//실행종료
            }
        }
        if (FieldNameUI2 == true)
        {
            FieldName.alpha -= Time.deltaTime * 0.8f;
            if (fieldIndex + 2 != 1)//첫번째스테이지
            {
                BonusGold.alpha -= Time.deltaTime * 0.8f;
            }
            if (FieldName.alpha <= 0)
            {
                FieldName.alpha = 0.0f;
                BonusGold.alpha = 0.0f;
                FieldNameUI2 = false;//실행종료
            }
        }
    }

    void mixArray(int [] fieldIndexArray)//인덱스 배열에서 숫자섞기
    {
        int random1;
        int random2;

        int tmp;

        for (int i = 0; i < 7; i++)
        {
            random1 = Random.Range(0, fieldIndexArray.Length);
            random2 = Random.Range(0, fieldIndexArray.Length);

            tmp = fieldIndexArray[random1];
            fieldIndexArray[random1] = fieldIndexArray[random2];
            fieldIndexArray[random2] = tmp;
        }
    }

    public void MainScreenButtonOff()//버튼 비활성화
    {
        compassJoyImage.GetComponent<Image>().enabled = false;
        InventroyButton.GetComponent<Button>().interactable = false;
        GameInfoButton.GetComponent<Button>().interactable = false;
        GameOptionButton.interactable = false;
        

        if (InventroyButton.gameObject.activeSelf == true)
        {
            InventoryPanel.SetActive(false);
        }
        if (GameInfoPanel.gameObject.activeSelf == true)
        {
            GameInfoPanel.SetActive(false);
        }
        if (GameoptionPanel.gameObject.activeSelf == true)
        {
            GameoptionPanel.SetActive(false);
        }
    }
    public void MainScreenButtonOn()//버튼 활성화
    {
        compassJoyImage.GetComponent<Image>().enabled = true;
        InventroyButton.GetComponent<Button>().interactable = true;
        GameInfoButton.GetComponent<Button>().interactable = true;
        GameOptionButton.interactable = true;
    }
}
