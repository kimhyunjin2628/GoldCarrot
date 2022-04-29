using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cameraPosition_script : MonoBehaviour
{
    //메인플레이어
    public GameObject main_player;
    player_script player_script;
    public GameObject[] carrotShip;//1.쇼핑거리 2.아폴루데 3.상인본부 4.아틀란티아 5.요타플랜트 6.앰브라이로스 7.엘 8.크로우뒷골목 9.상인전용 기차역

    //게임매니지먼트
    public GameObject gameM;
    gameManagement gamemanagment_script;

    //BlackScreen
    public Image BlackScreen;//페이드인,아웃용 스크린
    float AlphaBlack;//BlackScreen투명도
    bool fadeOut = false;//페이드아웃 상태
    public bool fadeIn = false;//페이드인 상태
    public bool fadeBlack = false;//화면검정색 -> AlphaBlack == 255상태

    //메인카메라
    public GameObject main_camera;
    camera_script camera_script;

    //FeildChange
    public GameObject FeildChange;
    FeildChange FeildChangeScript;
    public GameObject laststageText1;
    public GameObject laststageText2;

    //카메라이동
    public bool cameraMoveAgent = false;
    //도착좌표
    public GameObject Destination1;
    int DesIndex = 0;
    //도착
    bool arrive;
    
    // Start is called before the first frame update
    void Start()
    {
        player_script = main_player.GetComponent<player_script>();//플레이어
        camera_script = main_camera.GetComponent<camera_script>();//메인카메라스크립트
        gamemanagment_script = gameM.GetComponent<gameManagement>();//게임매니지먼트
        FeildChangeScript = FeildChange.GetComponent<FeildChange>();//필드체인지 스크립트
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("목표지점 " + agent.destination + "거리" + Vector3.Distance(this.transform.position, agent.destination) + "목표지점");

        if (cameraMoveAgent.Equals(true))
        {
            player_script.next_step = player_script.STEP.NOACTION; //플레이어이동불가
            arrive = false;//도착 미완료

            //carrotShip이동
            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)
            {
                this.transform.SetParent(carrotShip[0].transform.GetChild(0));//상속
                carrotShip[0].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)
            {
                this.transform.SetParent(carrotShip[1].transform.GetChild(0));//상속
                carrotShip[1].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.MH)
            {
                this.transform.SetParent(carrotShip[2].transform.GetChild(0));//상속
                carrotShip[2].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)
            {
                this.transform.SetParent(carrotShip[3].transform.GetChild(0));//상속
                carrotShip[3].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)
            {
                this.transform.SetParent(carrotShip[4].transform.GetChild(0));//상속
                carrotShip[4].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)
            {
                this.transform.SetParent(carrotShip[5].transform.GetChild(0));//상속
                carrotShip[5].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.ELL)
            {
                this.transform.SetParent(carrotShip[6].transform.GetChild(0));//상속
                carrotShip[6].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)
            {
                this.transform.SetParent(carrotShip[7].transform.GetChild(0));//상속
                carrotShip[7].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)
            {
                this.transform.SetParent(carrotShip[8].transform.GetChild(0));//상속
                carrotShip[8].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
            else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)
            {
                this.transform.SetParent(carrotShip[9].transform.GetChild(0));//상속
                carrotShip[9].transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }

            //x초뒤 페이드아웃
            StartCoroutine("FadeOut");
            //한번만실행
            cameraMoveAgent = false;
        }

        //페이드인아웃상태
        if (fadeOut == true)//페이드아웃
        {
            if (AlphaBlack < 1.0f)
                AlphaBlack += 1.0f * Time.deltaTime;
            else if (FeildChangeScript.fieldIndex + 2 == 7)//게임종료 -> 마지막씬 : 7
            {
                GameManagerInfo.Coin = gamemanagment_script.Coin;
                GameManagerInfo.PlayTime = gamemanagment_script.playTime;
                SceneManager.LoadScene("ResultScene");
            }
            else//페이드아웃 종료
            {

                fadeBlack = true; // -> 화면검정색상태
                AlphaBlack = 1f;
                StartCoroutine("FadeIn");
                fadeOut = false;
            }
        }
        if (fadeIn == true)//페이드인
        {
            if (AlphaBlack > 0f)
                AlphaBlack -= 1.0f * Time.deltaTime;
            else//페이드인종료
            {
                AlphaBlack = 0f;
                fadeIn = false;
                BlackScreen.gameObject.SetActive(false);

                //마지막씬
                if (FeildChangeScript.fieldIndex + 2 == 7)
                {
                    laststageText1.SetActive(true);
                    laststageText2.SetActive(true);
                }
            }
        }
        if (BlackScreen.gameObject.activeSelf == true)
        {
            BlackScreen.color = new Color(0f, 0f, 0f, AlphaBlack);
        }

    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.5f);//페이드아웃시작
        BlackScreen.gameObject.SetActive(true);
        fadeOut = true;

    }
    IEnumerator FadeIn()//필드 변경 직후
    {
        yield return new WaitForSeconds(1.0f);//페이드인시작
        fadeBlack = false;
        fadeIn = true;
        FeildChangeScript.MainScreenButtonOn();//버튼 활성화
        FeildChangeScript.FieldNameUI = true;//필드명 UI 활성화

        yield return new WaitForSeconds(1.0f);
        //필드변경 보너스골드
        gamemanagment_script.Coin += 1000;
    }
}
