using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//이벤트시스템
using System.Text.RegularExpressions;//텍스트 문자열에서 숫자만 추출 함수
using TMPro;//텍스트매쉬프로

public class ButtonScript : MonoBehaviour
{
    //게임매니저
    public GameObject gameManager;
    gameManagement gameManagerScript;

    //플레이어
    public GameObject main_player;
    player_script player_script;

    //조이스틱
    public GameObject joysctick;
    cJoyStick cjoystick_script;

    //아이템 데이터 베이스
    public GameObject ItemDB;
    itemDatabase ItemDbScript;

    //스토어스크립트
    public GameObject storeManager;
    storeScript storeScript;

    //인벤토리버튼
    public GameObject InventoryCanvas;

    //상점구매버튼
    public GameObject DealPanel;//구매패널
    GameObject DealPanel_ItemPanel;//클릭한 구매 패널

    //상점판매버튼
    public GameObject SalePanel;//판매패널
    GameObject SalePanel_MyItemPanel;//클릭한 판매패널

    //상점나가기버튼
    public GameObject StoreExitButton;

    //아이템구매버튼
    GameObject PurchaseItemPanel;//구매 클릭한 아이템패널
    GameObject PurchaseItemButton;//구매 클릭한 아이템버튼

    //아이템입찰버튼
    GameObject BiddingItemPanel;//구매 클릭한 아이템패널
    GameObject BiddingItemButton;//구매 클릭한 아이템버튼

    //가방아이템 구매
    public bool bag2_falg = false;
    public bool bag3_falg = false;

    //아이템판매버튼
    GameObject SaleItemPanel;//판매클릭한 아이템패널
    GameObject SaleItemButton;//판매 클릭한 아이템버튼
    bool saleFailDelay = false;//판매 실패 후 딜레이

    //아이템 구매실패 텍스트
    public TextMeshProUGUI CoinLack;
    public TextMeshProUGUI WeightLack;
    public TextMeshProUGUI BagLack;
    public TextMeshProUGUI SlotLack;

    //아이템구매후 인벤토리 이동
    public GameObject InventoryPanel;//아이템 인벤토리
    public GameObject InventoryPanel2;//아이템 인벤토리
    public int InventoryIndex = 0;//인벤토리번호


    //구매패널 아이템 개수 입력패널
    public float mousePosition_y;//마우스커서y
    public Text ItemCountText;//아이템 개수
    public Text ItemCostText;//아이템 가격
    public Text ItemWeightText;//아이템 무게
    public int ItemCount;//아이템 개수
    public int ItemCost;//아이템가격
    public float ItemWeight;//아이템무게
    public Image ItemImage;//아이템이미지
    public Text ItemName;//아이템이름
    public Image ItemFrame;//아이템액자
    public Text ItemQuality;//아이템품질

    //판매패널 판매 개수 입력패널
    public int MyItemCount;//아이템 개수
    public int MyItemCost;//아이템가격
    public Text MyItemCountText;//아이템 개수
    public Text MyItemCostText;//아이템 가격
    public Text MyItemWeightText;//아이템 무게
    public int MyCurrentCompareCost;//최근구매가
    public int MyItemCompareCost_int;//비교가
    public float MyItemWeight;//아이템무게
    public Image MyItemImage;//아이템이미지
    public Text MyItemName;//아이템이름
    public Image MyItemFrame;//아이템액자
    public Text MyItemQuality;//아이템품질
    public Text MyItemCompareCost;//비교가
    public Image MyItemCompareImage;//비교이미지

    //판매실패 텍스트
    public TextMeshProUGUI SaleFail;//판매실패

    //옥션
    public bool Auction_Start = false;
    public GameObject AuctionPanel;
    public Sprite EmptyImage;//빈이미지
    float CostUpPoint = 0.0f;//확률
    public GameObject BiddingWarningPanel;//입찰경고패널
    public GameObject GoButton;//가격증가 버튼
    public GameObject StopButton;//입찰가에 판매 버튼
    public GameObject BiddingButton;//입찰버튼
    public GameObject AuctionExitButton;//경매나가기 버튼
    public GameObject DummyAuctionInventoryPanel;//더미패널->child(0) -> activeSelf = false;
    //bool Enable_Bidding = false;//입찰가능
    public GameObject EmptyButton;//빈버튼
    public GameObject EmptyButton2;//빈버튼
    public TextMeshProUGUI BiddingItem;//입찰아이템 텍스트매쉬
    public Sprite SuccessAuction;
    public Sprite FailAuction;
    public Sprite SaleAuction;
    public GameObject BiddingResultPanel;//옥션 결과 패널
    int beforeBiddingCost = 0;//전입찰가
    int afterBiddingCost = 0;//후입찰가
    public GameObject AuctionHelpPanel;//옥션도움말 패널
    public GameObject AuctionHelpButton;//옥션도움말 버튼
    public GameObject CarrotWarning;//당근 판매불가 경고문

    //MyStorePanel
    public GameObject StorePanel;
    public GameObject StoreButton;
    public GameObject MyStorePanel;

    //Up Down Icon
    public Sprite UpCostImage;
    public Sprite DownCostImage;

    //상점나가기버튼
    public bool StoreExit = false;
    public bool AuctionExit = false;
    public bool StoreExit2 = false;
    public bool AuctionExit2 = false;

    //Gameinfo버튼
    public GameObject GameInfoPanel;

    //audio
    new private AudioSource audio;
    public AudioClip ButtonSound;
    public AudioClip RackSound;

    //게임옵션패널
    public GameObject OptionPanel;
    public GameObject ExitGameWarning;//나가기 재확인버튼

    //체크박스
    public Toggle SoundEffectToggle;
    public Toggle BackGroundSoundToggle;

    public GameObject mainCamera;//메인카메라(오디오추출)
    public GameObject UI;//UI(오디오추출)

    //게임데이터 joson저장
    GameObject GameData;
    GameData GameDataScript;

    // Start is called before the first frame update

    void Start()
    {
        ItemDbScript = ItemDB.GetComponent<itemDatabase>();//아이템 데이터 베이스
        storeScript = storeManager.GetComponent<storeScript>();//스토어 스크립트
        gameManagerScript = gameManager.GetComponent<gameManagement>();//게임매니저 스크립트
        player_script = main_player.GetComponent<player_script>();//플레이어 스크립트
        cjoystick_script = joysctick.GetComponent<cJoyStick>();//조이스틱
        GameData = GameObject.FindGameObjectWithTag("GameData");
        GameDataScript = GameData.GetComponent<GameData>();

        //오디오
        audio = GetComponent<AudioSource>();

        //오디오 정보
        if (StartSceneInfo.backGroundSoundEnable == true)
            BackGroundSoundToggle.isOn = true;
        else
            BackGroundSoundToggle.isOn = false;

        if (StartSceneInfo.SoundEffectEnable == true)
            SoundEffectToggle.isOn = true;
        else
            SoundEffectToggle.isOn = false;
    }

    public void OnClickInventory()//아이템창 ON
    {
        InventoryPanel.transform.localPosition = new Vector3(65.9f,-600f,0f);
        InventoryCanvas.SetActive(true);

        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();
    }
    public void OnClickInventoryExitButton()//아이템창 OFF
    {
        InventoryCanvas.SetActive(false);

        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();
    }

    public void OnClickItemPanelButton()//상점 아이템클릭
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        //개수 소진시 구매X
        if (EventSystem.current.currentSelectedGameObject.transform.GetChild(3).GetComponent<Text>().text == "X0")
        {
            return;
        }

        DealPanel_ItemPanel = EventSystem.current.currentSelectedGameObject;

        Debug.Log(DealPanel_ItemPanel);

        if (DealPanel_ItemPanel == null)//터치오류
        {
            return;
        }

        DealPanel.SetActive(true);
        SalePanel.SetActive(false);

        //UI데이터 초기화
        ItemCount = 1;//개수초기화
        ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
        ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

        //UI갱신
        ItemCountText.text = "X" + ItemCount.ToString();//아이템개수
        ItemWeightText.text = "무게:" + (ItemWeight * ItemCount).ToString();//무게
        ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
        ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
        ItemImage.sprite = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].itemImage;//아이템이미지
        ItemName.text = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].itemName;//아이템이름
        ItemQuality.text = DealPanel_ItemPanel.transform.GetChild(6).GetComponent<Text>().text;//아이템 품질
        //아이템액자
        switch (ItemQuality.GetComponent<Text>().text.ToString())
        {
            case "D":
                ItemFrame.sprite = storeScript.itemFrame[0];
                break;
            case "C":
                ItemFrame.sprite = storeScript.itemFrame[1];
                break;
            case "B":
                ItemFrame.sprite = storeScript.itemFrame[2];
                break;
            case "A":
                ItemFrame.sprite = storeScript.itemFrame[3];
                break;
            case "S":
                ItemFrame.sprite = storeScript.itemFrame[4];
                break;
        }

    }

    public void OnClickCancelButton()//상점 아이템클릭후 취소
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        DealPanel.SetActive(false);
    }

    public void OnClickMyCancelButton()//My상점 아이템클릭후 취소
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        SalePanel.SetActive(false);
    }

    public void OnClickMyItemPanelButton()//My상점 판매할 아이템클릭
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        SalePanel_MyItemPanel = EventSystem.current.currentSelectedGameObject;
        if (SalePanel_MyItemPanel == null)//터치오류
        {
            return;
        }

        if (Equals(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text, "X"))//판매불가아이템
        {
            if (saleFailDelay == false)
            {
                saleFailDelay = true;
                StartCoroutine("Sale_Fail");
            }
            return;
        }

        SalePanel.SetActive(true);
        DealPanel.SetActive(false);

        //UI데이터 초기화
        MyItemCount = 1;//개수초기화
        MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
        MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
            / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
        MyCurrentCompareCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(12).GetComponent<Text>().text);//최근구매가

        //UI갱신
        MyItemCountText.text = "X" + MyItemCount.ToString();//아이템개수
        MyItemWeightText.text = "무게:" + (MyItemWeight * MyItemCount).ToString();//무게
        MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
        MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
        MyItemImage.sprite = SalePanel_MyItemPanel.transform.GetChild(1).GetComponent<Image>().sprite;//아이템이미지
        MyItemName.text = SalePanel_MyItemPanel.transform.GetChild(7).GetComponent<Text>().text;//아이템이름
        MyItemQuality.text = SalePanel_MyItemPanel.transform.GetChild(6).GetComponent<Text>().text;//아이템 품질
        //아이템액자
        switch (MyItemQuality.GetComponent<Text>().text.ToString())
        {
            case "D":
                MyItemFrame.sprite = storeScript.itemFrame[0];
                break;
            case "C":
                MyItemFrame.sprite = storeScript.itemFrame[1];
                break;
            case "B":
                MyItemFrame.sprite = storeScript.itemFrame[2];
                break;
            case "A":
                MyItemFrame.sprite = storeScript.itemFrame[3];
                break;
            case "S":
                MyItemFrame.sprite = storeScript.itemFrame[4];
                break;
            case "Special":
                MyItemFrame.sprite = storeScript.itemFrame[5];
                break;
        }
        MyItemCompareCost.text = SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text;//비교가
        MyItemCompareImage.sprite = SalePanel_MyItemPanel.transform.GetChild(14).GetComponent<Image>().sprite;//비교이미지
    }

    public void OnDragItemCountPanel()//상점 아이템데이터
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (Input.mousePosition.y > mousePosition_y)
        {
            if (ItemCount < int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
            {
                ItemCount++;//개수 증가
            }
        }
        else if (ItemCount > 1)
        {
            ItemCount--;//개수 감소
        }

        //데이터 갱신
        ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
        ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

        //UI 갱신
        ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
        ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
        ItemCountText.text = "X" + ItemCount.ToString();//개수

        //마우스커서y
        mousePosition_y = Input.mousePosition.y;
    }

    public void OnDragMyItemCountPanel()//My상점 아이템데이터
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (Input.mousePosition.y > mousePosition_y)
        {
            if (MyItemCount < int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
            {
                MyItemCount++;//개수 증가
            }
        }
        else if (MyItemCount > 1)
        {
            MyItemCount--;//개수 감소
        }

        //데이터 갱신
        MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
        MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
            / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
        MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

        //UI 갱신
        MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
        MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
        MyItemCountText.text = "X" + MyItemCount.ToString();//개수
        MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가

        //마우스커서y
        mousePosition_y = Input.mousePosition.y;
    }

    public void OnClickStoreExitButton()//상점 나가기 버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        StoreExit = true;
        StoreButtonEnableOff();//버튼비활성화
        //구매취소패널 Exit
        OnClickCancelButton();
        OnClickMyCancelButton();
    }

    public void OnClickAuctionExitButton()//옥션 나가기 버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        AuctionExit = true;
        AuctionButtonEnableOff();//버튼비활성화
        //클릭아이템 초기화
        BiddingItemPanel = DummyAuctionInventoryPanel.transform.parent.gameObject;//선택한 아이템정보
        BiddingItemButton = DummyAuctionInventoryPanel;//선택한 아이템버튼
    }

    public void OnClickPurchaseButton()//구매버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        PurchaseItemPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;//구매한 아이템정보
        PurchaseItemButton = EventSystem.current.currentSelectedGameObject;//클릭한 구입 버튼

        bool HaveItem = false;
        int overlapIndex = 0;//중복인덱스
         //인벤토리 탐색 - 중복아이템검사:아이템이름,등급이 같을경우 중복아이템
        for (int i = 0; i <= InventoryIndex - 1; i++)
        {
            if (Equals(PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text)
                && Equals(PurchaseItemPanel.transform.GetChild(10).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text))
            {
                HaveItem = true;
                overlapIndex = i;
            }
            Debug.Log(PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text + "///" + InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text);
        }

        //구매가능여부판단
        if (HaveItem == false && InventoryIndex >= 12)//구매불가능
        {
            PurchaseItemButton.GetComponent<Button>().enabled = false;
            StartCoroutine("PurchaseX_4");
            return;
        }//가방슬롯부족if
        if ((int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))) > gameManagerScript.Coin)//구매불가능
        {
            PurchaseItemButton.GetComponent<Button>().enabled = false;
            StartCoroutine("PurchaseX");
            return;
        }//코인부족 if
        if ((int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text, @"\D", ""))) / 10.0f > (gameManagerScript.fullWeight - gameManagerScript.weight)+0.01f)//구매불가능  +0.01->계산오류 오차예외처리
        {
           // Debug.Log((int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text, @"\D", ""))) / 10.0f);
           // Debug.Log(gameManagerScript.fullWeight + " - " + gameManagerScript.weight + " = " + (gameManagerScript.fullWeight - gameManagerScript.weight));
           // Debug.Log(Mathf.Round((gameManagerScript.fullWeight - gameManagerScript.weight) * 0.1f) * 10f);
            PurchaseItemButton.GetComponent<Button>().enabled = false;
            StartCoroutine("PurchaseX_2");
            return;
        }//가방무게부족 if
        if (player_script.currentBagNum == 3 && (PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text == "행상인의가방" ||
            PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text == "거상의가방"))//구매불가능
        {
            PurchaseItemButton.GetComponent<Button>().enabled = false;
            StartCoroutine("PurchaseX_3");
            return;
        }//구매불가능 if

        if (PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text == "행상인의가방")//가방구매
        {
            bag2_falg = true;//가방구매 플래그
            player_script.currentBagNum = 2;//현재가방 번호

            //스테이터스 변경
            gameManagerScript.fullWeight = 50f;//무게총량변경
            cjoystick_script.speed = 0.22f;//속도증가

            //가방 장착
            player_script.bagLv1.SetActive(false);
            player_script.bagLv3.SetActive(false);
            player_script.bagLv2.SetActive(true);
        }
        if (PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text == "거상의가방")//가방구매
        {
            bag3_falg = true;//가방구매 플래그
            player_script.currentBagNum = 3;//현재가방 번호

            //스테이터스 변경
            gameManagerScript.fullWeight = 200f;//무게총량변경
            cjoystick_script.speed = 0.28f;//속도증가

            //가방 장착
            player_script.bagLv1.SetActive(false);
            player_script.bagLv2.SetActive(false);
            player_script.bagLv3.SetActive(true);
        }


        Debug.Log(HaveItem);
        //중복아이템아닌경우
        if (HaveItem == false && bag2_falg == false && bag3_falg == false)
        {
            //인벤토리 갱신
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(0).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(4).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(5).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(1).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(1).GetComponent<Image>().sprite = PurchaseItemPanel.transform.GetChild(5).GetComponent<Image>().sprite;//아이템이미지
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(2).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(2).GetComponent<Image>().sprite = PurchaseItemPanel.transform.GetChild(6).GetComponent<Image>().sprite;//아이템프레임
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(3).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(3).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text;//아이템개수
            int currentCost = (int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))) / int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(6).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(6).GetComponent<Text>().text = currentCost.ToString();//최근구매가
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(7).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(7).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(10).GetComponent<Text>().text;//아이템등급
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(8).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(8).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text;//아이템이름
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(9).gameObject.SetActive(true);
            InventoryPanel.transform.GetChild(InventoryIndex).transform.GetChild(9).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text;//아이템무게

            //인벤토리 갱신2
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(0).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(4).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(5).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(1).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(1).GetComponent<Image>().sprite = PurchaseItemPanel.transform.GetChild(5).GetComponent<Image>().sprite;//아이템이미지
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(2).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(2).GetComponent<Image>().sprite = PurchaseItemPanel.transform.GetChild(6).GetComponent<Image>().sprite;//아이템프레임
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(3).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(3).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text;//아이템개수
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(6).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(6).GetComponent<Text>().text = currentCost.ToString();//최근구매가
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(7).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(7).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(10).GetComponent<Text>().text;//아이템등급
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(8).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(8).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(11).GetComponent<Text>().text;//아이템이름
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(9).gameObject.SetActive(true);
            InventoryPanel2.transform.GetChild(InventoryIndex).transform.GetChild(9).GetComponent<Text>().text = PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text;//아이템무게

            //인덱스 증가
            InventoryIndex++;
        }
        else if (bag2_falg == false && bag3_falg == false)//중복아이템인경우
        {
            //인벤토리갱신1
            Debug.Log((int.Parse(Regex.Replace(InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(3).GetComponent<Text>().text, @"\D", ""))));
            int ItemCount = (int.Parse(Regex.Replace(InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(3).GetComponent<Text>().text, @"\D", ""))) + int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));
            InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(3).GetComponent<Text>().text = "X" + ItemCount.ToString();//아이템개수

            int currentCost = (int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))) / int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));
            InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(6).GetComponent<Text>().text = currentCost.ToString();//최근구매가

            float weight = (float.Parse(Regex.Replace(InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))) / 10f + float.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text, @"\D", "")) / 10f;
            InventoryPanel.transform.GetChild(overlapIndex).transform.GetChild(9).GetComponent<Text>().text = "무게:" + weight.ToString("F1");//무게

            //인벤토리갱신2
            InventoryPanel2.transform.GetChild(overlapIndex).transform.GetChild(3).GetComponent<Text>().text = "X" + ItemCount.ToString();//아이템개수
            InventoryPanel2.transform.GetChild(overlapIndex).transform.GetChild(6).GetComponent<Text>().text = currentCost.ToString();//최근구매가
            InventoryPanel2.transform.GetChild(overlapIndex).transform.GetChild(9).GetComponent<Text>().text = "무게:" + weight.ToString("F1");//무게
        }

        //당근 가격상승
        ItemDbScript.carrotPoint += ItemCount;
        ItemDbScript.carrotPoint += (int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))) / 100;

        //패널 다시 비활성화
        PurchaseItemPanel.SetActive(false);

        if (bag2_falg == false && bag3_falg == false)//가방이 아닐경우
        {
            //구매후 MyPanel갱신
            storeScript.MyStoreListLoading();
            //구매후 무게 적립
            gameManagerScript.weight += float.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(12).GetComponent<Text>().text, @"\D", "")) / 10.0f;
        }

        //구매후 재고처리
        storeScript.arrstoreData2[storeScript.storePointerIndex].itemCount[DealPanel_ItemPanel.transform.GetSiblingIndex()] -= int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));
        int itemCount = (int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""))) - int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));
        DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text = "X" + itemCount.ToString();

        //구매후 플레이어 금화 계산
        gameManagerScript.Coin -= (int.Parse(Regex.Replace(PurchaseItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", "")));

        //플래그 초기화
        bag2_falg = false;
        bag3_falg = false;
    }

    public void OnClickSaleButton()//판매버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        SaleItemPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;//판매한 아이템정보
        int ItemCount = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));//현재수량
        int SaleItemCount = int.Parse(Regex.Replace(SaleItemPanel.transform.GetChild(7).GetComponent<Text>().text, @"\D", ""));//판매수량
        int ResultItemCount = 0;//남은수량
        float PerOneWeight;

        PerOneWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"[^-?\d+\.]", "")) /
        int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));//한개당 무게

        int Index = 0;

        if (SaleItemPanel.transform.GetChild(11).GetComponent<Text>().text == "당근")
        {
            PerOneWeight = 0.0f;
        }

        //인벤토리 탐색
        for (int i = 0; i <= InventoryIndex - 1; i++)
        {
            if (Equals(SaleItemPanel.transform.GetChild(11).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text)//이름
                && Equals(SaleItemPanel.transform.GetChild(10).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text))//등급
            {
                Index = i;
            }
        } // -> 인벤토리 일치하는 아이템 탐색


        if (ItemCount == SaleItemCount)//전수량 판매
        {
            SalePanel_MyItemPanel.transform.SetAsLastSibling();
            SalePanel_MyItemPanel.SetActive(false);

            //인벤토리에서 삭제
            for (int i = 0; i <= 9; i++)
            {
                InventoryPanel.transform.GetChild(Index).transform.GetChild(i).gameObject.SetActive(false);
            }

            InventoryPanel.transform.GetChild(Index).transform.SetAsLastSibling();


            // 인벤토리에서 삭제2
            for (int i = 0; i <= 9; i++)
            {
                InventoryPanel2.transform.GetChild(Index).transform.GetChild(i).gameObject.SetActive(false);
            }

            InventoryPanel2.transform.GetChild(Index).transform.SetAsLastSibling();

            //인덱스
            InventoryIndex--;
        } else
        {
            ResultItemCount = ItemCount - SaleItemCount;//남은수량
            //무게

            //패널갱신
            SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text = "X" + ResultItemCount;//남은수량
            SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text = "무게:" + PerOneWeight * ResultItemCount;//무게

            //인벤토리 갱신
            InventoryPanel.transform.GetChild(Index).transform.GetChild(9).GetComponent<Text>().text = "무게:" + PerOneWeight * ResultItemCount;//아이템무게
            InventoryPanel.transform.GetChild(Index).transform.GetChild(3).GetComponent<Text>().text = "X" + ResultItemCount;//아이템개수

            //인벤토리 갱신2
            InventoryPanel2.transform.GetChild(Index).transform.GetChild(9).GetComponent<Text>().text = "무게:" + PerOneWeight * ResultItemCount;//아이템무게
            InventoryPanel2.transform.GetChild(Index).transform.GetChild(3).GetComponent<Text>().text = "X" + ResultItemCount;//아이템개수
        }

        //플레이어 정보 갱신
        gameManagerScript.Coin += int.Parse(SalePanel.transform.GetChild(9).GetComponent<Text>().text);//골드
        gameManagerScript.weight -= PerOneWeight * SaleItemCount;//무게

        //패널 비활성화
        SaleItemPanel.SetActive(false);
    }

    //Auction
    public void OnSelectBiddingButton()//입찰 아이템 선택 버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
            return ;

        BiddingItemPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;//선택한 아이템정보
        BiddingItemButton = EventSystem.current.currentSelectedGameObject;//선택한 아이템버튼
        BiddingItem.text = "아이템:" + BiddingItemButton.transform.GetChild(8).GetComponent<Text>().text + "/" + BiddingItemButton.transform.GetChild(7).GetComponent<Text>().text + "등급"
            + "/총구매가:" + int.Parse(Regex.Replace(BiddingItemButton.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * int.Parse(BiddingItemButton.transform.GetChild(6).GetComponent<Text>().text);
    }

    public void OnClickBiddingWarningButton()//옥션판매
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null || BiddingItemButton == null)
            return;

        EmptyButton.SetActive(false);//빈버튼 작동X
        EmptyButton2.SetActive(false);//빈버튼 작동X
        if (BiddingItemButton.transform.GetChild(0).gameObject.activeSelf == false)//빈 패널 선택시 동작X
            return;

        //당근일경우 경고
        if (BiddingItemButton.transform.GetChild(8).GetComponent<Text>().text == "당근")
        {
            BiddingItemButton = null;
            StartCoroutine("PurchaseX_5");
            return;
        }

        AuctionButtonEnableOff();//버튼비활성화
        BiddingWarningPanel.SetActive(true);

    }

    public void OnClickBiddingCancelButton()//옥션판매취소
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        EmptyButton.SetActive(true);//빈버튼 작동
        EmptyButton2.SetActive(true);//빈버튼 작동X
        AuctionButtonEnableOn();//활성화
        BiddingWarningPanel.SetActive(false);
        //클릭아이템 초기화
        BiddingItemPanel = DummyAuctionInventoryPanel.transform.parent.gameObject;//선택한 아이템정보
        BiddingItemButton = DummyAuctionInventoryPanel;//선택한 아이템버튼
    }

    public void OnClickBiddingButton()//입찰버튼
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        Auction_Start = true;//옥션시작
        AuctionButtonEnableOn();//활성화
        //판매관련 버튼 생성
        GoButton.SetActive(true);//가격증가 버튼
        StopButton.SetActive(true);//입찰가에 판매 버튼
        //입찰,나가기 버튼 Hide
        BiddingButton.SetActive(false);//입찰 버튼
        AuctionExitButton.SetActive(false);//옥션 나가기 버튼

        for (int i = 0; i < 11; i++)//입찰할 아이템 선택버튼 비활성화
        {
            InventoryPanel2.transform.GetChild(i).GetComponent<Button>().enabled = false;
        }

        //옥션패널 갱신
        int totalCurrentCost;
        AuctionPanel.transform.GetChild(1).GetComponent<Image>().sprite = BiddingItemButton.transform.GetChild(1).GetComponent<Image>().sprite;//아이템이미지
        AuctionPanel.transform.GetChild(7).GetComponent<Text>().text = BiddingItemButton.transform.GetChild(8).GetComponent<Text>().text;//아이템이름
        AuctionPanel.transform.GetChild(6).GetComponent<Text>().text = BiddingItemButton.transform.GetChild(7).GetComponent<Text>().text;//아이템등급
        AuctionPanel.transform.GetChild(2).GetComponent<Image>().sprite = BiddingItemButton.transform.GetChild(2).GetComponent<Image>().sprite;//아이템액자
        AuctionPanel.transform.GetChild(3).GetComponent<Text>().text = BiddingItemButton.transform.GetChild(3).GetComponent<Text>().text;//아이템개수
        AuctionPanel.transform.GetChild(5).GetComponent<Text>().text = BiddingItemButton.transform.GetChild(6).GetComponent<Text>().text;//최근구매가

        totalCurrentCost = int.Parse(BiddingItemButton.transform.GetChild(6).GetComponent<Text>().text) *
            int.Parse((Regex.Replace(BiddingItemButton.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")));
        AuctionPanel.transform.GetChild(15).GetComponent<Text>().text = totalCurrentCost.ToString(); //최근총구매가

        //등급에따라 입찰가변경
        int AuctionCost = 0;
        if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "D")
        {
            AuctionCost = (int)(int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text) / Random.Range(2.0f, 4.0f));
            CostUpPoint = 6.0f;
        }
        else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "C")
        {
            AuctionCost = (int)(int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text) / Random.Range(2.0f, 3.5f));
            CostUpPoint = 7.0f;
        }
        else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "B")
        {
            AuctionCost = (int)(int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text) / Random.Range(2.0f, 3.3f));
            CostUpPoint = 7.0f;
        }
        else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "A")
        {
            AuctionCost = (int)(int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text) / Random.Range(1.5f, 3.3f));
            CostUpPoint = 7.5f;
        }
        else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "S")
        {
            AuctionCost = (int)(int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text) / Random.Range(1.3f, 3.0f));
            CostUpPoint = 8.0f;
        }

        if (AuctionCost == 0)
            AuctionCost = 1;

        AuctionPanel.transform.GetChild(17).GetComponent<Text>().text = AuctionCost.ToString();//입찰가갱신

        //가격비교
        if (totalCurrentCost <= AuctionCost)//비교대비 상승
        {
            AuctionPanel.transform.GetChild(12).GetComponent<Image>().sprite = UpCostImage;//비교이미지
            AuctionPanel.transform.GetChild(11).GetComponent<Text>().text = "비교:       " + (AuctionCost - totalCurrentCost).ToString();//비교
        }
        else//비교대비 하락
        {
            AuctionPanel.transform.GetChild(12).GetComponent<Image>().sprite = DownCostImage;//비교이미지
            AuctionPanel.transform.GetChild(11).GetComponent<Text>().text = "비교:       " + (totalCurrentCost - AuctionCost).ToString();//비교
        }

        BiddingWarningPanel.SetActive(false);
    }

    public void OnClickCostUpButton()//가격올리기
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        int CurrentCost = (int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text));
        int AuctionCost = (int.Parse(AuctionPanel.transform.GetChild(17).GetComponent<Text>().text));
        beforeBiddingCost = AuctionCost;
        if (Random.Range(0.0f, 10.0f) <= CostUpPoint) //CostUpPoint * 10 만큼의 확률%
        {
            if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "D")
            {
                if (CurrentCost * 1.8f < AuctionCost)//원가의 두배를 넘어선경우
                    AuctionCost = (int)((AuctionCost * Random.Range(1.1f, 1.5f)));//가격상승
                else
                    AuctionCost = (int)((AuctionCost * Random.Range(1.5f, 2.0f)));//가격상승

                CostUpPoint -= 6;//확률감소
                if (CostUpPoint <= 10)
                    CostUpPoint = 5;
            }
            else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "C")
            {
                if (CurrentCost * 1.8f < AuctionCost)//원가의 두배를 넘어선경우
                    AuctionCost = (int)((AuctionCost * Random.Range(1.2f, 1.6f)));//가격상승
                else
                    AuctionCost = (int)((AuctionCost * Random.Range(1.5f, 2.0f)));//가격상승

                CostUpPoint -= 6;//확률감소
                if (CostUpPoint <= 10)
                    CostUpPoint = 5;
            }
            else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "B")
            {
                if (CurrentCost * 1.8f < AuctionCost)//원가의 두배를 넘어선경우
                    AuctionCost = (int)((AuctionCost * Random.Range(1.3f, 1.6f)));//가격상승
                else
                    AuctionCost = (int)((AuctionCost * Random.Range(1.5f, 2.5f)));//가격상승

                CostUpPoint -= 8;//확률감소
                if (CostUpPoint <= 10)
                    CostUpPoint = 5;
            }
            else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "A")
            {
                if (CurrentCost * 1.8f < AuctionCost)//원가의 두배를 넘어선경우
                    AuctionCost = (int)((AuctionCost * Random.Range(1.3f, 1.8f)));//가격상승
                else
                    AuctionCost = (int)((AuctionCost * Random.Range(1.6f, 2.5f)));//가격상승

                CostUpPoint -= 7;//확률감소
                if (CostUpPoint <= 10)
                    CostUpPoint = 5;
            }
            else if (AuctionPanel.transform.GetChild(6).GetComponent<Text>().text == "S")
            {
                if (CurrentCost * 1.8f < AuctionCost)//원가의 두배를 넘어선경우
                    AuctionCost = (int)((AuctionCost * Random.Range(1.3f, 2.0f)));//가격상승
                else
                    AuctionCost = (int)((AuctionCost * Random.Range(1.5f, 2.5f)));//가격상승

                CostUpPoint -= 7;//확률감소
                if (CostUpPoint <= 10)
                    CostUpPoint = 5;
            }
            AuctionPanel.transform.GetChild(17).GetComponent<Text>().text = AuctionCost.ToString();//입찰가 갱신
            afterBiddingCost = AuctionCost;

            int totalCurrentCost = int.Parse(AuctionPanel.transform.GetChild(15).GetComponent<Text>().text);
            //가격비교
            if (totalCurrentCost <= AuctionCost)//비교대비 상승
            {
                AuctionPanel.transform.GetChild(12).GetComponent<Image>().sprite = UpCostImage;//비교이미지
                AuctionPanel.transform.GetChild(11).GetComponent<Text>().text = "비교:       " + (AuctionCost - totalCurrentCost).ToString();//비교
            }
            else//비교대비 하락
            {
                AuctionPanel.transform.GetChild(12).GetComponent<Image>().sprite = DownCostImage;//비교이미지
                AuctionPanel.transform.GetChild(11).GetComponent<Text>().text = "비교:       " + (totalCurrentCost - AuctionCost).ToString();//비교
            }
            //성공패널
            AuctionSuccess();
        }
        else//가격상승실패
        {
            afterBiddingCost = (int)(AuctionCost * 0.3f);
            AuctionPanel.transform.GetChild(17).GetComponent<Text>().text = afterBiddingCost.ToString();//입찰가 갱신
            AuctionFail();
        }
    }
    public void OnClickAuctionPurchase()//옥션판매
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        if (BiddingResultPanel.GetComponent<Image>().sprite == SuccessAuction)//성공
        {
            AuctionButtonEnableOn();//버튼활성화
            BiddingResultPanel.SetActive(false);
            return;
        }

        Auction_Start = false;//옥션종료
        beforeBiddingCost = (int.Parse(AuctionPanel.transform.GetChild(17).GetComponent<Text>().text));

        //판매관련 버튼 삭제
        GoButton.SetActive(false);//가격증가 버튼
        StopButton.SetActive(false);//입찰가에 판매 버튼
        //입찰,나가기 버튼 생성
        BiddingButton.SetActive(true);//입찰 버튼
        AuctionExitButton.SetActive(true);//옥션 나가기 버튼
        //입찰,나가기 버튼 생성
        BiddingButton.SetActive(true);//입찰 버튼
        AuctionExitButton.SetActive(true);//옥션 나가기 버튼
        for (int i = 0; i < 11; i++)//입찰할 아이템 선택버튼 활성화
        {
            InventoryPanel2.transform.GetChild(i).GetComponent<Button>().enabled = true;
        }

        int Index = 0;
        //인벤토리 탐색
        for (int i = 0; i <= InventoryIndex - 1; i++)
        {
            if (Equals(AuctionPanel.transform.GetChild(7).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text)//이름
                && Equals(AuctionPanel.transform.GetChild(6).GetComponent<Text>().text, InventoryPanel.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text))//등급
            {
                Index = i;
            }
        } // -> 인벤토리 일치하는 아이템 탐색

        //인벤토리에서 삭제
        for (int i = 0; i <= 9; i++)
        {
            InventoryPanel.transform.GetChild(Index).transform.GetChild(i).gameObject.SetActive(false);
        }
        InventoryPanel.transform.GetChild(Index).transform.SetAsLastSibling();

        //인벤토리 삭제2
        for (int i = 0; i <= 9; i++)
        {
            InventoryPanel2.transform.GetChild(Index).transform.GetChild(i).gameObject.SetActive(false);
        }
        InventoryPanel2.transform.GetChild(Index).transform.SetAsLastSibling();

        //MyStorePanel삭제
        StorePanel.SetActive(true);
        MyStorePanel.transform.GetChild(Index).transform.gameObject.SetActive(false);
        MyStorePanel.transform.GetChild(Index).transform.SetAsLastSibling();
        StorePanel.SetActive(false);

        //정보갱신
        float Weight = int.Parse(Regex.Replace(BiddingItemButton.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))/10.0f;//무게
                                                                                                                               
        //플레이어 정보 갱신
        gameManagerScript.Coin += int.Parse(AuctionPanel.transform.GetChild(17).GetComponent<Text>().text);//골드
        gameManagerScript.weight -= Weight;//무게

        //인덱스
        InventoryIndex--;
        //옥션패널 기본값으로 초기화
        AuctionPanel.transform.GetChild(1).GetComponent<Image>().sprite = EmptyImage;//아이템이미지
        AuctionPanel.transform.GetChild(7).GetComponent<Text>().text = " ";//아이템이름
        AuctionPanel.transform.GetChild(6).GetComponent<Text>().text = " ";//아이템등급
        AuctionPanel.transform.GetChild(2).GetComponent<Image>().sprite = EmptyImage;//아이템액자
        AuctionPanel.transform.GetChild(3).GetComponent<Text>().text = " ";//아이템개수
        AuctionPanel.transform.GetChild(5).GetComponent<Text>().text = " ";//최근구매가
        AuctionPanel.transform.GetChild(15).GetComponent<Text>().text = " "; //최근총구매가
        AuctionPanel.transform.GetChild(17).GetComponent<Text>().text = " "; //입찰가
        AuctionPanel.transform.GetChild(11).GetComponent<Text>().text = "비교:"; //비교가
        AuctionPanel.transform.GetChild(12).GetComponent<Image>().sprite = EmptyImage;//비교이미지

        //클릭아이템 초기화
        BiddingItemPanel = DummyAuctionInventoryPanel.transform.parent.gameObject;//선택한 아이템정보
        BiddingItemButton = DummyAuctionInventoryPanel;//선택한 아이템버튼

        EmptyButton.SetActive(true);//빈버튼 작동
        EmptyButton2.SetActive(true);//빈버튼 작동X
        BiddingResultPanel.SetActive(false);//패널off
        AuctionButtonEnableOn();//버튼활성화
    }

    public void StoreButtonEnableOff()//상점 관련 버튼기능 비활성화
    {
        for (int i = 0; i <= 11; i++)//입찰할 아이템 선택버튼 활성화
        {
           // Debug.Log(i);
            if(StoreButton.transform.GetChild(i).gameObject.activeSelf == true)
                StoreButton.transform.GetChild(i).GetComponent<Button>().interactable = false;

            if (MyStorePanel.transform.GetChild(i).gameObject.activeSelf == true)
                MyStorePanel.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        StoreExitButton.GetComponent<Button>().interactable = false;
    }

    public void StoreButtonEnableOn()//상점 관련 버튼기능 활성화
    {
        for (int i = 0; i <= 11; i++)//입찰할 아이템 선택버튼 성화
        {
            if (StoreButton.transform.GetChild(i).gameObject.activeSelf == true)
                StoreButton.transform.GetChild(i).GetComponent<Button>().interactable = true;

            if (MyStorePanel.transform.GetChild(i).gameObject.activeSelf == true)
                MyStorePanel.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        StoreExitButton.GetComponent<Button>().interactable = true;
    }


    public void AuctionButtonEnableOff()//옥션 관련 버튼기능 비활성화
    {
        GoButton.GetComponent<Button>().interactable = false;
        StopButton.GetComponent<Button>().interactable = false;
        BiddingButton.GetComponent<Button>().interactable = false; 
        AuctionExitButton.GetComponent<Button>().interactable = false;
        for (int i = 0; i <= 11; i++)//입찰할 아이템 선택버튼 활성화
        {
            InventoryPanel2.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
    public void AuctionButtonEnableOn()//옥션 관련 버튼기능 활성화
    {
        GoButton.GetComponent<Button>().interactable= true;
        StopButton.GetComponent<Button>().interactable = true;
        BiddingButton.GetComponent<Button>().interactable = true;
        AuctionExitButton.GetComponent<Button>().interactable = true;
        for (int i = 0; i <= 11; i++)//입찰할 아이템 선택버튼 활성화
        {
            InventoryPanel2.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void OnClickEmptyButton()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        Debug.Log("빈버튼클릭");
        BiddingItemPanel = DummyAuctionInventoryPanel.transform.parent.gameObject;//선택한 아이템정보
        BiddingItemButton = DummyAuctionInventoryPanel;//선택한 아이템버튼
    }


    //옥션 성공,실패,판매 메서드
    public void AuctionSuccess()//옥션 - 성공
    {
       AuctionButtonEnableOff();

        BiddingResultPanel.SetActive(true);//패널불러오기

        BiddingResultPanel.GetComponent<Image>().sprite = SuccessAuction;

        BiddingResultPanel.transform.GetChild(0).GetComponent<Text>().text = "낙찰가:" + beforeBiddingCost + " -> " + afterBiddingCost;//입찰가비교
        BiddingResultPanel.transform.GetChild(1).GetComponent<Image>().sprite = UpCostImage;//비교이미지
        BiddingResultPanel.transform.GetChild(2).GetComponent<Text>().text = (afterBiddingCost - beforeBiddingCost).ToString();//비교가
        BiddingResultPanel.transform.GetChild(4).GetComponent<Text>().text = afterBiddingCost.ToString();//최종판매가격
    }
    public void AuctionFail()//옥션 - 실패
    {
        AuctionButtonEnableOff();

        BiddingResultPanel.SetActive(true);//패널불러오기

        BiddingResultPanel.GetComponent<Image>().sprite = FailAuction;

        BiddingResultPanel.transform.GetChild(0).GetComponent<Text>().text = "낙찰가:" + beforeBiddingCost + " -> " + afterBiddingCost;//입찰가비교
        BiddingResultPanel.transform.GetChild(1).GetComponent<Image>().sprite = DownCostImage;//비교이미지
        BiddingResultPanel.transform.GetChild(2).GetComponent<Text>().text = (beforeBiddingCost -  afterBiddingCost).ToString();//비교가
        BiddingResultPanel.transform.GetChild(4).GetComponent<Text>().text = afterBiddingCost.ToString();//최종판매가격
    }
    public void AuctionSale()//옥션 - 판매
    {
        AuctionButtonEnableOff();

        BiddingResultPanel.SetActive(true);//패널불러오기

        BiddingResultPanel.GetComponent<Image>().sprite = SaleAuction;

        BiddingResultPanel.transform.GetChild(0).GetComponent<Text>().text = " ";//입찰가비교
        BiddingResultPanel.transform.GetChild(2).GetComponent<Text>().text = " ";//비교가
        BiddingResultPanel.transform.GetChild(4).GetComponent<Text>().text = AuctionPanel.transform.GetChild(17).GetComponent<Text>().text;//최종판매가격
    }

    public void OnClickAuctionHelp()//옥션도움말
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        AuctionHelpPanel.SetActive(true);
        AuctionButtonEnableOff();//버튼비활성화
        AuctionHelpButton.GetComponent<Button>().interactable = false;
    }
    public void OnClickAuctionHelpExit()//옥션도움말 끄기
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        AuctionHelpPanel.SetActive(false);
        AuctionButtonEnableOn();//버튼비활성화
        AuctionHelpButton.GetComponent<Button>().interactable = true;
    }
  
    public void OnClickButtonCostUp1()//X1
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount += 1;

            if (ItemCount > int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                ItemCount = int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount += 1;

            if (MyItemCount > int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                MyItemCount = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }
    public void OnClickButtonCostUp10()//X10
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount += 10;

            if (ItemCount > int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                ItemCount = int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount += 10;

            if (MyItemCount > int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                MyItemCount = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }
    public void OnClickButtonCostUp100()//X100
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount += 100;

            if (ItemCount > int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                ItemCount = int.Parse(Regex.Replace(DealPanel_ItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount += 100;

            if (MyItemCount > int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")))
                MyItemCount = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", ""));

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }

    public void OnClickButtonCostDown1()//X1
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount -= 1;

            if (ItemCount <= 1)
                ItemCount = 1;

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount -= 1;

            if (MyItemCount <= 1)
                MyItemCount = 1;

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }
    public void OnClickButtonCostDown10()//X10
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount -= 10;

            if (ItemCount <= 1)
                ItemCount = 1;

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount -= 10;

            if (MyItemCount <= 1)
                MyItemCount = 1;

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }
    public void OnClickButtonCostDown100()//X100
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        if (DealPanel.activeSelf == true)
        {
            ItemCount -= 100;

            if (ItemCount <= 1)
                ItemCount = 1;

            ItemCost = int.Parse(DealPanel_ItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            ItemWeight = ItemDbScript.arrItemData[int.Parse(DealPanel_ItemPanel.transform.GetChild(10).GetComponent<Text>().text)].weight; //아이템무게

            //UI 갱신
            ItemWeightText.text = "무게:" + (Mathf.Round(ItemWeight * ItemCount * 10) * 0.1f).ToString("N1");//무게
            ItemCostText.text = (ItemCost * ItemCount).ToString();//가격
            ItemCountText.text = "X" + ItemCount.ToString();//개수
        }
        else
        {
            MyItemCount -= 100;

            if (MyItemCount <= 1)
                MyItemCount = 1;

            //데이터 갱신
            MyItemCost = int.Parse(SalePanel_MyItemPanel.transform.GetChild(5).GetComponent<Text>().text);//아이템가격
            MyItemWeight = float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(9).GetComponent<Text>().text, @"\D", ""))
                / float.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(3).GetComponent<Text>().text, @"\D", "")) * 0.1f; //아이템무게
            MyItemCompareCost_int = int.Parse(Regex.Replace(SalePanel_MyItemPanel.transform.GetChild(13).GetComponent<Text>().text, @"\D", ""));//비교가

            //UI 갱신
            MyItemWeightText.text = "무게:" + (Mathf.Round(MyItemWeight * MyItemCount * 10) * 0.1f).ToString("N1");//무게
            MyItemCostText.text = (MyItemCost * MyItemCount).ToString();//가격
            MyItemCountText.text = "X" + MyItemCount.ToString();//개수
            MyItemCompareCost.text = "비교:       " + (MyItemCompareCost_int * MyItemCount).ToString();//비교가
        }
    }

    //GameInfo
    public void onClickGameInfoButton()
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        GameInfoPanel.SetActive(true);
    }
    public void OnClickGameInfoExitButton()
    {
        //버튼클릭시 사운드재생
        audio.clip = ButtonSound;//사운드변경
        audio.Play();

        GameInfoPanel.SetActive(false);
    }

    //옵션

    public void OnClickOptionButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        ///데이터로드
        GameDataScript.LoadPlayerDataToJson();

        //패널활성화
        OptionPanel.SetActive(true);

    }
    public void ExitOptionButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        //패널비활성화
        OptionPanel.SetActive(false);
    }
    public void SoundEffectValueChange()//효과음 On 
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        if (audio.mute == false)
        {
            audio.mute = true;
            player_script.audio.mute = true;
            UI.GetComponent<AudioSource>().mute = true;

            StartSceneInfo.SoundEffectEnable = false;
            GameDataScript.PlayerData.SoundEffect = false;   
        }
        else
        {
            audio.mute = false;
            player_script.audio.mute = false;
            UI.GetComponent<AudioSource>().mute = false;

            StartSceneInfo.SoundEffectEnable = true;
            GameDataScript.PlayerData.SoundEffect = true;
        }

        ///데이터세이브
        GameDataScript.SavePlayerDataToJson();
    }
    public void BackGroundSoundValueChange()//배경음 On 
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        if (mainCamera.GetComponent<AudioSource>().mute == false)
        {
            mainCamera.GetComponent<AudioSource>().mute = true;
            StartSceneInfo.backGroundSoundEnable = false;
            GameDataScript.PlayerData.BackGroundSound = false;
        }
        else
        {
            mainCamera.GetComponent<AudioSource>().mute = false;
            StartSceneInfo.backGroundSoundEnable = true;
            GameDataScript.PlayerData.BackGroundSound = true;
        }

        ///데이터세이브
        GameDataScript.SavePlayerDataToJson();
    }

    public void OnClickSoundButton()//효과음+배경음 onoff
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        if (audio.mute == false && mainCamera.GetComponent<AudioSource>().mute == false)//OFF
        {
            BackGroundSoundToggle.isOn = false;
            SoundEffectToggle.isOn = false;
        }
        else if (audio.mute == true && mainCamera.GetComponent<AudioSource>().mute == true)//ON
        {
            BackGroundSoundToggle.isOn = true;
            SoundEffectToggle.isOn = true;
        }
        else if (audio.mute == true || mainCamera.GetComponent<AudioSource>().mute == true)//ON
        {
            BackGroundSoundToggle.isOn = true;
            SoundEffectToggle.isOn = true;
        }
        else//OFF
        {
            BackGroundSoundToggle.isOn = false;
            SoundEffectToggle.isOn = false;
        }
    }

    public void OnClickExitCancel()///게임종료취소
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        ExitGameWarning.SetActive(false);
    }
    public void OnClickExitGameWarning()//게임종료 확인창
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        audio.clip = ButtonSound;
        audio.Play();

        ExitGameWarning.SetActive(true);
    }
    public void OnClickExitGame()//게임종료
    {
        //오디오
        audio.clip = ButtonSound;

        audio.Play();
        audio.clip = ButtonSound;
        audio.Play();

        Application.Quit();//어플리케이션 종료
    }


    //코루틴
    IEnumerator PurchaseX()//구매불가-코인부족
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        CoinLack.gameObject.SetActive(true);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        PurchaseItemButton.GetComponent<Button>().enabled = true;
        CoinLack.gameObject.SetActive(false);
    }
    IEnumerator PurchaseX_2()//구매불가-무게부족
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        WeightLack.gameObject.SetActive(true);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        PurchaseItemButton.GetComponent<Button>().enabled = true;
        WeightLack.gameObject.SetActive(false);
    }
    IEnumerator PurchaseX_4()//구매불가-슬롯초과
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        SlotLack.gameObject.SetActive(true);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        PurchaseItemButton.GetComponent<Button>().enabled = true;
        SlotLack.gameObject.SetActive(false);
    }
    IEnumerator PurchaseX_3()//구매불가-가방구매불가
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        BagLack.gameObject.SetActive(true);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        PurchaseItemPanel.GetComponent<Image>().color = new Color(219f, 219f, 219f);
        PurchaseItemButton.GetComponent<Button>().enabled = true;
        BagLack.gameObject.SetActive(false);
    }

    IEnumerator PurchaseX_5()//당근 경매불가
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        CarrotWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        CarrotWarning.gameObject.SetActive(false);
    }


    IEnumerator Sale_Fail()//판매불가
    {
        //RackSound
        audio.clip = RackSound;//사운드변경
        audio.Play();

        SaleFail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SaleFail.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SaleFail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SaleFail.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SaleFail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SaleFail.gameObject.SetActive(false);
        saleFailDelay = false;//딜레이초기화
    }
}
