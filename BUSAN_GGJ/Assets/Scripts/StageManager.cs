using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;


public class StageManager : Singleton<StageManager>
{
    [SerializeField, Header("캐릭터 체력")] private float health = 10; 
    [SerializeField, Header("최대 시간")] private float timer;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] Animator character;
    //[SerializeField] private float speed;
    [SerializeField] private GameObject mini;
    [SerializeField,Header("스페이스바 클릭시 오르는 양")] private float bar_index;
    [SerializeField, Header("미니게임 제한시간")] private float minigame_timer; //미니게임 제한시간
    [SerializeField, Header("미니게임 데미지")] private int minidamage; //미니게임 데미지
    [SerializeField] private bool minigame = false; //미니게임 실행여부
    [SerializeField, Header("미니게임 시작 시간")] private float[] minigame_time; //미니게임 실행 시간
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject countdown_txt;

    //[SerializeField] private float fivertime;
    //[SerializeField] private float fiverspeed;
    //[SerializeField] private GameObject objectlist;


    Coroutine game = null;
    int minigame_num = 0;
    float event_timer = 0.0f;
    float mini_timer;
    //bool fiver = false;
    //bool gamestart = true;

    //[SerializeField, Header("게이지 다운 스피드")]
    //private float time_speed = 1.0f; //게이지 하락 스피드

    //[SerializeField]
    //private Slider gage; //게이지

    private void Start()
    {
        GameStop();
        StartCoroutine(Count_Down(3));
        //gage.value = gage.maxValue = max_value;
        //TryGetComponent(out character);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        text.text = timer.ToString("F1");

        event_timer += Time.deltaTime;
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
        //gage.value -= Time.deltaTime * time_speed; //게이지가 점점 줄어듬
    }

    

    public void GameStop()
    {
        //gamestart = false;
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        //gamestart = true;

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

        while (minigame_bar.value < 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                minigame_bar.value += bar_index;
                yield return null;
            }

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
        yield return new WaitForSecondsRealtime(1.0f);
        Debug.Log(1);
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
            cam.transform.position = Vector3.Lerp(cam.transform.position,pos,Time.unscaledDeltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.0f, Time.unscaledDeltaTime);

            if (Vector3.Distance(cam.transform.position, pos) < 0.01f)
            {
                cam.transform.position = pos;
                cam.orthographicSize = 5.0f;
                cam.orthographic = true;
                break;
            }
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);
        Resume();
    }
    //public void Fail_Check()
    //{
    //    if(gage.value <= 0)
    //    {
    //        GameStop();
    //        Debug.Log("패배");
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
