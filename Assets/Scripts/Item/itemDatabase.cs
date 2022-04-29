using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class itemDatabase : MonoBehaviour
{
    [Serializable]
    public struct itemData
    {
        public Sprite itemImage;//아이템이미지
        public string itemName;//아이템이름
        public int itemAveragePrice;//아이템평균가격
        public int itemAverageVolume;//아이템평균물량
        public float weight;//아이템무게
    };

    public itemData[] arrItemData;//아이템 정보 배열

    public int carrotPoint;



}
    //아이템목록
    /* 1.사과(Apple) 2.당근(Carrot) */

