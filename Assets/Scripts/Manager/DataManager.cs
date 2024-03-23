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

    public ItemSO GetItem(int id)
    {
        foreach (ItemSO item in items)
        {
            if (item.id != id)
                continue;

            return item;
        }

        return null;
    }
}
