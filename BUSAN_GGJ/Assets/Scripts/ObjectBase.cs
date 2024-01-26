using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //데미지 또는 회복 수치
    private int index;

    public float speed;//이동 스피드

    public void OnPointerClick(PointerEventData eventData) //클릭했을때 반응
    {
        Touch();
    }

    private void Update() //계속 우측으로감
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Touch() //획득
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) //데미지 또는 회복
    {
        StageManager.Instance.HP = index;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //public float Set_Speed { set { speed = value; } get}
}
