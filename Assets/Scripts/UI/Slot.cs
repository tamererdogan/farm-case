using UnityEngine;
using UnityEngine.EventSystems;

public class Slot
    : MonoBehaviour,
        IDragHandler,
        IEndDragHandler,
        IPointerEnterHandler,
        IPointerExitHandler
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
                int? itemId = InventoryManager.Instance.GetItemId(id);
                if (itemId == null)
                    return;
                ItemSO item = DataManager.Instance.GetItem((int)itemId);
                if (item == null)
                    return;
                InventoryManager.Instance.RemoveItem(id);

                Vector3 playerPosition = FirstPersonController
                    .Instance
                    .gameObject
                    .transform
                    .position;
                Vector3 randomOffset = Random.insideUnitSphere * 3f;
                Vector3 targetPosition = playerPosition + randomOffset;
                targetPosition.y = 0.5f;
                Instantiate(item.prefab, targetPosition, Quaternion.Euler(0, 0, 180));

                return;
            }

            return;
        }

        Slot droppedSlot;
        eventData.pointerEnter.TryGetComponent(out droppedSlot);
        if (droppedSlot == null)
            return;

        if (type == "Inventory" && droppedSlot.type == "Inventory")
        {
            InventoryManager.Instance.ReplaceItem(id, droppedSlot.id);
            return;
        }

        if (type == "Inventory" && droppedSlot.type == "Market")
        {
            int? itemId = InventoryManager.Instance.GetItemId(id);
            if (itemId == null)
                return;
            ItemSO item = DataManager.Instance.GetItem((int)itemId);
            if (item == null)
                return;
            InventoryManager.Instance.AddMoney(item.sellPrice);
            InventoryManager.Instance.RemoveItem(id);
            return;
        }

        if (type == "Market" && droppedSlot.type == "Inventory")
        {
            int? itemId = MarketManager.Instance.GetItemId(id);
            if (itemId == null)
                return;
            ItemSO item = DataManager.Instance.GetItem((int)itemId);
            if (item == null)
                return;

            if (!InventoryManager.Instance.CheckMoney(item.buyPrice))
                return;

            InventoryManager.Instance.SubMoney(item.buyPrice);
            InventoryManager.Instance.AddItem(item.id);
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // string name = eventData.pointerEnter?.name;
        // string[] nameParts = name.Split("-");
        // int pointedSlotId = nameParts.Length >= 2 ? int.Parse(nameParts[0]) : -1;
        // string pointedSlotType = nameParts.Length >= 2 ? nameParts[1] : "";

        // if (pointedSlotId == -1 || pointedSlotType == "")
        //     return;

        // Item pointedItem = null;
        // if (pointedSlotType == "InventorySlot")
        // {
        //     pointedItem = InventoryManager.Instance.GetItem(pointedSlotId);
        // }
        // else if (pointedSlotType == "MarketSlot")
        // {
        //     pointedItem = MarketManager.Instance.GetItem(pointedSlotId);
        // }

        // if (pointedItem == null)
        //     return;

        // TooltipManager.Instance.ShowTooltip(pointedItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TooltipManager.Instance.HideTooltip();
    }
}
