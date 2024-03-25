#nullable enable

using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region SINGLETON
    public static DataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    [SerializeField]
    private ItemSO[] items;

    public ItemSO? GetItem(int id)
    {
        foreach (ItemSO item in items)
        {
            if (item.id != id)
                continue;

            return item;
        }

        return null;
    }

    public string GetItemName(int id)
    {
        ItemSO? item = GetItem(id);
        return item == null ? "" : item.itemName;
    }

    public int GetItemExp(int id)
    {
        ItemSO? item = GetItem(id);
        return item == null ? 0 : item.taskExp;
    }

    public int[] GetItemWithMaxLevelFilter(int maxLevel)
    {
        List<int> itemIds = new List<int>();
        foreach (ItemSO item in items)
        {
            if (item.level <= maxLevel)
                itemIds.Add(item.id);
        }

        return itemIds.ToArray();
    }
}
