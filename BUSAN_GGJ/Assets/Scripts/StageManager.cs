using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Audio;


public class StageManager : Singleton<StageManager>
{
    [SerializeField, Header("ĳ���� ü��")] private float health = 10;
    [SerializeField, Header("�ִ� �ð�")] private float timer;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] Animator character;
    //[SerializeField] private float speed; 
    [SerializeField] private GameObject mini;
    [SerializeField,Header("�����̽��� Ŭ���� ������ ��")] private float bar_index;
    [SerializeField, Header("�̴ϰ��� ���ѽð�")] private float minigame_timer; //�̴ϰ��� ���ѽð�
    [SerializeField, Header("�̴ϰ��� ������")] private int minidamage; //�̴ϰ��� ������
    [SerializeField] private bool minigame = false; //�̴ϰ��� ���࿩��
    [SerializeField, Header("�̴ϰ��� ���� �ð�")] private float[] minigame_time; //�̴ϰ��� ���� �ð�
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject countdown_txt;
    [SerializeField] private AudioSource audio;

    //[SerializeField] private float fivertime;
    //[SerializeField] private float fiverspeed;
    //[SerializeField] private GameObject objectlist;


    Coroutine game = null;
    int minigame_num = 0;
    float event_timer = 0.0f;
    float mini_timer;
    //bool fiver = false;
    bool gamestart = false;

    public bool read_gamestart { get { return gamestart; } }

    //[SerializeField, Header("������ �ٿ� ���ǵ�")]
    //private float time_speed = 1.0f; //������ �϶� ���ǵ�

    //[SerializeField]
    //private Slider gage; //������

    protected virtual void Start()
    {
        //GameStop();
        StartCoroutine(Count_Down(3));
        //gage.value = gage.maxValue = max_value;
        //TryGetComponent(out character);
    }

    private void Update()
    {
        if(gamestart )
        {
            timer -= Time.deltaTime;
            event_timer += Time.deltaTime;
        }
        text.text = timer.ToString("F1");

        //if(!fiver && event_timer >= fivertime)
        //{
        //    fiver = true;

        //    for(int i = 0; i < objectlist.transform.childCount; i++)
        //        objectlist.transform.GetChild(i).GetComponent<ObjectBase>().speed = fiverspeed;
        //}

        if (minigame_num < minigame_time.Length && minigame_time[minigame_num] <= event_timer)
        {
            minigame = true;
            minigame_num++;
        }

        if(minigame)
            if(game == null)
            {
                mini_timer = minigame_timer;
                game = StartCoroutine(Minigame());
            }

        //Fail_Check();
        //gage.value -= Time.deltaTime * time_speed; //�������� ���� �پ��
    }

    

    public void GameStop()
    {
        gamestart = false;
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        gamestart = true;

    }

    public float HP
    {
        set
        {
            float sum = health + value;
            health = sum <= 0 ? 0 : sum >= 10 ? 10 : sum;
            character.SetFloat("health", health);
        }
        get { return health; }
    }

    IEnumerator Minigame()
    {
        mini.SetActive(true);
        Slider minigame_bar = mini.GetComponentInChildren<Slider>();
        minigame_bar.value = 0.0f;

        while (minigame_bar.value < 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                minigame_bar.value += bar_index;

            mini_timer -= Time.unscaledDeltaTime;

            if(mini_timer <= 0)
            {
                HP = -minidamage;
                MinigameEnd();
            }
            yield return null;
        }
        MinigameEnd();
    }

    private void MinigameEnd()
    {
        mini.SetActive(false);
        minigame = false;
        Resume();
        game = null;
        StopAllCoroutines();
    }

    IEnumerator Count_Down(int count)
    {
        yield return new WaitForSeconds(1.0f);
        count--;
        countdown_txt.GetComponent<TextMeshProUGUI>().text = count.ToString();
        HP = -2;

        if (count == 0)
        {
            countdown_txt.SetActive(false);
            StartCoroutine(ZoomOut());
        }
        else StartCoroutine(Count_Down(count));
    }

    IEnumerator ZoomOut()
    {
        Vector3 pos = new Vector3(0, 0, -10);
        while(true)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position,pos,Time.deltaTime * 2f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.0f, Time.deltaTime * 2f);

            if (Vector3.Distance(cam.transform.position, pos) < 0.01f)
            {
                cam.transform.position = pos;
                cam.orthographicSize = 5.0f;
                cam.orthographic = true;
                break;
            }
            yield return null;
        }

        //yield return new WaitForSecondsRealtime(0.5f);
        Resume();
        //audio.Play();
    }
    //public void Fail_Check()
    //{
    //    if(gage.value <= 0)
    //    {
    //        GameStop();
    //        Debug.Log("�й�");
    //        Debug.Log(Time.timeScale);
    //    }

    //}

    //public void Scucces_Check()
    //{
    //    if (gage.value >= 1)
    //    {
    //        GameStop();
    //    }
    //}
}
