using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScript : MonoBehaviour
{
    //게임매니지먼트
    public GameObject gameM;
    gameManagement gamemanagment_script;

    //메인플레이어
    public GameObject mainPlayer;
    player_script playerScript;
    //조이스틱
    public GameObject joysctick;
    cJoyStick cjoystick_script;
    //ButtonScript
    public GameObject ButtonScriptGameObject;
    ButtonScript buttonScript;

    //StoreUi
    public Image StoreUi;
    //public GameObject StoreButtonScroll;
    //public GameObject MyStoreButtonScroll;
    bool enableStore = true;//true상태= StoreUI생성가능상태
    public bool storeUiOn = false;
    public bool storeUiOff = false;

    //AuctionUi
    public Image AuctionUi;
    bool enableAuction = true;//true상태= AuctionUI생성가능상태
    bool AuctionUiOn = false;
    bool AuctionUiOff = false;
    //public GameObject AuctionButtonScroll;

    //InventoryUi
    public GameObject InventoryCanvas;//인벤토리 캔버스

    //MainUi
    public GameObject mainCanvas;//메인화면 캔버스

    //CompassUi
    public GameObject compassCanvas;//나침반 조이스틱 캔버스
    public Image Joy;//조이스틱
    Vector3 JoyPos;//compassJoy시작 위치

    //WeghitUi
    public GameObject weightUi;//가방무게 ui
    //CoinUi
    public GameObject coinUi;//보유코인 ui
    //SlotUi
    public GameObject slotUi;//보유슬롯ui

    //GmaeInfoUI
    public GameObject GameInfoUI;//게임정보ui
    public GameObject GameInfoPanel;

    public GameObject FieldNumUi;//스테이지 번호 ui

    //CoinPlusMinusUi
    int CurrentCoin = 0;
    int BeforeCoin = 0;
    public TextMeshProUGUI coinChange;

    //Option
    public GameObject OptionButton;//게임옵션버튼

    //audio
    new public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = mainPlayer.GetComponent<player_script>();//메인플레이어
        cjoystick_script = joysctick.GetComponent<cJoyStick>();//조이스틱
        JoyPos = Joy.transform.position;//조이스틱초기위치
        buttonScript = ButtonScriptGameObject.GetComponent<ButtonScript>();//버튼스크립트
        gamemanagment_script = gameM.GetComponent<gameManagement>();//게임매니지먼트
        BeforeCoin = gamemanagment_script.Coin;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AuctionUi.GetComponent<RectTransform>().localPosition);
        //상점
        if (playerScript.step == player_script.STEP.SHOPPING && enableStore == true)
        {
            //메인화면UI비활성화
            mainCanvas.SetActive(false);
            //나침반UI비활성화
            compassCanvas.SetActive(false);
            //인벤토리캔버스off
            InventoryCanvas.SetActive(false);
            //게임정보 아이콘 off
            GameInfoUI.SetActive(false);
            GameInfoPanel.SetActive(false);
            //스테이지 번호 아이콘 비활성화
            FieldNumUi.SetActive(false);
            //옵션 아이콘off
            OptionButton.SetActive(false);

            //가방UI활성화
            weightUi.SetActive(true);
            //보유코인ui활성화
            coinUi.SetActive(true);
            //보유슬롯ui활성화
            slotUi.SetActive(true);

            StartCoroutine("CstoreUiOn");

            enableStore = false;//한번만실행
        }
        if (storeUiOn == true)//
        {
            if (StoreUi.transform.localPosition.x > 280f)
                StoreUi.transform.Translate(Vector3.left * 4000.0f * Time.deltaTime);
            else
            {
                // StoreUi.transform.localPosition = new Vector3(110f, 92f, 0f); //위치고정 -> 살짝 끊김있게 보임
                buttonScript.StoreButtonEnableOn();//버튼활성화
                storeUiOn = false;
            }
        }
        if (storeUiOff == true)//
        {
            if (StoreUi.transform.localPosition.x < 2000f)
                StoreUi.transform.Translate(Vector3.left * -2500.0f * Time.deltaTime);
            else
            {
                StoreUi.transform.localPosition = new Vector3(2000f, 221f, 0f); 
                storeUiOff = false;
                //스토어패널off
                StoreUi.gameObject.SetActive(false);
            }
        }
        //쇼핑해제(한번만실행)
        if (playerScript.step == player_script.STEP.SHOPPING && buttonScript.StoreExit == true)
        {
            //메인화면UI활성화
            mainCanvas.SetActive(true);
            //나침반UI활성화
            compassCanvas.SetActive(true);
            //게임정보 아이콘 활성화
            GameInfoUI.SetActive(true);
            GameInfoPanel.SetActive(false);//패널false
            //스테이지 번호 아이콘 활성화
            FieldNumUi.SetActive(true);
            //옵션 아이콘on
            OptionButton.SetActive(true);

            Joy.rectTransform.position = JoyPos;//조이스틱 원위치
            //가방UI활성화
            weightUi.SetActive(false);
            //보유코인ui활성화
            coinUi.SetActive(false);
            //보유슬롯ui비활성화
            slotUi.SetActive(false);

            //캐릭터정지
            cjoystick_script.EndDrag();
            //캐릭터 정상이동 가능상태
            playerScript.Col_Check = false;

            storeUiOff = true;//UI OFF
            enableStore = true;//UI ON가능

            //플래그초기화
            buttonScript.StoreExit = false;
            buttonScript.StoreExit2 = true;
        }




        //Auction
        if (playerScript.step == player_script.STEP.AUCTION && enableAuction == true)
        {
            //메인화면UI비활성화
            mainCanvas.SetActive(false);
            //나침반UI비활성화
            compassCanvas.SetActive(false);
            //인벤토리캔버스off
            InventoryCanvas.SetActive(false);
            //게임정보 아이콘 비활성화
            GameInfoUI.SetActive(false);
            GameInfoPanel.SetActive(false);
            //스테이지 번호 아이콘 비활성화
            FieldNumUi.SetActive(false);
            //옵션 아이콘off
            OptionButton.SetActive(false);

            //가방UI활성화
            weightUi.SetActive(true);
            //보유코인ui활성화
            coinUi.SetActive(true);
            //보유슬롯ui활성화
            slotUi.SetActive(true);

            StartCoroutine("CacutionUiOn");

            enableAuction = false;//한번만실행

        }
        if (AuctionUiOn == true)
        {
            if (AuctionUi.GetComponent<RectTransform>().anchoredPosition.x > -110f)
                AuctionUi.transform.Translate(Vector3.left * 4000.0f * Time.deltaTime);
            else
            {
                // StoreUi.transform.localPosition = new Vector3(110f, 92f, 0f); //위치고정 -> 살짝 끊김있게 보임
                buttonScript.AuctionButtonEnableOn();//버튼활성화
                AuctionUiOn = false;
            }
        }
        if (AuctionUiOff == true)
        {
            if (AuctionUi.transform.localPosition.x < 2000f)
                AuctionUi.transform.Translate(-Vector3.left * 2500.0f * Time.deltaTime);
            else
            {
                AuctionUi.transform.localPosition = new Vector3(2000f, 26.6f, 0f);
                AuctionUiOff = false;

                //옥션패널off
                AuctionUi.gameObject.SetActive(false);
            }
        }

        //옥션해제(한번만실행)
        if (playerScript.step == player_script.STEP.AUCTION && buttonScript.AuctionExit == true)
        {
            //메인화면UI비활성화
            mainCanvas.SetActive(true);
            //나침반UI비활성화
            compassCanvas.SetActive(true);
            //게임정보 아이콘 활성화
            GameInfoUI.SetActive(true);
            GameInfoPanel.SetActive(false);//패널false
            //스테이지 번호 아이콘 활성화
            FieldNumUi.SetActive(true);
            //옵션 아이콘on
            OptionButton.SetActive(true);


            Joy.rectTransform.position = JoyPos;//조이스틱 원위치
            //가방UI활성화
            weightUi.SetActive(false);
            //보유코인ui활성화
            coinUi.SetActive(false);
            //보유슬롯ui비활성화
            slotUi.SetActive(false);

            //캐릭터정지
            cjoystick_script.EndDrag();
            //캐릭터 정상이동 가능상태
            playerScript.Col_Check = false;

            AuctionUiOff = true;//UI OFF
            enableAuction= true;//UI ON가능

            //플래그초기화
            buttonScript.AuctionExit = false;
            buttonScript.AuctionExit2 = true;

        }

        //Coint 증가감소 Ui
        CurrentCoin = gamemanagment_script.Coin;
        if (CurrentCoin > BeforeCoin)//보유코인 변동++
        {
            StopCoroutine("CointPlus");
            StopCoroutine("CointMinus");
            coinChange.gameObject.SetActive(true);
            coinChange.GetComponent<TextMeshProUGUI>().text = "+" + (CurrentCoin - BeforeCoin).ToString();
            coinChange.color = new Color(0, 256, 0 , 1);//Green
            if (playerScript.step == player_script.STEP.NORMAL || playerScript.step == player_script.STEP.MOVE)
                coinChange.GetComponent<RectTransform>().anchoredPosition = new Vector3(-547f, 395f, 0f);
            else
                coinChange.GetComponent<RectTransform>().anchoredPosition = new Vector3(-353f, -458f, 0f);

            StartCoroutine("CoinPlus");
            BeforeCoin = CurrentCoin;

           audio.Play();
        }
       else if (CurrentCoin < BeforeCoin)//보유코인 변동--
        {
            StopCoroutine("CointPlus");
            StopCoroutine("CointMinus");
            coinChange.gameObject.SetActive(true);
            coinChange.GetComponent<TextMeshProUGUI>().text = "-" + (BeforeCoin - CurrentCoin).ToString();
            coinChange.color = new Color(256, 0, 0);//Green

            if (playerScript.step == player_script.STEP.NORMAL || playerScript.step == player_script.STEP.MOVE)
                coinChange.GetComponent<RectTransform>().anchoredPosition = new Vector3(-547f, 395f, 0f);
            else
                coinChange.GetComponent<RectTransform>().anchoredPosition = new Vector3(-353f, -458f, 0f);

            StartCoroutine("CoinMinus");
            BeforeCoin = CurrentCoin;

            audio.Play();
        }
    }

   
    IEnumerator CstoreUiOn()
    {
        yield return new WaitForSeconds(0.5f);
        StoreUi.gameObject.SetActive(true);
        storeUiOn = true;
    }

    IEnumerator CacutionUiOn()
    {
        yield return new WaitForSeconds(0.5f);
        AuctionUi.gameObject.SetActive(true);
        AuctionUiOn = true;
    }

    IEnumerator CoinPlus()
    {
        coinChange.color = new Color(0, 256, 0, 1f);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.9f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.8f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.7f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.6f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.5f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.4f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.3f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(0, 256, 0, 0.2f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.gameObject.SetActive(false);
    }
    IEnumerator CoinMinus()
    {
        coinChange.color = new Color(256, 0, 0, 1f);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.9f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.8f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.7f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.6f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.5f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.4f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.3f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.color = new Color(256, 0, 0, 0.2f);
        coinChange.transform.position = new Vector3(coinChange.transform.position.x, coinChange.transform.position.y + 5.0f, coinChange.transform.position.z);
        yield return new WaitForSeconds(0.05f);
        coinChange.gameObject.SetActive(false);
    }
}
