using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject creating; //���� ������Ʈ
    [SerializeField] private float create_time; //�����ֱ� 
    [SerializeField] private float[] level; //�ð��ʰ� �ö󰨿����� ����Ǵ� ������Ʈ �ӵ�
    [SerializeField] private int[] level_count; //�ð��ʰ� �ö󰨿� ���� ��Ÿ���� ������Ʈ ����
    
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
