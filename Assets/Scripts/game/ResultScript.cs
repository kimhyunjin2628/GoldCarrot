using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{

    public TextMeshProUGUI playTime;
    public TextMeshProUGUI Gold;

    public TextMeshProUGUI[] Rank = new TextMeshProUGUI[6];

    //게임데이터 joson저장
    GameObject GameData;
    GameData GameDataScript;

    //신기록 
    public GameObject newRecord;//신기록

    //게임옵션패널
    public GameObject OptionPanel;
    public GameObject ExitGameWarning;//나가기 재확인버튼

    //체크박스
    public Toggle SoundEffectToggle;
    public Toggle BackGroundSoundToggle;

    public GameObject mainCamera;//메인카메라(오디오추출)

    //오디오
    new private AudioSource audio;
    public AudioClip ButtonSound;

    // Start is called before the first frame update
    void Start()
    {
        //결과
        ResultUpdate();
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


        //신기록탐색
        if (GameDataScript.HighestRecordSearch(GameManagerInfo.Coin) == true)//신기록
            newRecord.SetActive(true);
        else
            newRecord.SetActive(false);

        //데이터저장
        if (GameDataScript.PlayerData.index == 10)//최소값 찾아서 그자리에 삽입
        {
            GameDataScript.PlayerData.score[GameDataScript.LeastRecordSearch()] = GameManagerInfo.Coin;
        }
        else
        {
            GameDataScript.PlayerData.score[GameDataScript.PlayerData.index] = GameManagerInfo.Coin;
            GameDataScript.PlayerData.index++;
        }
        //최근스코어
        GameDataScript.PlayerData.score[10] = GameManagerInfo.Coin;
        GameDataScript.SavePlayerDataToJson();
    }

    void ResultUpdate()
    {
        int hour = (int)GameManagerInfo.PlayTime / 3600;
        int minute = ((int)GameManagerInfo.PlayTime % 3600) / 60;
        int second = ((int)GameManagerInfo.PlayTime % 3600) % 60;
        string hourText;
        string minuteText;
        string secondText;

        if (hour < 10)
            hourText = "0" + hour;
        else
            hourText = hour.ToString();

        if (minute < 10)
            minuteText = "0" + minute;
        else
            minuteText = minute.ToString();

        if (second < 10)
            secondText = "0" + second;
        else
            secondText = second.ToString();


        playTime.text ="PlayTime" + hourText + ":" + minuteText + ":" + secondText;
        Gold.text = GameManagerInfo.Coin.ToString();

        if (GameManagerInfo.Coin < 15000)//D
        {
            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[0].gameObject.SetActive(true);
        }
        else if (GameManagerInfo.Coin < 25000)//C
        {

            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[1].gameObject.SetActive(true);
        }
        else if (GameManagerInfo.Coin < 40000)//B
        {

            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[2].gameObject.SetActive(true);
        }
        else if (GameManagerInfo.Coin < 60000)//A
        {

            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[3].gameObject.SetActive(true);
        }
        else if (GameManagerInfo.Coin < 120000)//S
        {

            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[4].gameObject.SetActive(true);
        }
        else //SS
        {

            for (int i = 0; i <= 5; i++)
            {
                Rank[i].gameObject.SetActive(false);
            }
            Rank[5].gameObject.SetActive(true);
        }
    }

    public void onClickStartSceneButton()
    {
        //오디오
        audio.clip = ButtonSound;
        audio.Play();

        SceneManager.LoadScene("StartScene");
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
