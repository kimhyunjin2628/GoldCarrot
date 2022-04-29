using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YotaPlant_ObjectMove : MonoBehaviour
{
    public GameObject[] ClockWork;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<=12; i++)
        {
            ClockWork[i].transform.Rotate(-60f * Time.deltaTime, 0, 0);
        }
    }
}
