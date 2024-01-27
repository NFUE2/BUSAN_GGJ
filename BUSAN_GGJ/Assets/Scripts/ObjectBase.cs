using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //데미지 또는 회복 수치
    private float index;


    private AudioSource audio;
    private Animator ani;
    public float speed;//이동 스피드
    bool destroy = false;

    Coroutine coroutine = null;

    private void Start()
    {
        TryGetComponent(out ani);
        TryGetComponent(out audio);
    }

    public void OnPointerClick(PointerEventData eventData) //클릭했을때 반응
    {
        Stop();
    }

    private void Update() //계속 우측으로감
    {
        if(!destroy) transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Sound()
    {
        audio.PlayOneShot(audio.clip);
    }

    private void OnTriggerEnter2D(Collider2D collision) //데미지 또는 회복
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

    public void Stop() //파괴
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
