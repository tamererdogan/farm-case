public class MarketManager : BagManager
{
    #region SINGLETON
    public static MarketManager Instance { get; private set; }

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

    protected override void Start()
    {
        base.Start();
        itemIds[0] = 0;
        itemIds[1] = 1;
        itemIds[2] = 2;
        itemIds[3] = 3;
        itemIds[4] = 4;
        itemIds[5] = 5;
        UpdateDisplay();
    }

    public int? GetItemId(int index)
    {
        return itemIds[index];
    }
}
