using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StageManager : Singleton<StageManager>
{

    [SerializeField, Header("카운트 다운 시간")]
    private float time; //카운트 다운

    [SerializeField, Header("카운트 다운 스피드")]
    private float time_speed; //카운트다운 스피드

    [SerializeField, Header("메세지 종류")]
    private string[] msg; //메세지 종류

    [SerializeField]
    private Slider gage; //게이지

    [SerializeField]
    private TextMeshProUGUI time_txt; //시간 표시 텍스트

    [SerializeField]
    private GameObject msg_box; //메세지를 담을 박스

    private void Start()
    {
        //count_txt.text = count.ToString();
    }

    private void Update()
    {
        if (time < 0) //게임 패배
        {

        }
        else
            time -= Time.deltaTime * time_speed; //카운트 다운

        if(gage.value >= 1.0f) //게임 승리
        {

        }
    }

    private void GameStop()
    {
        Time.timeScale = 0.0f;
    }
    private void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
