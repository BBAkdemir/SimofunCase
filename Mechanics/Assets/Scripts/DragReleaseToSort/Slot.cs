using UnityEngine;

public class Slot : MonoBehaviour
{
    int slotIndex;
    int groupIndex;
    private void OnTriggerEnter(Collider other)
    {
        DragAndDrop dragDrop = other.GetComponent<DragAndDrop>();
        if (dragDrop)
        {
            dragDrop.transform.position = transform.position;
        }
    }

    public void SetSlotIndex(int _Index)
    {
        slotIndex = _Index + 1;
        if (slotIndex == 1 || slotIndex == 2 || slotIndex == 5 || slotIndex == 6)
        {
            groupIndex = 1;
        }
        else if (slotIndex == 3 || slotIndex == 4 || slotIndex == 7 || slotIndex == 8)
        {
            groupIndex = 2;
        }
        else if (slotIndex == 9 || slotIndex == 10 || slotIndex == 13 || slotIndex == 14)
        {
            groupIndex = 3;
        }
        else
        {
            groupIndex = 4;
        }
        if (slotIndex == 1 || slotIndex == 3 || slotIndex == 9 || slotIndex == 11)
        {
            slotIndex = 1;
        }
        else if (slotIndex == 2 || slotIndex == 4 || slotIndex == 10 || slotIndex == 12)
        {
            slotIndex = 2;
        }
        else if (slotIndex == 5 || slotIndex == 7 || slotIndex == 13 || slotIndex == 15)
        {
            slotIndex = 3;
        }
        else
        {
            slotIndex = 4;
        }
    }

    public int GetGroupIndex() { return groupIndex; }
    public int GetSlotIndex() { return slotIndex; }
}