using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StageManager : Singleton<StageManager>
{

    [SerializeField, Header("ī��Ʈ �ٿ� �ð�")]
    private float time; //ī��Ʈ �ٿ�

    [SerializeField, Header("ī��Ʈ �ٿ� ���ǵ�")]
    private float time_speed; //ī��Ʈ�ٿ� ���ǵ�

    [SerializeField, Header("�޼��� ����")]
    private string[] msg; //�޼��� ����

    [SerializeField]
    private Slider gage; //������

    [SerializeField]
    private TextMeshProUGUI time_txt; //�ð� ǥ�� �ؽ�Ʈ

    [SerializeField]
    private GameObject msg_box; //�޼����� ���� �ڽ�

    private void Start()
    {
        //count_txt.text = count.ToString();
    }

    private void Update()
    {
        if (time < 0) //���� �й�
        {

        }
        else
            time -= Time.deltaTime * time_speed; //ī��Ʈ �ٿ�

        if(gage.value >= 1.0f) //���� �¸�
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
