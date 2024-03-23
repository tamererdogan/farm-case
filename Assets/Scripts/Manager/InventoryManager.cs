using TMPro;
using UnityEngine;

public class InventoryManager : BagManager
{
    #region SINGLETON
    public static InventoryManager Instance { get; private set; }

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
    private float money;

    public TMP_Text moneyUI;

    protected override void Start()
    {
        base.Start();
        UpdateMoneyDisplay();
        UpdateDisplay();
    }

    public int? GetItemId(int index)
    {
        return itemIds[index];
    }

    public void AddItem(int itemId)
    {
        for (int i = 0; i < itemIds.Count; i++)
        {
            if (itemIds[i] != null)
                continue;

            itemIds[i] = itemId;
            break;
        }
        UpdateDisplay();
    }

    public void ReplaceItem(int indexOne, int indexTwo)
    {
        int? temp = itemIds[indexTwo];
        itemIds[indexTwo] = itemIds[indexOne];
        itemIds[indexOne] = temp;
        UpdateDisplay();
    }

    public void RemoveItem(int index)
    {
        itemIds.RemoveAt(index);
        UpdateDisplay();
    }

    public bool CheckMoney(float price)
    {
        return money >= price;
    }

    public void AddMoney(float money)
    {
        this.money += money;
        UpdateMoneyDisplay();
    }

    public void SubMoney(float money)
    {
        this.money -= money;
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        moneyUI.text = money.ToString();
    }
}
