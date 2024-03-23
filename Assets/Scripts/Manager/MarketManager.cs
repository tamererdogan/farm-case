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
        UpdateDisplay();
    }
}
