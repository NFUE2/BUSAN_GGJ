using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Level_Type
{
    Low,
    Middel,
    High
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject damage_obj; //생성 오브젝트
    [SerializeField] private GameObject heal_obj; //생성 오브젝트
    [SerializeField] private Transform[] tracks;
    [SerializeField] private float heal_time;
    #region
    [SerializeField] private float[] low_level1;
    [SerializeField] private float[] low_level2;
    [SerializeField] private float[] low_level3;
    [SerializeField] private float[] low_level4;
    [SerializeField] private float[] low_level5;

    [SerializeField] private float[] middle_level1;
    [SerializeField] private float[] middle_level2;
    [SerializeField] private float[] middle_level3;
    [SerializeField] private float[] middle_level4;
    [SerializeField] private float[] middle_level5;

    [SerializeField] private float[] high_level1;
    [SerializeField] private float[] high_level2;
    [SerializeField] private float[] high_level3;
    [SerializeField] private float[] high_level4;
    [SerializeField] private float[] high_level5;
    #endregion

    [SerializeField] Level_Type[] level_list;

    [SerializeField] float fivertime;
    [SerializeField] float speed;
    bool fiver = false;

    Coroutine coroutine = null;

    Dictionary<int, float[]> low_level = new Dictionary<int, float[]>();
    Dictionary<int, float[]> middel_level = new Dictionary<int, float[]>();
    Dictionary<int, float[]> high_level = new Dictionary<int, float[]>();

    List<float[]> level = new List<float[]>();


    float cur_time = 0.0f,cur_time2;
    int num1 = 0,num2 = 0;

    private void Start()
    {
        Set_Level(low_level,low_level1, low_level2, low_level3, low_level4, low_level5);
        Set_Level(middel_level, middle_level1, middle_level2, middle_level3, middle_level4, middle_level5);
        Set_Level(high_level, high_level1, high_level2, high_level3, high_level4, high_level5);


        for(int i = 0; i < level_list.Length; i++)
        {
            int num = Random.Range(0, 5);

            switch(level_list[i])
            {
                case Level_Type.Low:
                    level.Add(low_level[num]);
                    break;

                case Level_Type.Middel:
                    level.Add(low_level[num]);
                    break;

                case Level_Type.High:
                    level.Add(low_level[num]);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.Instance.read_gamestart)
        {
            cur_time2 = cur_time += Time.deltaTime;

            if (coroutine == null)
            {
                coroutine = StartCoroutine(Heal());
                //StartCoroutine(Heal());
            }
        }
        else
        {
            StopAllCoroutines();
            return;
        }

        if (90 - fivertime <= cur_time2) fiver = true;

        if(num2 < level[num1].Length && cur_time >= level[num1][num2])
        {
            int track = Random.Range(0, 2);
            GameObject obj = Instantiate(damage_obj, tracks[track].position,Quaternion.identity);
            if (fiver) obj.GetComponent<ObjectBase>().speed = speed;
            num2++;
        }

        if(cur_time >= 10)
        {
            cur_time -= 10;
            num1++;
            num2 = 0;
        }
    }

    private void Set_Level(Dictionary<int,float[]> dic ,params float[][] param)
    {
        for (int i = 0; i < param.Length; i++)
            dic[i] = param[i];
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(heal_time);
        int track = Random.Range(0, 2);
        GameObject obj = Instantiate(heal_obj, tracks[track].position, Quaternion.identity);
        StartCoroutine(Heal());
    }
}
