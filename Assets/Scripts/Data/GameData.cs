using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GameData : MonoBehaviour
{
    public PlayerData PlayerData;
    public static GameData instance;
    public int[] top5array = new int[5];

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()//Combine ->뒤에 자동으로 / 붙혀줌
    {
        string jsonData = JsonUtility.ToJson(PlayerData, true);
        string path = Application.persistentDataPath + "playerData.json";
        File.WriteAllText(path, jsonData);
    }


    [ContextMenu("From Json Data")]
    public void LoadPlayerDataToJson()
    {
        if (Path.Combine(Application.persistentDataPath + "playerData.json") == null)
            return;

        string path = Application.persistentDataPath + "playerData.json";
        //pc:Application.dataPath + "playerData.json"
        //mobile:pplication.persistentDataPath + "data/data/com.hj.goldcarrot/files"
        if (File.Exists(path) == false)
            return;

        string jsonData = File.ReadAllText(path);
        PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }
    private void Start()
    {
        //Debug.Log("Start");
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        LoadPlayerDataToJson();//데이터불러오기
        scoreTop5();//데이터정렬
    }

    //정렬
    public void scoreTop5()
    {
        int temp = 0;
        for (int i = 0; i <= 9; i++)//오름차순
        {
            for (int j = 0; j <= 9; j++)
            {
                if (PlayerData.score[i] > PlayerData.score[j])
                {
                    temp = PlayerData.score[i];
                    PlayerData.score[i] = PlayerData.score[j];
                    PlayerData.score[j] = temp;
                }
            }
        }
        top5array[0] = PlayerData.score[0];//0
        top5array[1] = PlayerData.score[1];//1
        top5array[2] = PlayerData.score[2];//2
        top5array[3] = PlayerData.score[3];//3

    }

    public int LeastRecordSearch()
    {
        int tmp = PlayerData.score[0];
        int tempIndex = 0;
        for (int i = 1; i < 10; i++)
        {
            if (PlayerData.score[i] < tmp)
            {
                tmp = PlayerData.score[i];
                tempIndex = i;
            }
        }
        return tempIndex;
    }

    public bool HighestRecordSearch(int CurrentRecord)
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerData.score[i] > CurrentRecord)
                return false;
        }
        return true;//신기록
    }
}

[System.Serializable]
public class PlayerData
{
    public int[] score = new int[11];
    public int index;
    public bool SoundEffect = true;
    public bool BackGroundSound = true;
}
