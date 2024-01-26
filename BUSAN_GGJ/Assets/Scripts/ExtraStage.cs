using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ExtraStage : StageManager
{
    //[SerializeField, Header("������ �϶� �ִ뽺�ǵ�")]
    //private float maxspeed;

    [SerializeField, Header("���� �����ð�")]
    private float max_spanwtime;

    [SerializeField, Header("���� �����ð�")]
    private float min_spawntime;

    [SerializeField, Header("�Ѱ� �ð�")] //�����ð��� �� ���Ϸ� ������ �� ����;
    private float limit_spawntime;

    [SerializeField, Header("���̵�����")]
    private int level;

    [SerializeField, Header("������Ʈ �ִ�ӵ�")]
    private float max_speed;

    [SerializeField,Header("������ ����Ƿ�� �ö󰡴� �ӵ�")]
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
