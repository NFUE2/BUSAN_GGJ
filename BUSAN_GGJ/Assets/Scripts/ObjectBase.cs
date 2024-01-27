using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //데미지 또는 회복 수치
    private float index;

    private Animator ani;
    public float speed;//이동 스피드
    bool destroy = false;

    private void Start()
    {
        TryGetComponent(out ani);
    }

    public void OnPointerClick(PointerEventData eventData) //클릭했을때 반응
    {
        Touch();
    }

    private void Update() //계속 우측으로감
    {
        if(!destroy) transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Touch() //획득
    {
        gameObject.SetActive(false);
        Stop();
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

    private void Stop()
    {
        ani.SetTrigger("Destroy");
        destroy = true;
    }

    //public float Set_Speed { set { speed = value; } get}
}
