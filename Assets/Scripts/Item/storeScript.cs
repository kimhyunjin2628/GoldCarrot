using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storeScript : MonoBehaviour
{
    //게임매니지먼트
    public GameObject gameM;
    gameManagement gamemanagment_script;

    //플레이어
    public GameObject main_player;
    player_script player_script;

    //아이템목록
    public GameObject itemDatabase;
    public int storePointerIndex;//밟고있는상점
    public bool storeItemSet = false;//필드이동시 상점 아이템목록 초기화

    //상점아이템패널 최대
    static int storePanelMax = 50;

    //BagOnly
    public GameObject bagOnly1;
    public GameObject bagOnly2;

    //상점베이스
    public struct storeItemData
    {
        public int[] itemDataNum;//판매가능 아이템종류
        public int[] itemqualityNum;//아이템 품질변수

        //생성자
        public storeItemData(int[] itemDataNum, int[] itemqualityNum)
        {
            this.itemDataNum = itemDataNum;
            this.itemqualityNum = itemqualityNum;
        }
    };
    storeItemData[] arrstoreData = new storeItemData[storePanelMax];

    //상점정보
    public struct storeItemData2
    {
        public int[] itemCount;//실제 아이템 개수
        public int[] itemCost;//실제 아이템 가격
        public storeItemData2(int[] itemCount, int[] itemCost)
        {
            this.itemCount = itemCount;
            this.itemCost = itemCost;
        }
    };
    public storeItemData2[] arrstoreData2 = new storeItemData2[storePanelMax];

    //상점판매관련정보
    public struct MyStoreItemData
    {
        public int[] purchaseItemList;//구매 아이템 목록
        public int[] purchaseItemQuality;//구매아이템 품질
        public MyStoreItemData(int[] purchaseItemList, int[] purchaseItemQuality)
        {
            this.purchaseItemList = purchaseItemList;
            this.purchaseItemQuality = purchaseItemQuality;
        }
    };
    public MyStoreItemData[] arrMyStoreData = new MyStoreItemData[storePanelMax];

    //상점판매 가격
    public struct MyStoreItemData2
    {
        public int[] itemCost;//구매 아이템 목록
       public MyStoreItemData2(int[] itemCost)
        {
            this.itemCost = itemCost;
        }
    };
    public MyStoreItemData2[] arrMyStoreData2 = new MyStoreItemData2[storePanelMax];

    //아이템패널
    public GameObject[] storePanelButton;
    public GameObject panelPrefab;

    //아이템데이터베이스 스크립트
    itemDatabase itemDbScript;

    //아이템 품질별 프레임 DCBAS
    public Sprite[] itemFrame;

    //상점 입장 플래그
    public bool storeEnter = false;

    //인벤토리 정보
    public GameObject InventoryPanel;
    public GameObject MyStorePanel;

    //버튼스크립트
    public GameObject btnOb;
    ButtonScript btnScript;

    //비교 화살표 이미지
    public Sprite UpPoint;
    public Sprite DownPoint;
    public Sprite Empty;

    // Start is called before the first frame update
    void Start()
    {
        gamemanagment_script = gameM.GetComponent<gameManagement>();
        itemDbScript = itemDatabase.GetComponent<itemDatabase>();
        btnScript = btnOb.GetComponent<ButtonScript>();
        player_script = main_player.GetComponent<player_script>();
    }

    // Update is called once per frame
    void Update()
    {
        //필드별 아이템목록
        if (storeItemSet == true)//상점데이터 불러오기
        {
            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//0~9 -> 구매 아이템 목록 62는 임시 오류방지용 
            {
                //0
                int[] itemDataNum = {13,24};//판매 아이템목록 
                int storeLength = Random.Range(2,3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0),ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 62 };//구매 아이템 목록
                arrMyStoreData[0] = new MyStoreItemData(purchaseItemDataNum, RandomItemQuality(purchaseItemDataNum.Length));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, purchaseItemDataNum.Length));

                //1
                int[] itemDataNum1 = { 45,60 };//판매 아이템목록 
                int storeLength1 = Random.Range(2,3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 62 };//구매 아이템 목록
                arrMyStoreData[1] = new MyStoreItemData(purchaseItemDataNum1, RandomItemQuality(purchaseItemDataNum1.Length));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, purchaseItemDataNum1.Length));

                //2
                int[] itemDataNum2 = { 16, 17, 18 };//판매 아이템목록 
                int storeLength2 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = { 62 };//구매 아이템 목록
                arrMyStoreData[2] = new MyStoreItemData(purchaseItemDataNum2, RandomItemQuality(purchaseItemDataNum2.Length));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, purchaseItemDataNum2.Length));

                //3
                int[] itemDataNum3 = { 23,25 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 62 };//구매 아이템 목록
                arrMyStoreData[3] = new MyStoreItemData(purchaseItemDataNum3, RandomItemQuality(purchaseItemDataNum3.Length));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, purchaseItemDataNum3.Length));

                //4
                int[] itemDataNum4 = { 15,42 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 62 };//구매 아이템 목록
                arrMyStoreData[4] = new MyStoreItemData(purchaseItemDataNum4, RandomItemQuality(purchaseItemDataNum4.Length));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, purchaseItemDataNum4.Length));

                //5
                int[] itemDataNum5 = { 11,36};//판매 아이템목록 
                int storeLength5 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[5] = new storeItemData(RandomItemList(itemDataNum5, storeLength5), RandomItemQuality(storeLength5));
                arrstoreData2[5] = new storeItemData2(ItemCountList(5), ItemCostList(5));//개수,가격

                int[] purchaseItemDataNum5 = { 62 };//구매 아이템 목록
                arrMyStoreData[5] = new MyStoreItemData(purchaseItemDataNum5, RandomItemQuality(purchaseItemDataNum5.Length));
                arrMyStoreData2[5] = new MyStoreItemData2(ItemMyCostList(5, purchaseItemDataNum5.Length));

                //6
                int[] itemDataNum6 = { 14,28 };//판매 아이템목록 
                int storeLength6 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[6] = new storeItemData(RandomItemList(itemDataNum6, storeLength6), RandomItemQuality(storeLength6));
                arrstoreData2[6] = new storeItemData2(ItemCountList(6), ItemCostList(6));//개수,가격

                int[] purchaseItemDataNum6 = { 62 };//구매 아이템 목록
                arrMyStoreData[6] = new MyStoreItemData(purchaseItemDataNum6, RandomItemQuality(purchaseItemDataNum6.Length));
                arrMyStoreData2[6] = new MyStoreItemData2(ItemMyCostList(6, purchaseItemDataNum1.Length));

                //7
                int[] itemDataNum7 = { 27,37 };//판매 아이템목록 
                int storeLength7 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[7] = new storeItemData(RandomItemList(itemDataNum7, storeLength7), RandomItemQuality(storeLength7));
                arrstoreData2[7] = new storeItemData2(ItemCountList(7), ItemCostList(7));//개수,가격

                int[] purchaseItemDataNum7 = { 62 };//구매 아이템 목록
                arrMyStoreData[7] = new MyStoreItemData(purchaseItemDataNum7, RandomItemQuality(purchaseItemDataNum7.Length));
                arrMyStoreData2[7] = new MyStoreItemData2(ItemMyCostList(7, purchaseItemDataNum7.Length));

                //8
                int[] itemDataNum8 = { 31,67 };//판매 아이템목록 
                int storeLength8 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[8] = new storeItemData(RandomItemList(itemDataNum8, storeLength8), RandomItemQuality(storeLength8));
                arrstoreData2[8] = new storeItemData2(ItemCountList(8), ItemCostList(8));//개수,가격

                int[] purchaseItemDataNum8 = { 62 };//구매 아이템 목록
                arrMyStoreData[8] = new MyStoreItemData(purchaseItemDataNum8, RandomItemQuality(purchaseItemDataNum8.Length));
                arrMyStoreData2[8] = new MyStoreItemData2(ItemMyCostList(8, purchaseItemDataNum8.Length));

                //9
                int[] itemDataNum9 = { 32 };//판매 아이템목록 
                int storeLength9 = Random.Range(1, 2);//판매 아이템개수
                arrstoreData[9] = new storeItemData(RandomItemList(itemDataNum9, storeLength9), RandomItemQuality(storeLength9));
                arrstoreData2[9] = new storeItemData2(ItemCountList(9), ItemCostList(9));//개수,가격

                int[] purchaseItemDataNum9 = { 62 };//구매 아이템 목록
                arrMyStoreData[9] = new MyStoreItemData(purchaseItemDataNum9, RandomItemQuality(purchaseItemDataNum9.Length));
                arrMyStoreData2[9] = new MyStoreItemData2(ItemMyCostList(9, purchaseItemDataNum9.Length));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69,70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.APOLLUDE)//0~4 아폴루데
            {
                //0
                int[] itemDataNum = { 9, 14, 20, 21, 23, 25, 27, 43, 63 };//판매 아이템목록 
                int storeLength = Random.Range(2, 5);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 4,10,11,13,15,16,17,18,24,29,30
                ,31,34,36,37,40,42,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,2,8,51,58,59,62};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 14,12,19,21,28,32,38,43,63 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 4,10,11,13,15,16,17,18,24,29,30
                ,31,34,36,37,40,42,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,8,51,65,58,59};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 3,9,6,22,26,28,52,61,63 };//판매 아이템목록 
                int storeLength2 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = { 4,10,11,13,15,16,17,18,24,29,30
                ,31,34,36,37,40,42,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,2,8,33,51,57,58,59,62};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 14,19,20,23,25,26,27,39 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 4,10,11,13,15,16,17,18,24,29,30
                ,31,34,36,37,40,42,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,8,51,66,58,59,62};//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 5,6,7,9,21,22,20,63,64,68 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = {4,10,11,13,15,16,17,18,24,29,30
                ,31,34,36,37,40,42,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,2,8,51,57,62,65,66};//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.MH)//0~4 상인본부
            {
                //0
                int[] itemDataNum = { 6,7,13,15,21,37,40 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 4,5,9,10,20,24,25,27,28,29,34,38,42,
                44,53,54,55,60,67
                ,2,3,32,58,51,57,59,64,66,62};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 6,7,14,23,30,31,40,45 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 4,5,9,10,20,24,25,27,28,29,34,38,42,
                44,53,54,55,60,67
                ,3,12,32,35,41,57,59,64,66,68};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 11,16,17,18,22,26,32,56 };//판매 아이템목록 
                int storeLength2 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = { 4,5,9,10,20,24,25,27,28,29,34,38,42,
                44,53,54,55,60,67
                ,2,8,19,33,38,51,57,59,64,68};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 30,36,39,46,47,48,49,50,56 };//판매 아이템목록 
                int storeLength3 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = {4,5,9,10,20,24,25,27,28,29,34,38,42,
                44,53,54,55,60,67
                ,3,12,35,41,58,59,62,65,68};//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 30,39,43,52,61,63 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = {4,5,9,10,20,24,25,27,28,29,34,38,42,
                44,53,54,55,60,67
                ,3,33,38,62,64,68};//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }

            else if (gamemanagment_script.town == gameManagement.Town.ATLANTIA)//0~4 아틀란티아
            {
                //0
                int[] itemDataNum = { 3,11,14,30,36,39,43,55 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 6,7,9,10,15,18,20,21,22,23,24,25,26,27,28,31,
                40,42,45,48,49,50,53,54,56,60,67
                ,52,64,62};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 4,5,8,11,29,30,34,36,37,39,61,64 };//판매 아이템목록 
                int storeLength1 = Random.Range(4, 5);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 6,7,9,10,15,18,20,21,22,23,24,25,26,27,28,31,
                40,42,45,48,49,50,53,54,56,60,67
                ,12,19,62};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 2,34,35,39,44,46,47,65 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = { 6,7,9,10,15,18,20,21,22,23,24,25,26,27,28,31,
                40,42,45,48,49,50,53,54,56,60,67
                ,32,33,35,57,62,66};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 11,30,36,39,63 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 6,7,9,10,15,18,20,21,22,23,24,25,26,27,28,31,
                40,42,45,48,49,50,53,54,56,60,62,67
                ,52,64,68};//구매 아이템 목록
                int MystoreLength3 = Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 13,16,17,30,36,38,39,43,63,65 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 6,7,9,10,15,18,20,21,22,23,24,25,26,27,28,31,
                40,42,45,48,49,50,53,54,56,60,67
                ,12,19,58,59,62};//구매 아이템 목록
                int MystoreLength4 = Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.YOTAPLANT)//0~5 요타플랜트
            {
                //0
                int[] itemDataNum = { 4,5,29,34,35,41,61 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 7,10,11,13,14,15,16,17,18,20,21,23,24,27,28,30,36,37,39
                ,40,42,43,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,38,52,57,58,64,65,62};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 2,3,4,6,9,29,31,33,61 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 7,10,11,13,14,15,16,17,18,20,21,23,24,27,28,30,36,37,39
                ,40,42,43,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,38,52,59,64,65,66,59};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 4,6,8,12,19,29,34,51,61,68 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = { 6,7,10,15,18,20,21,22,23,24,27,28,
                40,42,45,48,49,50,53,54,56,60,67
                ,53,57,58,59,66};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 3,22,26,32,61,63,68 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 7,10,11,13,14,15,16,17,18,20,21,23,24,27,28,30,36,37,39
                ,40,42,43,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,52,53,64,65,66};//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 4,5,9,29,31,34,35,61,68 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 7,10,11,13,14,15,16,17,18,20,21,23,24,27,28,30,36,37,39
                ,40,42,43,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,52,64,65};//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //5
                int[] itemDataNum5 = { 4,6,25,34 };//판매 아이템목록 
                int storeLength5 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[5] = new storeItemData(RandomItemList(itemDataNum5, storeLength5), RandomItemQuality(storeLength5));
                arrstoreData2[5] = new storeItemData2(ItemCountList(5), ItemCostList(5));//개수,가격

                int[] purchaseItemDataNum5 = { 7,10,11,13,14,15,16,17,18,20,21,23,24,27,28,30,36,37,39
                ,40,42,43,44,45,46,47,48,49,50,53,54,55,56,60,67
                ,57,59,66};//구매 아이템 목록
                int MystoreLength5 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[5] = new MyStoreItemData(RandomItemList(purchaseItemDataNum5, MystoreLength5), RandomItemQuality(MystoreLength5));
                arrMyStoreData2[5] = new MyStoreItemData2(ItemMyCostList(5, MystoreLength5));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.AMBRAYLOSE)//0~5 앰브라이로스
            {
                //0
                int[] itemDataNum = { 7,13,15,16,17,18,24,38,42,44 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68 };//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 45,46,47,48,49,50,52,53,54,57 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 7,15,24,38,45,50,59,67 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = {2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 7,13,15,42,48,49,50,67 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68};//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 13,15,16,17,27,53,54 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68};//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //5
                int[] itemDataNum5 = { 15,42,45,46,47,48,57,59 };//판매 아이템목록 
                int storeLength5 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[5] = new storeItemData(RandomItemList(itemDataNum5, storeLength5), RandomItemQuality(storeLength5));
                arrstoreData2[5] = new storeItemData2(ItemCountList(5), ItemCostList(5));//개수,가격

                int[] purchaseItemDataNum5 = { 2,3,4,6,9,10,11,14,20,21,22,23,25,26,28,29,30
                ,31,32,33,34,35,36,37,39,40,41,43,51,55,56,58,60,61,62,63,64,65,66,68
                ,5,8,12,19};//구매 아이템 목록
                int MystoreLength5 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[5] = new MyStoreItemData(RandomItemList(purchaseItemDataNum5, MystoreLength5), RandomItemQuality(MystoreLength5));
                arrMyStoreData2[5] = new MyStoreItemData2(ItemMyCostList(5, MystoreLength5));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.ELL)//0~4 엘
            {
                //0
                int[] itemDataNum = { 2,4,6,32,33,34,35,39,41 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 5,7,10,13,14,16,17,18,20,21,22,24,26,27,28,29,30,31,37
                ,40,42,44,45,46,47,49,53,60,61,63,67
                ,5,12,38,52,66,53};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 2,9,11,15,32,33,41,56 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 3,10,14,20,21,22,26,28,29,30
                ,31,37,40,51,60,61,63,65,66
                ,3,5,8,38,52,66};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 6,34,35,36,39,43,48,54,55 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = {3,10,14,20,21,22,26,28,29,30
                ,31,37,40,51,60,61,63,65,66
                ,3,5,8,19,52,53,65,66};//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 2,23,25,55,57,58,62,64,68 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 3,10,14,20,21,22,26,28,29,30
                ,31,37,40,51,60,61,63,65,66
                ,3,5,66};//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 25,36,43,50,56,57,59 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 3,10,14,20,21,22,26,28,29,30
                ,31,37,40,51,60,61,63,65,66
                ,5,65,66};//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.CROWALLEY)//0~4 크로우 뒷골목 암시장
            {
                //0
                int[] itemDataNum = { 6, 7, 14, 20, 26, 48, 63, 68 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = {1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67};//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 4, 6, 7, 9, 26, 38, 65, 68 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = {1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67};//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 3, 4, 6, 7, 14, 38, 39, 65 };//판매 아이템목록 
                int storeLength2 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = {1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67 };//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 26, 43, 48, 65, 68 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67 };//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 6, 14, 20, 29, 38, 42, 65 };//판매 아이템목록 
                int storeLength4 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67 };//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //5
                int[] itemDataNum5 = { 3, 6, 14, 20, 29, 38, 39 ,42, 48, 65 };//판매 아이템목록 
                int storeLength5 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[5] = new storeItemData(RandomItemList(itemDataNum5, storeLength5), RandomItemQuality(storeLength5));
                arrstoreData2[5] = new storeItemData2(ItemCountList(5), ItemCostList(5));//개수,가격

                int[] purchaseItemDataNum5 = { 1,2,5,8,10,11,12,13,15,16,17,18,19,21,22,23,24,25,27,28,30,31,32,33,34,
                35,36,37,40,41,44,45,46,47,49,50,51,52,53,54,55,56,57,58,59,60,61,62,64,66,67 };//구매 아이템 목록
                int MystoreLength5 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[5] = new MyStoreItemData(RandomItemList(purchaseItemDataNum5, MystoreLength5), RandomItemQuality(MystoreLength5));
                arrMyStoreData2[5] = new MyStoreItemData2(ItemMyCostList(5, MystoreLength5));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.TRAINSTATION)//0~4 기차역
            {
                //0
                int[] itemDataNum = {  12, 14, 18, 20, 21, 22, 24, 28 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 1,2,3,4,5,6,7,9,10,11,13,15,16,17,19,23,25,26,27,29,30,31,32,34,35,36,
                37,38,39,41,42,43,45,46,47,48,50,52,53,54,55,56,57,58,59,60,61,62,63,65,67,68 };//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 33, 40, 44, 49, 51, 64, 66 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 1,2,3,4,5,6,7,9,10,11,13,15,16,17,19,23,25,26,27,29,30,31,32,34,35,36,
                37,38,39,41,42,43,45,46,47,48,50,52,53,54,55,56,57,58,59,60,61,62,63,65,67,68 };//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 8, 14, 20, 22, 28, 40, 49, 64, 66 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = {1,2,3,4,5,6,7,9,10,11,13,15,16,17,19,23,25,26,27,29,30,31,32,34,35,36,
                37,38,39,41,42,43,45,46,47,48,50,52,53,54,55,56,57,58,59,60,61,62,63,65,67,68 };//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 12, 18, 21, 24, 33, 40, 51 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 1,2,3,4,5,6,7,9,10,11,13,15,16,17,19,23,25,26,27,29,30,31,32,34,35,36,
                37,38,39,41,42,43,45,46,47,48,50,52,53,54,55,56,57,58,59,60,61,62,63,65,67,68 };//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 12, 14, 20, 22, 28, 40, 49, 64, 66 };//판매 아이템목록 
                int storeLength4 = Random.Range(3, 4);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 1,2,3,4,5,6,7,9,10,11,13,15,16,17,19,23,25,26,27,29,30,31,32,34,35,36,
                37,38,39,41,42,43,45,46,47,48,50,52,53,54,55,56,57,58,59,60,61,62,63,65,67,68 };//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
            else if (gamemanagment_script.town == gameManagement.Town.ARTEDE)//0~4 아르테데
            {
                //0
                int[] itemDataNum = { 5, 16, 21, 24, 27, 28, 31 };//판매 아이템목록 
                int storeLength = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[0] = new storeItemData(RandomItemList(itemDataNum, storeLength), RandomItemQuality(storeLength));
                arrstoreData2[0] = new storeItemData2(ItemCountList(0), ItemCostList(0));//개수,가격

                int[] purchaseItemDataNum = { 1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[0] = new MyStoreItemData(RandomItemList(purchaseItemDataNum, MystoreLength), RandomItemQuality(MystoreLength));
                arrMyStoreData2[0] = new MyStoreItemData2(ItemMyCostList(0, MystoreLength));

                //1
                int[] itemDataNum1 = { 44,52,53,58,62,65,66,67 };//판매 아이템목록 
                int storeLength1 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[1] = new storeItemData(RandomItemList(itemDataNum1, storeLength1), RandomItemQuality(storeLength1));
                arrstoreData2[1] = new storeItemData2(ItemCountList(1), ItemCostList(1));//개수,가격

                int[] purchaseItemDataNum1 = { 1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength1 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[1] = new MyStoreItemData(RandomItemList(purchaseItemDataNum1, MystoreLength1), RandomItemQuality(MystoreLength1));
                arrMyStoreData2[1] = new MyStoreItemData2(ItemMyCostList(1, MystoreLength1));

                //2
                int[] itemDataNum2 = { 5,21,27,52,58,65,67 };//판매 아이템목록 
                int storeLength2 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[2] = new storeItemData(RandomItemList(itemDataNum2, storeLength2), RandomItemQuality(storeLength2));
                arrstoreData2[2] = new storeItemData2(ItemCountList(2), ItemCostList(2));//개수,가격

                int[] purchaseItemDataNum2 = {1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength2 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[2] = new MyStoreItemData(RandomItemList(purchaseItemDataNum2, MystoreLength2), RandomItemQuality(MystoreLength2));
                arrMyStoreData2[2] = new MyStoreItemData2(ItemMyCostList(2, MystoreLength2));

                //3
                int[] itemDataNum3 = { 5, 16, 24, 31, 44, 53, 62, 66, 67 };//판매 아이템목록 
                int storeLength3 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[3] = new storeItemData(RandomItemList(itemDataNum3, storeLength3), RandomItemQuality(storeLength3));
                arrstoreData2[3] = new storeItemData2(ItemCountList(3), ItemCostList(3));//개수,가격

                int[] purchaseItemDataNum3 = { 1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength3 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[3] = new MyStoreItemData(RandomItemList(purchaseItemDataNum3, MystoreLength3), RandomItemQuality(MystoreLength3));
                arrMyStoreData2[3] = new MyStoreItemData2(ItemMyCostList(3, MystoreLength3));

                //4
                int[] itemDataNum4 = { 5, 16, 21, 24, 27, 28, 31, 44, 52, 53, 58, 65, 66, 67 };//판매 아이템목록 
                int storeLength4 = Random.Range(3, 5);//판매 아이템개수
                arrstoreData[4] = new storeItemData(RandomItemList(itemDataNum4, storeLength4), RandomItemQuality(storeLength4));
                arrstoreData2[4] = new storeItemData2(ItemCountList(4), ItemCostList(4));//개수,가격

                int[] purchaseItemDataNum4 = { 1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength4 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[4] = new MyStoreItemData(RandomItemList(purchaseItemDataNum4, MystoreLength4), RandomItemQuality(MystoreLength4));
                arrMyStoreData2[4] = new MyStoreItemData2(ItemMyCostList(4, MystoreLength4));

                //5
                int[] itemDataNum5 = { 21, 24, 28, 31, 52, 53, 58, 62 };//판매 아이템목록 
                int storeLength5 = Random.Range(2, 3);//판매 아이템개수
                arrstoreData[5] = new storeItemData(RandomItemList(itemDataNum5, storeLength5), RandomItemQuality(storeLength5));
                arrstoreData2[5] = new storeItemData2(ItemCountList(5), ItemCostList(5));//개수,가격

                int[] purchaseItemDataNum5 = { 1,2,3,4,6,7,8,9,10,11,12,13,14,15,17,18,19,20,22,23,25,26,29,30,32,33,34,35,
                36,37,38,39,40,41,42,43,45,46,47,48,49,50,51,54,55,56,57,59,60,61,63,64,68 };//구매 아이템 목록
                int MystoreLength5 =Random.Range(13, 20);//구매 아이템개수
                arrMyStoreData[5] = new MyStoreItemData(RandomItemList(purchaseItemDataNum5, MystoreLength5), RandomItemQuality(MystoreLength5));
                arrMyStoreData2[5] = new MyStoreItemData2(ItemMyCostList(5, MystoreLength5));

                //30 -> 거상HJ
                int[] itemDataNum30 = { 69, 70 };//판매 아이템목록 
                int[] itemQuality30 = { 5, 5 };//등급배열
                int[] itemCount30 = { 1, 1 };//개수배열
                int storeLength30 = Random.Range(2, 3);//판매 아이템개수

                if (player_script.currentBagNum == 2)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 1;
                }
                else if (player_script.currentBagNum == 3)
                {
                    itemCount30[0] = 0;
                    itemCount30[1] = 0;
                }
                arrstoreData[30] = new storeItemData(itemDataNum30, itemQuality30);
                arrstoreData2[30] = new storeItemData2(itemCount30, ItemCostList(30));


                int[] purchaseItemDataNum30 = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30
                ,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,
                61,62,63,64,65,66,67,68};//구매 아이템 목록
                int MystoreLength30 = Random.Range(68, 69);//구매 아이템개수
                arrMyStoreData[30] = new MyStoreItemData(RandomItemList(purchaseItemDataNum30, MystoreLength30), RandomItemQuality(MystoreLength30));
                arrMyStoreData2[30] = new MyStoreItemData2(ItemMyCostList(30, MystoreLength30));
            }
           
            //한번만실행
            storeItemSet = false;
        }//end of storeItemSet If

        //상점별 상점패널교체
        if (storeEnter == true)
        {
            StoreListLoading();//스토어리스트 갱신
            MyStoreListLoading();//myStoreData에 인벤토리정보 불러오기
            storeEnter = false;//한번만실행
        }
        /* if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//상점 입장시 한번만실행, 상점품목 고정 
         {
             if (storeEnter == true)
             {
                 StoreListLoading();//스토어리스트 갱신
                 MyStoreListLoading();//myStoreData에 인벤토리정보 불러오기
                 storeEnter = false;//한번만실행
             }
         }//end of if -> ShoppingStreet*/

        //Debug.Log(btnScript.InventoryIndex);
    }

    //메서드

    //아이템 등급설정
    int[] RandomItemQuality(int storeLength)//판매 아이템 개수만큼 랜덤등급배열 생성
    {
        int[] randomitemquality = new int[storeLength];
        for (int i = 0; i < storeLength; i++)
        {
            int Ran = Random.Range(1, 101);
            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//시작마을
            {
                randomitemquality[i] = 2;//C등급 고정
            }
            else if (Ran <= 60)
            {
                randomitemquality[i] = 1;//D
            }
            else if (Ran <= 80)
            {
                randomitemquality[i] = 2;//C
            }
            else if (Ran <= 90)
            {
                randomitemquality[i] = 3;//B
            }
            else if (Ran <= 97)
            {
                randomitemquality[i] = 4;//A
            }
            else
            {
                randomitemquality[i] = 5;//S
            }
        }
        return randomitemquality;
    }

    int[] ItemCountList(int pointerIndex)//포인터 위치에 따른 개수배열생성
    {
        int i = 0;
        int[] countarr = new int[arrstoreData[pointerIndex].itemDataNum.Length];

        for (; i < arrstoreData[pointerIndex].itemDataNum.Length; i++)
        {
            //Debug.Log("포인터인덱스" +pointerIndex + " i:" + i);
            int count = itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemAverageVolume;

            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET || pointerIndex == 30)//조건부 개수고정
            {
                count = itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemAverageVolume;
            }
            else if (count < 10) //0~10
            {
                count = (int)(count * Random.Range(0.5f, 2.5f));
                if (count == 0)
                    count = 1;
            }
            else if (count < 151)//11~150
                count = (int)(count * Random.Range(0.2f, 3.0f));
            else//150~
                count = (int)(count * Random.Range(0.2f, 2.0f));

            countarr[i] = count;
        }

        return countarr;
    }

    int[] ItemCostList(int pointerIndex)//포인터 위치에 따른 가격배열생성(상점)
    {
        int i = 0;
        int[] costarr = new int[arrstoreData[pointerIndex].itemDataNum.Length];
        for (; i < arrstoreData[pointerIndex].itemDataNum.Length; i++)
        {
            //Debug.Log(pointerIndex + ": " + itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemName + pointerIndex + ": " + itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemAverageVolume);
            int cost = itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemAveragePrice;

            if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET || pointerIndex == 30)//조건부 개수고정
            {
                cost = itemDbScript.arrItemData[arrstoreData[pointerIndex].itemDataNum[i]].itemAveragePrice;
            }
            else if (cost <= 20) //0~20
            {
                cost = (int)(cost * Random.Range(0.2f, 1.8f));
                if (cost == 0)
                    cost = 2;
            }
            else if (cost <= 200)//21~200
                cost = (int)(cost * Random.Range(0.2f, 1.8f));
            else if (cost <= 1000)//201 ~ 1000
                cost = (int)(cost * Random.Range(0.3f, 1.5f));
            else if (cost <= 10000)//1001 ~ 10000
                cost = (int)(cost * Random.Range(0.4f, 1.5f));
            else //10000 ~
                cost = (int)(cost * Random.Range(0.4f, 1.5f));



            //등급별 프리미엄
            if (pointerIndex == 30)//조건부 프리미엄 고정
            {
                cost = (int)(cost * 1.0f);
            }
            else if (gamemanagment_script.town == gameManagement.Town.SHOPPINGSTREET)//조건부 프리미엄 고정
            {
                cost = (int)(cost * 0.8f);
            }
            else if (arrstoreData[pointerIndex].itemqualityNum[i] == 1)//D
            {
                cost = (int)(cost * Random.Range(0.7f, 1.0f));
            }
            else if (arrstoreData[pointerIndex].itemqualityNum[i] == 2)//C
            {
                cost = (int)(cost * Random.Range(0.7f, 1.0f));
            }
            else if (arrstoreData[pointerIndex].itemqualityNum[i] == 3)//B
            {
                cost = (int)(cost * Random.Range(0.6f, 1.0f));
            }
            else if (arrstoreData[pointerIndex].itemqualityNum[i] == 4)//A
            {
                cost = (int)(cost * Random.Range(0.6f, 0.9f));
            }
            else if (arrstoreData[pointerIndex].itemqualityNum[i] == 5)//S
            {
                cost = (int)(cost * Random.Range(0.6f, 1.0f));
            }

            //가격0일때
            if (cost == 0)
                cost = 1;

            costarr[i] = cost;
        }
        return costarr;
    }

    int[] ItemMyCostList(int pointerIndex , int Length)//포인터 위치에 따른 가격배열생성(상점)
    {
        int i = 0;
        int[] costarr = new int[Length];
        for (; i < Length; i++)
        {
            int cost = itemDbScript.arrItemData[arrMyStoreData[pointerIndex]. purchaseItemList[i]].itemAveragePrice;
            if (pointerIndex == 30)//HJ포인터 가격고정
            {
                cost = cost / 2;
            }
            else if (cost <= 20) //0~20
            {
                cost = (int)(cost * Random.Range(0.5f, 2.5f));
                if (cost == 0)
                    cost = 1;
            }
            else if (cost <= 200)//21~200
                cost = (int)(cost * Random.Range(0.2f, 1.8f));
            else if (cost <= 1000)//201 ~ 1000
                cost = (int)(cost * Random.Range(0.5f, 1.6f));
            else if (cost <= 10000)//1001 ~ 10000
                cost = (int)(cost * Random.Range(0.7f, 1.5f));
            else //10000 ~
                cost = (int)(cost * Random.Range(0.6f, 2.1f));



            //등급별 프리미엄
            if (pointerIndex == 30f)//HJ프리미엄 고정
            {
                cost = cost * 1;
            }
            else if (arrMyStoreData[pointerIndex].purchaseItemQuality[i] == 1)//D
            {
                cost = (int)(cost * Random.Range(0.7f, 1.2f));
            }
            else if (arrMyStoreData[pointerIndex].purchaseItemQuality[i] == 2)//C
            {
                cost = (int)(cost * Random.Range(0.9f, 1.2f));
            }
            else if (arrMyStoreData[pointerIndex].purchaseItemQuality[i] == 3)//B
            {
                cost = (int)(cost * Random.Range(0.9f, 1.6f));
            }
            else if (arrMyStoreData[pointerIndex].purchaseItemQuality[i] == 4)//A
            {
                cost = (int)(cost * Random.Range(1.5f, 2.2f));
            }
            else if (arrMyStoreData[pointerIndex].purchaseItemQuality[i] == 5)//S
            {
                cost = (int)(cost * Random.Range(1.5f, 3.0f));
            }

            costarr[i] = cost;
        }
        return costarr;
    }


    //아이템 목록설정
    int[] RandomItemList(int[] itemDataNum,int storeLength)//판매아이템 개수만큼 판매아이템 목록에서 추출
    {
        int[] randomItemList = new int[storeLength];
        for (int i = 0; i < storeLength; i ++)
        {
            randomItemList[i] = itemDataNum[Random.Range(0, itemDataNum.Length )];

            //중복검사
            if (overLapChk(randomItemList) == true)//중복일경우
            {
                i--;
            }
        }
        return randomItemList;
    }

    //배열중복검사
    bool overLapChk(int[] randomItemList)
    {
        for (int i = 0; i < randomItemList.Length - 1; i++) 
        {
            for (int j = randomItemList.Length - 2; j >= i; j--)
            {
                if (randomItemList[i] == randomItemList[j + 1] && randomItemList[j+1] != 0)
                    return true;
            }
        }
        return false;
    }

    //상점 입장시 스토어리스트 불러오기
    void StoreListLoading()
    {
        int i = 0;

        //bagOnly
        if (storePointerIndex == 30)
        {
            bagOnly1.SetActive(true);
            bagOnly2.SetActive(true);
        }
        else
        {
            bagOnly1.SetActive(false);
            bagOnly2.SetActive(false);
        }

        for ( ; i < arrstoreData[storePointerIndex].itemDataNum.Length; i++)
        {
            //Debug.Log(i.ToString());
            //패널활성화
            storePanelButton[i].SetActive(true);
            //패널 아이템데이터 child(0~) -> 1.아이템이미지 2.품질별 액자 3.개수 5.가격 6.품질 7.아이템이름 8.아이템무게
            //1.아이템이미지
            storePanelButton[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = itemDbScript.arrItemData[arrstoreData[storePointerIndex].itemDataNum[i]].itemImage;
            //2.품질별 액자
            string itemQuality = null;
            Sprite itemFrame = this.itemFrame[0];
            switch (arrstoreData[storePointerIndex].itemqualityNum[i])
            {
                case 1:
                    itemFrame = this.itemFrame[0];//D
                    itemQuality = "D";
                    break;
                case 2:
                    itemFrame = this.itemFrame[1];//C
                    itemQuality = "C";
                    break;
                case 3:
                    itemFrame = this.itemFrame[2];//B
                    itemQuality = "B";
                    break;
                case 4:
                    itemFrame = this.itemFrame[3];//A
                    itemQuality = "A";
                    break;
                case 5:
                        itemFrame = this.itemFrame[4];//S
                        itemQuality = "S";
                    break;

            }
            storePanelButton[i].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = itemFrame;

            //3. 개수
            // storePanelButton[i].transform.GetChild(3).gameObject.GetComponent<Text>().text = "X" + arrstoreData2[storePointerIndex].itemCount[i];
            storePanelButton[i].transform.GetChild(3).gameObject.GetComponent<Text>().text = "X" + arrstoreData2[storePointerIndex].itemCount[i];

            //5. 가격
            int cost = arrstoreData2[storePointerIndex].itemCost[i];
            //공식필요
            storePanelButton[i].transform.GetChild(5).gameObject.GetComponent<Text>().text = cost.ToString();

            //6. 품질
            storePanelButton[i].transform.GetChild(6).gameObject.GetComponent<Text>().text = itemQuality;

            //7.아이템 이름 
            storePanelButton[i].transform.GetChild(7).gameObject.GetComponent<Text>().text = itemDbScript.arrItemData[arrstoreData[storePointerIndex].itemDataNum[i]].itemName;

            //9. 아이템 무게
            storePanelButton[i].transform.GetChild(9).gameObject.GetComponent<Text>().text = "한 개당 무게: " + itemDbScript.arrItemData[arrstoreData[storePointerIndex].itemDataNum[i]].weight;

            //10. 아이템번호
            storePanelButton[i].transform.GetChild(10).gameObject.GetComponent<Text>().text = arrstoreData[storePointerIndex].itemDataNum[i].ToString();

        }//end of for

        //뒷패널제거
        for (; i < 10; i++)
        {
            storePanelButton[i].SetActive(false);
        }
    }//end of StoreListLoading

    //상점 입장시 My스토어리스트 불러오기
    public void MyStoreListLoading()
    {
        for (int i = 0; i < btnScript.InventoryIndex; i++)
        {
            //패널활성화
            MyStorePanel.transform.GetChild(i).gameObject.SetActive(true);

            //MystorePanel패널정보(getChild)
            // 1.아이템이미지 2.품질Frame 3.개수 5.가격  6.품질 7.아이템이름 9.무게 12.최근구매가 13.비교가 14.비교 화살표 이미지
            // Inventory 
            // 1.아이템이미지 2.품질Frame 3.개수 6.최근구매가 7.품질 8.아이템이름 9.무게(총합)
            MyStorePanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Image>().sprite = InventoryPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Image>().sprite;//아이템이미지;
            MyStorePanel.transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().sprite = InventoryPanel.transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().sprite;//품질Frame;
            MyStorePanel.transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text = InventoryPanel.transform.GetChild(i).transform.GetChild(3).GetComponent<Text>().text;//개수;

            int item_index = 0;
            bool purchase_enable = false;//구매 불가능/가능
            int Hj_cost = 0;//거상hj 가격

            //Debug.Log(itemDbScript.arrItemData[arrMyStoreData[storePointerIndex].purchaseItemList[0]].itemName + "입니다");
            //가격
            for (int j = 0; j < arrMyStoreData[storePointerIndex].purchaseItemList.Length; j++)//구매가능 아이템인지 탐색
            {
                if (Equals(InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text ,
                    itemDbScript.arrItemData[arrMyStoreData[storePointerIndex].purchaseItemList[j]].itemName))
                {
                    purchase_enable = true;
                    item_index = j;//탐색완료 아이템인덱스
                    if(InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text == "당근" )//당근일경우
                        Hj_cost = arrMyStoreData2[storePointerIndex].itemCost[item_index] * 2;
                    else
                       Hj_cost = int.Parse(InventoryPanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text);
                }
            }

            int cost = arrMyStoreData2[storePointerIndex].itemCost[item_index];
            if (storePointerIndex == 30)//HJ포인터 가격변경
                cost = Hj_cost/2;//헐값에 구매 -> 최근구매가 / 2 

            if (purchase_enable == true)//구매 가능 아이템
            {
                MyStorePanel.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255);
                MyStorePanel.transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = cost.ToString();//가격
            }
            else//구매불가능 아이템
            {
                MyStorePanel.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 0, 0);
                MyStorePanel.transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = "X";//가격
                MyStorePanel.transform.GetChild(i).transform.GetChild(14).GetComponent<Image>().sprite = Empty;//비교이미지 없애기
            }

            MyStorePanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text = InventoryPanel.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text;//품질
            MyStorePanel.transform.GetChild(i).transform.GetChild(7).GetComponent<Text>().text = InventoryPanel.transform.GetChild(i).transform.GetChild(8).GetComponent<Text>().text;//아이템이름
            MyStorePanel.transform.GetChild(i).transform.GetChild(9).GetComponent<Text>().text = InventoryPanel.transform.GetChild(i).transform.GetChild(9).GetComponent<Text>().text;//무게
            MyStorePanel.transform.GetChild(i).transform.GetChild(12).GetComponent<Text>().text = InventoryPanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text;//최근구매가

            //비교
            int compareCost;//비교가
            if (purchase_enable == false)
            {
                MyStorePanel.transform.GetChild(i).transform.GetChild(13).GetComponent<Text>().text = "X";//비교가
            }
            else if (cost >= int.Parse(InventoryPanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text))//이득거래
            {
                compareCost = cost - int.Parse(InventoryPanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text);//가격비교
                MyStorePanel.transform.GetChild(i).transform.GetChild(14).GetComponent<Image>().sprite = UpPoint;//비교화살표이미지

                MyStorePanel.transform.GetChild(i).transform.GetChild(13).GetComponent<Text>().text = "비교:       " + compareCost.ToString();//비교가
            }
            else//손해거래
            {
                compareCost = int.Parse(InventoryPanel.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text) - cost;//가격비교
                MyStorePanel.transform.GetChild(i).transform.GetChild(14).GetComponent<Image>().sprite = DownPoint;//비교화살표이미지

                MyStorePanel.transform.GetChild(i).transform.GetChild(13).GetComponent<Text>().text = "비교:       " + compareCost.ToString();//비교가
            }
            
        }//end of for
    }//end of MystoreListLoading

    //아이템데이터베이스 이름탐색
    int ItemDatabaseSearch(string itemName)
    {
        for (int i = 0; i <= itemDbScript.arrItemData.Length; i++)
        {
            if (Equals(itemName, itemDbScript.arrItemData[i].itemName))
            {
                return i;//탐생성공
            }
        }
        return 0;//탐색실패
    }//end if Search
}
