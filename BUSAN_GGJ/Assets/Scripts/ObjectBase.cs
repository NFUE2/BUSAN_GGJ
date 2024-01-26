using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] //������ �Ǵ� ȸ�� ��ġ
    private int index;

    public float speed;//�̵� ���ǵ�

    public void OnPointerClick(PointerEventData eventData) //Ŭ�������� ����
    {
        Touch();
    }

    private void Update() //��� �������ΰ�
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Touch() //ȹ��
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) //������ �Ǵ� ȸ��
    {
        StageManager.Instance.HP = index;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //public float Set_Speed { set { speed = value; } get}
}
