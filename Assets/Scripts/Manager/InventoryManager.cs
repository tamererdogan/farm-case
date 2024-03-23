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
}
