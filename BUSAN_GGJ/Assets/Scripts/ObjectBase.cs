using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //������ �Ǵ� ȸ�� ��ġ
    private float index;


    private AudioSource audio;
    private Animator ani;
    public float speed;//�̵� ���ǵ�
    bool destroy = false;

    Coroutine coroutine = null;

    private void Start()
    {
        TryGetComponent(out ani);
        TryGetComponent(out audio);
    }

    public void OnPointerClick(PointerEventData eventData) //Ŭ�������� ����
    {
        Stop();
    }

    private void Update() //��� �������ΰ�
    {
        if(!destroy) transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Sound()
    {
        audio.PlayOneShot(audio.clip);
    }

    private void OnTriggerEnter2D(Collider2D collision) //������ �Ǵ� ȸ��
    {
        if(collision.tag != "Object")
        {
            StageManager.Instance.HP = index;
            Stop();
        }
        //gameObject.SetActive(false);
    }

    public void OnEvent()
    {
        Destroy(gameObject);
    }

    public void Stop() //�ı�
    {
        ani.SetTrigger("Destroy");
        destroy = true;

        //if(coroutine == null) coroutine = StartCoroutine(Sound());
    }

    //IEnumerator Sound()
    //{
    //    audio.PlayOneShot(audio.clip, GameManager.Instance.eff_vol);
    //    Debug.Log(12);
    //    if (!audio.isPlaying)
    //    {
    //        Debug.Log(1);
    //        Destroy(gameObject);
    //    }

    //    yield return null;
    //}
    //public float Set_Speed { set { speed = value; } get}
}
