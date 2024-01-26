using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float score;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Å¬¸¯");
    }

    private void Touch()
    {

    }
}
