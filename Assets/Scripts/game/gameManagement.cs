using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public static class GameManagerInfo
{
    public static int Coin = 0;
    public static float PlayTime = 0.0f;
}
public class gameManagement : MonoBehaviour
{
    //스테이지번호
    int stageNum = 0;

    //ButtonScript
    public GameObject ButtonScriptGameObject;
    ButtonScript buttonScript;

    //게임데이터 joson저장
    GameObject GameData;
    GameData GameDataScript;

    public enum STEP
    {
        STAGE0,//START
        STAGE1,//GAME
        STAGE2,//RESULT
    };

    //위치중인 마을
    public enum Town
    {
        SHOPPINGSTREET,
        APOLLUDE,
        MH,
        ATLANTIA,
        YOTAPLANT,
        AMBRAYLOSE,
        ELL,
        CROWALLEY,
        TRAINSTATION,
        ARTEDE
    };

    public STEP step = STEP.STAGE0;

    public Town town = Town.SHOPPINGSTREET;
    public Town next_town = Town.SHOPPINGSTREET;


    //플레이어 정보
    public int Coin;
    public float weight;
    public float fullWeight;//보유가능 무게 총량
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI weightText2;
    public TextMeshProUGUI coinText_store;
    public TextMeshProUGUI weightText_store;
    public TextMeshProUGUI SlotText_Inventory;
    public TextMeshProUGUI SlotText2_Inventory;
    public TextMeshProUGUI SlotText_store;

    //플레이타임
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        buttonScript = ButtonScriptGameObject.GetComponent<ButtonScript>();//버튼스크립트
        GameData = GameObject.FindGameObjectWithTag("GameData");
        GameDataScript = GameData.GetComponent<GameData>();
    }

    private void Awake()
    {
        Application.targetFrameRate = 30;
        //해상도
        // Screen.SetResolution(1920, 1080, true);
        // Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }
    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            GameManagerInfo.Coin = Coin;
            GameManagerInfo.PlayTime = playTime;
            SceneManager.LoadScene("ResultScene");

            GameDataScript.SavePlayerDataToJson();
        }*/

            coinText.text = Coin.ToString();//보유코인
            coinText_store.text = Coin.ToString();
            weightText.text = "무게:" + weight.ToString("F1") + "/" + fullWeight.ToString("F1");//보유무게 
            weightText2.text = "무게:" + weight.ToString("F1") + "/" + fullWeight.ToString("F1");//보유무게 
            weightText_store.text = "무게:" + weight.ToString("F1") + "/" + fullWeight.ToString("F1");//보유무게 
            SlotText_Inventory.text = "슬롯:" + buttonScript.InventoryIndex + "/12";//슬롯갱신 
            SlotText2_Inventory.text = "슬롯:" + buttonScript.InventoryIndex + "/12";//슬롯갱신 
            SlotText_store.text = "슬롯:" + buttonScript.InventoryIndex + "/12";//슬롯갱신 

        //데이터전송
       // GameDataScript.PlayerData.score = Coin;

        //플레이타임 
        playTime += Time.deltaTime;
    }
}
