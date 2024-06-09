using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private int ObjectType;
    Vector3 startPosition;
    Vector3 startScale;
    bool isDragging = false;
    BoxCollider boxCollider;

    void Start()
    {
        startScale = transform.localScale;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isDragging)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Slot slot = hit.collider.GetComponent<Slot>();
                if (slot != null && slot.transform.childCount == 0) // If the slot is not full
                {
                    transform.position = slot.transform.position;
                }
                else
                {
                    transform.position = hit.point;
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0)) // When the mouse is released
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Slot slot = hit.collider.GetComponent<Slot>();
                if (slot != null && slot.transform.childCount == 0) // If the slot is not full
                {
                    GameManager.instance.RemoveObjectToGroup(transform.parent.GetComponent<Slot>().GetGroupIndex(), transform.parent.GetComponent<Slot>().GetSlotIndex());
                    transform.position = slot.transform.position;
                    transform.SetParent(slot.transform);
                    GameManager.instance.AddObjectToGroup(transform.parent.GetComponent<Slot>().GetGroupIndex(), transform.parent.GetComponent<Slot>().GetSlotIndex(), ObjectType);
                }
                else
                {
                    transform.position = startPosition;
                    transform.localScale = startScale;
                }
            }
            else
            {
                transform.position = startPosition;
                transform.localScale = startScale;
            }

            isDragging = false;
            boxCollider.enabled = true;
        }
    }

    void OnMouseDown()
    {
        startPosition = transform.position;
        isDragging = true;
        boxCollider.enabled = false;
    }

    public void SetObjectDefaultParent(GameObject _Slot)
    {
        transform.SetParent(_Slot.transform);
        transform.localPosition = Vector3.zero;
        GameManager.instance.AddObjectToGroup(_Slot.GetComponent<Slot>().GetGroupIndex(), _Slot.GetComponent<Slot>().GetSlotIndex(), ObjectType);
    }
}