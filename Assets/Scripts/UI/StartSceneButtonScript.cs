using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public static class StartSceneInfo
{
    //모자인덱스
    public static int hatIndex = 0;

    //사운드플래그
    public static bool backGroundSoundEnable = true;
    public static bool SoundEffectEnable = true;

}
public class StartSceneButtonScript : MonoBehaviour
{

    //모자
    public GameObject[] PlayerHat = new GameObject[6];
    public Image blackScreen;
    bool fadein = false;
    float blackScreenOpacity;

    //버튼
    public Button leftHatChangeButton;
    public Button RightHatChangeButton;
    public Button GameStartButton;
    public Button InfoButton;
    public Button GameRecordButton;

    //설명패널
    public GameObject InfoPanel;

    //게임데이터 joson저장
    GameObject GameData;
    GameData GameDataScript;

    //기록패널
    public GameObject RecordPanel;
    public GameObject[] RecordRank = new GameObject[5];

    //게임옵션패널
    public Button OptionButton;//옵션버튼
    public GameObject OptionPanel;
    public GameObject ExitGameWarning;//나가기 재확인버튼

    //오디오
    //audio
    new public AudioSource audio;
    public AudioClip ButtonSound;

    //체크박스
    public Toggle SoundEffectToggle;
    public Toggle BackGroundSoundToggle;

    public GameObject mainCamera;//메인카메라(오디오추출)

    // Start is called before the first frame update
    void Start()
    {
        GameData = GameObject.FindGameObjectWithTag("GameData");
        GameDataScript = GameData.GetComponent<GameData>();

        //오디오 정보 - 로드데이터
        if (GameDataScript.PlayerData.SoundEffect == true)
            SoundEffectToggle.isOn = true;
        else
            SoundEffectToggle.isOn = false;

        if (GameDataScript.PlayerData.BackGroundSound == true)
            BackGroundSoundToggle.isOn = true;
        else
            BackGroundSoundToggle.isOn = false;

        //오디오 정보
        if (StartSceneInfo.backGroundSoundEnable == true)
            BackGroundSoundToggle.isOn = true;
        else
            BackGroundSoundToggle.isOn = false;

        if (StartSceneInfo.SoundEffectEnable == true)
            SoundEffectToggle.isOn = true;
        else
            SoundEffectToggle.isOn = false;

       // audio = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        Application.targetFrameRate = 30;
        StartSceneInfo.hatIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            blackScreen.color = new Color(0,0,0,blackScreenOpacity);
            blackScreenOpacity += 0.01f;
            if (blackScreenOpacity >= 1f)///씬전환
            {
                blackScreenOpacity = 0.0f;
                blackScreen.color = new Color(0, 0, 0, 1);
                fadein = false;
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void OnClickRightCustomizingButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        if (StartSceneInfo.hatIndex == 5)
            StartSceneInfo.hatIndex = -1;
        StartSceneInfo.hatIndex++;
        Switching_Hat();
    }
    public void OnClickLeftCustomizingButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        if (StartSceneInfo.hatIndex == 0)
            StartSceneInfo.hatIndex = 6;
        StartSceneInfo.hatIndex--;
        Switching_Hat();
    }
    public void Switching_Hat()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == StartSceneInfo.hatIndex)
                PlayerHat[i].SetActive(true);
            else if (PlayerHat[i].gameObject.activeSelf)
                PlayerHat[i].SetActive(false);
        }
    }

    public void OnClickStartButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        fadein = true;
        //버튼비활성화
        GameStartButton.interactable = false;
        leftHatChangeButton.interactable = false;
        RightHatChangeButton.interactable = false;
        InfoButton.interactable = false;
        GameRecordButton.interactable = false;
        OptionButton.interactable = false;

        InfoPanel.SetActive(false);
    }

    public void OnClickInfoButton()
    {
        //오디오
        audio.clip = ButtonSound;

        audio.Play();
        InfoPanel.SetActive(true);
    }
    public void OnClickInfoExitButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        InfoPanel.SetActive(false);
    }

    //기록 버튼
    public void OnClickRecordPanelButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        //불러오기
        GameDataScript.LoadPlayerDataToJson();//데이터불러오기
        //오름차순 정렬
        GameDataScript.scoreTop5();

        RecordPanel.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameDataScript.top5array[i].ToString();//Top4
        }
        RecordPanel.transform.GetChild(4).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameDataScript.PlayerData.score[10].ToString();//최근데이터

        //랭크표기
        for (int i = 0; i <= 4; i++)
        {
            if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) == 0)//기록없음
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) < 15000)//D
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(true);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) < 25000)//C
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(true);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) < 40000)//B
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(true);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) < 60000)//A
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(true);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else if (int.Parse(RecordPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) < 120000)//S
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(true);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(false);
            }
            else//SS
            {
                RecordRank[i].transform.GetChild(0).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(1).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(2).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(3).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(4).gameObject.SetActive(false);
                RecordRank[i].transform.GetChild(5).gameObject.SetActive(true);
            }
        }
     }
    public void ExitRecordPanelButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        RecordPanel.SetActive(false);
    }

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
            StartSceneInfo.SoundEffectEnable = false;
            GameDataScript.PlayerData.SoundEffect = false;
        }
        else
        {
            audio.mute = false;
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

}
