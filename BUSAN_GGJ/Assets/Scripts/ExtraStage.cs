using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ExtraStage : StageManager
{
    //[SerializeField, Header("게이지 하락 최대스피드")]
    //private float maxspeed;

    [SerializeField, Header("시작 스폰시간")]
    private float max_spanwtime;

    [SerializeField, Header("최저 스폰시간")]
    private float min_spawntime;

    [SerializeField, Header("한계 시간")] //최저시간이 이 이하로 떨어질 수 없음;
    private float limit_spawntime;

    [SerializeField, Header("난이도설정")]
    private int level;

    [SerializeField, Header("오브젝트 최대속도")]
    private float max_speed;

    [SerializeField,Header("게임이 진행되루록 올라가는 속도")]
    private float speed_level = 1.0f;

    [SerializeField]
    private Transform[] tracks;

    [SerializeField]
    private GameObject[] Objects;

    [SerializeField]
    float time = 0.0f, spawntime;

    private void Start()
    {
        Set_SpanwTime();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= spawntime)
        {

            int track = Random.Range(0, tracks.Length);
            int num = Random.Range(0, Objects.Length);

            GameObject obj = Instantiate(Objects[num]);
            obj.transform.position = tracks[track].position;

            time -= spawntime;
            Set_SpanwTime();

            float speed = obj.GetComponent<ObjectBase>().speed;

            if (speed <= max_speed) obj.GetComponent<ObjectBase>().speed += Time.deltaTime * speed_level;
            if (min_spawntime >= limit_spawntime)
            {
                float min = Time.deltaTime * level;
                min_spawntime -= min;
                max_spanwtime -= min;
            }
        }
    }

    private void Set_SpanwTime()
    {
        spawntime = Random.Range(min_spawntime, max_spanwtime);
    }
}
