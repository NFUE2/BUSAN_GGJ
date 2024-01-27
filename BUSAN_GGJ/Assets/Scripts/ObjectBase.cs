using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //������ �Ǵ� ȸ�� ��ġ
    private float index;

    private Animator ani;
    public float speed;//�̵� ���ǵ�
    bool destroy = false;

    private void Start()
    {
        TryGetComponent(out ani);
    }

    public void OnPointerClick(PointerEventData eventData) //Ŭ�������� ����
    {
        Stop();
    }

    private void Update() //��� �������ΰ�
    {
        if(!destroy) transform.position += Vector3.right * speed * Time.deltaTime;
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

    private void Stop() //�ı�
    {
        ani.SetTrigger("Destroy");
        destroy = true;
    }

    //public float Set_Speed { set { speed = value; } get}
}
