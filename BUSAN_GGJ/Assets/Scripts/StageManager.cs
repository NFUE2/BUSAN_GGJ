using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;


public class StageManager : Singleton<StageManager>
{
    [SerializeField, Header("캐릭터 체력")] private float health = 10;
    [SerializeField, Header("최대 시간")] private float timer;
    //[SerializeField] private TextMeshProUGUI text;
    [SerializeField] Animator character;
    [SerializeField] Slider slider;
    //[SerializeField] private float speed; 
    //[SerializeField] private GameObject mini;
    //[SerializeField,Header("스페이스바 클릭시 오르는 양")] private float bar_index;
    [SerializeField] private Animator heart;
    [SerializeField] private Transform object_field;
    [SerializeField] private GameObject alram;
    [SerializeField] private GameObject[] mini_obj;
    [SerializeField] private int mini_object_index;
    [SerializeField, Header("미니게임 제한시간")] private float minigame_timer; //미니게임 제한시간
    [SerializeField, Header("미니게임 데미지")] private int minidamage; //미니게임 데미지
    [SerializeField] private bool minigame = false; //미니게임 실행여부
    [SerializeField, Header("미니게임 시작 시간")] private float[] minigame_time; //미니게임 실행 시간
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject countdown_txt;
    [SerializeField] private AudioSource audio;
    [SerializeField] private GameObject ending;
    [SerializeField] private Sprite[] ending_list;

    [SerializeField] UnityEvent Event;

    [SerializeField] AudioClip[] clip;
    [SerializeField] AudioSource effect_audio;

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

    //[SerializeField, Header("게이지 다운 스피드")]
    //private float time_speed = 1.0f; //게이지 하락 스피드

    //[SerializeField]
    //private Slider gage; //게이지

    protected override void Awake()
    {
        Set_Instance2();
    }

    protected virtual void Start()
    {
        //GameStop();
        StartCoroutine(Count_Down(3));
        slider.value = slider.maxValue = timer;
        TryGetComponent(out audio);
        //gage.value = gage.maxValue = max_value;
        //TryGetComponent(out character);
    }

    private void Update()
    {
        if(gamestart)
        {
            slider.value -= Time.deltaTime;
            event_timer += Time.deltaTime;
        }
        //text.text = timer.ToString("F1");

        if (game != null) return;

        if(slider.value <= 0.0f)
        {
            gamestart = false;
            GameStop();
            StopAllCoroutines();

            if(game == null)
                game = StartCoroutine(Ending());
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape)) Event?.Invoke();


        //if(!fiver && event_timer >= fivertime)
        //{
        //    fiver = true;

        //    for(int i = 0; i < objectlist.transform.childCount; i++)
        //        objectlist.transform.GetChild(i).GetComponent<ObjectBase>().speed = fiverspeed;
        //}

        if (minigame_num < minigame_time.Length && minigame_time[minigame_num] <= event_timer)
        {
            Debug.Log(event_timer);
            minigame = true;
            minigame_num++;
        }

        if (minigame)
            if (game == null)
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
            if (value < 0)
            {
                heart.SetTrigger("Dameged");
                if(gamestart) effect_audio.PlayOneShot(clip[1]);
            }
        }
        get { return health; }
    }


    #region old
    //IEnumerator Minigame()
    //{
    //    mini.SetActive(true);
    //    Slider minigame_bar = mini.GetComponentInChildren<Slider>();
    //    minigame_bar.value = 0.0f;

    //    while (minigame_bar.value < 1)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //            minigame_bar.value += bar_index;

    //        mini_timer -= Time.unscaledDeltaTime;

    //        if(mini_timer <= 0)
    //        {
    //            HP = -minidamage;
    //            MinigameEnd();
    //        }
    //        yield return null;
    //    }
    //    MinigameEnd();
    //}

    //private void MinigameEnd()
    //{
    //    mini.SetActive(false);
    //    minigame = false;
    //    Resume();
    //    game = null;
    //    StopAllCoroutines();
    //}
    #endregion

    protected IEnumerator Minigame()
    {
        alram.SetActive(true);
        yield return new WaitForSeconds(1);
        alram.SetActive(false);

        List<GameObject> list = new List<GameObject>();

        float dx = object_field.localScale.x / 2;
        float dy = object_field.localScale.y / 2;

        Vector2 origin = new Vector2(object_field.position.x,object_field.position.y);

        for(int i = 0; i < mini_object_index; i++)
        {
            int num = Random.Range(0, 4);
            float x = Random.Range(origin.x - dx, origin.x + dx);
            float y = Random.Range(origin.y - dy, origin.y + dy);

            Vector2 pos = new Vector2(x,y);
            GameObject obj = Instantiate(mini_obj[num],pos,Quaternion.identity);
            list.Add(obj);
        }

        //int index = 0;
        float limit_time = minigame_timer;

        while(list.Count > 0)
        {
            bool iterable = true;
            if(Input.GetKeyDown(KeyCode.Space) && iterable)
            {
                Delete_Object(list);

                iterable = false;

                //list[0].SetActive(false);
                //index++;
            }

            limit_time -= Time.deltaTime;

            if(limit_time <= 0)
            {
                HP = minidamage;
                while(list.Count > 0)
                    Delete_Object(list);

                break;
            }
            yield return null;
        }
        minigame = false;
        game = null;
    }

    private void Delete_Object(List<GameObject> list)
    {
        GameObject obj = list[0];
        effect_audio.PlayOneShot(clip[0]);
        //obj.GetComponent<ObjectBase>().Sound();
        //obj.GetComponent<ObjectBase>().Stop();
        list.Remove(obj);
        Destroy(obj);
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
            audio.Play();
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
        //Resume();
        gamestart = true;
        Time.timeScale = 1.0f;
        //audio.Play();
    }

    IEnumerator Ending()
    {
        int num = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Hard" ? 3 : 0;
        ending.SetActive(true);

        if (HP <= 3)
        {
            ending.GetComponentInChildren<Image>().sprite = ending_list[0];
            GameManager.Instance.album[0 + num] = true;
        }
        else if(HP <= 8)
        {
            ending.GetComponentInChildren<Image>().sprite = ending_list[1];
            GameManager.Instance.album[1 + num] = true;
        }
        else
        {
            ending.GetComponentInChildren<Image>().sprite = ending_list[2];
            GameManager.Instance.album[2 + num] = true;
        }


        yield return null;
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
