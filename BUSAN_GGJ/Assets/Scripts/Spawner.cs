using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject creating; //생성 오브젝트
    [SerializeField] private float create_time; //생성주기 
    [SerializeField] private float[] level; //시간초가 올라감에따라 적용되는 오브젝트 속도
    [SerializeField] private int[] level_count; //시가초가 올라감에 따라 나타나는 오브젝트 갯수
    
    float cur_time = 0.0f;
    int obj_num = 0;

    // Update is called once per frame
    void Update()
    {
        cur_time += Time.deltaTime;

        if(cur_time > create_time && level_count[obj_num] != 0)
        {
            GameObject obj = Instantiate(creating);
            //obj.

        }
    }
}
