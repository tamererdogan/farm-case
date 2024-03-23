using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int id;
    public string type;

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null)
        {
            if (type == "Inventory")
            {
                Debug.Log(id + " slotundaki itemi yere at.");
                return;
            }

            return;
        }

        Slot droppedSlot;
        eventData.pointerEnter.TryGetComponent<Slot>(out droppedSlot);
        if (droppedSlot == null)
            return;

        if (type == "Inventory" && droppedSlot.type == "Inventory")
        {
            Debug.Log(id + " slotundaki itemi " + droppedSlot.id + " slotuyla yer değiştir.");
            return;
        }

        if (type == "Inventory" && droppedSlot.type == "Market")
        {
            Debug.Log(id + " slotundaki itemi markete sat.");
            return;
        }

        if (type == "Market" && droppedSlot.type == "Inventory")
        {
            Debug.Log(id + " slotundaki itemi satın al.");
            return;
        }
    }
}
