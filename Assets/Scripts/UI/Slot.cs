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

        if (type == "Inventory" && droppedSlot.type == "ToolEquip")
        {
            int? itemId = InventoryManager.Instance.GetItemId(id);
            ItemSO item = DataManager.Instance.GetItem((int)itemId);
            if (item.GetType().FullName == "ToolSO")
            {
                int? toolItemId = EquipManager.Instance.GetToolItemId();
                if (toolItemId != null)
                {
                    InventoryManager.Instance.AddItem((int)toolItemId);
                }

                EquipManager.Instance.EquipTool(itemId);
                InventoryManager.Instance.RemoveItem(id);
            }

            return;
        }

        if (type == "ToolEquip" && droppedSlot.type == "Inventory")
        {
            int? itemId = EquipManager.Instance.GetToolItemId();
            if (itemId == null)
                return;

            EquipManager.Instance.EquipTool(null);
            InventoryManager.Instance.AddItem((int)itemId);

            return;
        }

        if (type == "Inventory" && droppedSlot.type == "SeedEquip")
        {
            int? itemId = InventoryManager.Instance.GetItemId(id);
            ItemSO item = DataManager.Instance.GetItem((int)itemId);
            if (item.GetType().FullName == "SeedSO")
            {
                int? seedItemId = EquipManager.Instance.GetSeedItemId();
                if (seedItemId != null)
                {
                    InventoryManager.Instance.AddItem((int)seedItemId);
                }

                EquipManager.Instance.EquipSeed(itemId);
                InventoryManager.Instance.RemoveItem(id);
            }

            return;
        }

        if (type == "SeedEquip" && droppedSlot.type == "Inventory")
        {
            int? itemId = EquipManager.Instance.GetSeedItemId();
            if (itemId == null)
                return;

            EquipManager.Instance.EquipSeed(null);
            InventoryManager.Instance.AddItem((int)itemId);

            return;
        }

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

            if (item.level > LevelManager.Instance.GetLevel())
            {
                ConsoleManager.Instance.AddMessage("Leveliniz yetersiz.", Color.red);
                return;
            }

            if (!InventoryManager.Instance.CheckMoney(item.buyPrice))
            {
                ConsoleManager.Instance.AddMessage("Paranız yetersiz.", Color.red);
                return;
            }

            InventoryManager.Instance.SubMoney(item.buyPrice);
            InventoryManager.Instance.AddItem(item.id);
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Slot pointerSlot;
        eventData.pointerEnter.TryGetComponent(out pointerSlot);
        if (pointerSlot == null)
            return;

        int? itemId = null;
        if (pointerSlot.type == "Inventory")
            itemId = InventoryManager.Instance.GetItemId(id);

        if (pointerSlot.type == "Market")
            itemId = MarketManager.Instance.GetItemId(id);

        if (itemId == null)
            return;

        ItemSO item = DataManager.Instance.GetItem((int)itemId);
        if (item == null)
            return;

        switch (item.GetType().FullName)
        {
            case "ToolSO":
                TooltipManager.Instance.ShowToolTooltip((ToolSO)item);
                break;
            case "SeedSO":
                TooltipManager.Instance.ShowSeedTooltip((SeedSO)item);
                break;
            case "CropSO":
                TooltipManager.Instance.ShowCropTooltip((CropSO)item);
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
